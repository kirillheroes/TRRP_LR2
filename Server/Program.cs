using Common.Model;
using Newtonsoft.Json;
using Server.Core;
using Server.Core.Servers;
using Server.Database;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
	public class Program : IMessageHandler
    {
        private string ConnectionString = "";
        private readonly StudentsServiceProvider _graduatesService;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private object _locker;

        public Program()
        {
            _locker = new object();
            _cancellationTokenSource = new CancellationTokenSource();

            Console.WriteLine("Введите пароль пользователя postgres: ");
            string pass = Console.ReadLine();
            Console.Clear();
            ConnectionString = $"Host=localhost;Port=5432;Database=NormalizedStudents;Username=postgres;Password={pass};";
            _graduatesService = new StudentsServiceProvider(ConnectionString);
        }

        static void Main()
        {
            var program = new Program();
            Console.CancelKeyPress += (sender, e) =>
            {
                try
                {
                    program.Cancel();
                }
                finally
                {
                    e.Cancel = true;
                }
            };
            program.Run();
        }

        public void Run()
        {
            var ip = Dns.GetHostEntry("localhost").AddressList[0];
            var port = 11000;

            var servers = new List<IServer>()
            {
                new SocketServer(ip, port)
            };

            var tasks = servers
                .Select(server => Task.Run(() => server.Run(this, _cancellationTokenSource.Token)))
                .ToArray();
			Console.WriteLine("Ожидаю клиента...");

            Task.WaitAll(tasks);
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
        }

        public void Handle(string message)
        {
            Console.WriteLine("Полученные данные:");
            Console.WriteLine(message);

            var graduates = JsonConvert.DeserializeObject<List<StudentsAllData>>(message);

            foreach (var graduate in graduates)
            {
                lock (_locker)
                {
                    var id_university = _graduatesService.InsertUniversity(graduate.university_name);
                    var id_faculty = _graduatesService.InsertFaculty(graduate.faculty_name, id_university);
                    var id_student = _graduatesService.InsertStudent(graduate.student_full_name, id_faculty);
                    var id_subject = _graduatesService.InsertSubject(graduate.discipline_name);
                    var id_grade = _graduatesService.InsertGrade(graduate.grade, id_student, id_subject);
                }
            }

            Console.WriteLine("Полученные данные записаны!");
            Console.WriteLine();
        }
    }
}
