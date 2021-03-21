using Common.Crypto;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Client.Clients
{
    public class SocketClient
    {
        private readonly IPEndPoint _remoteEP;
        private readonly IPAddress _ip;
        public SocketClient(IPAddress ip, int port)
        {
            _ip = ip ?? throw new ArgumentNullException(nameof(ip));
            _remoteEP = new IPEndPoint(ip, port);
        }

        public void Send(string data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    Socket socket = new Socket(_ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(_remoteEP);

                    var des = GetDes(socket);

                    var encryptedData = Cryptographer.SymmetricEncrypt(data, des);
                    socket.Send(encryptedData);

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    socket.Dispose();
                }
            }
            catch (ArgumentNullException error)
            {
                Console.WriteLine("ОШИБКА аргументов : {0}", error.ToString());
            }
            catch (SocketException error)
            {
                Console.WriteLine("ОШИБКА сокетов: : {0}", error.ToString());
            }
            catch (Exception error)
            {
                Console.WriteLine("ОШИБКА: {0}", error.ToString());
            }
        }

        private DESCryptoServiceProvider GetDes(Socket socket)
        {
            byte[] buffer = new byte[1024];

            // Принимаем RSA public
            int sizeRSA = socket.Receive(buffer);
            var publicKeyJsonByte = new byte[sizeRSA];
            Array.Copy(buffer, 0, publicKeyJsonByte, 0, sizeRSA);

            var publicKeyJson = Encoding.UTF8.GetString(publicKeyJsonByte);
            var publicKey = JsonConvert.DeserializeObject<RSAPublicKeyParams>(publicKeyJson);
            var publicKeyParameters = publicKey.GetRSAParameters();

            var des = Cryptographer.GetDES();
            var iv = Cryptographer.RSAEncrypt(des.IV, publicKeyParameters);
            var key = Cryptographer.RSAEncrypt(des.Key, publicKeyParameters);

            var encrypdedDes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new DESParams(iv, key)));
            socket.Send(encrypdedDes);

            return des;
        }
    }
}
