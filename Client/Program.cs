using Client.Clients;
using Common.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Client
{
	class Program
	{
		const string DBPath = @"D:\Databases\SQLite\NenormUniversity.db";

		static void Main(string[] args)
		{
			var studentService = new StudentServiceProvider(DBPath);

			Socket(studentService);

			Console.WriteLine("Все данные были отправлены! Нажмите любую клавишу...");
			Console.ReadKey();
		}

        private static void Socket(StudentServiceProvider studentService)
        {
            var ip = Dns.GetHostEntry("localhost").AddressList[0];
            var port = 11000;
            var socketClient = new SocketClient(ip, port);

            foreach (List<StudentsAllData> data in studentService.GetAll(2))
            {
                var jsonData = JsonConvert.SerializeObject(data);

                socketClient.Send(jsonData);

                Console.WriteLine($"Отправленные данные:");
                Console.WriteLine(jsonData);
                Console.WriteLine();
                Console.WriteLine("Нажмите любую клавишу для отправки следующего пакета данных...");
                Console.ReadKey();
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
