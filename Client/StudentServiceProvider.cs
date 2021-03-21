using Common.Model;
using System.Collections.Generic;
using System.Collections;
using System.Data.SQLite;

namespace Client
{
    public class StudentServiceProvider
    {
        private readonly string _connectionString;

        public StudentServiceProvider(string path)
        {
            _connectionString = new SQLiteConnectionStringBuilder
            {
                DataSource = path,
            }.ConnectionString;
        }

        public IEnumerable GetAll(int sliceSize)
        {
            using var conn = new SQLiteConnection(_connectionString);
            var sCommand = new SQLiteCommand()
            {
                Connection = conn,
                CommandText = @"SELECT student_full_name, faculty_name, university_name, discipline_name, grade FROM all_data;"
            };

            conn.Open();
            var reader = sCommand.ExecuteReader();

            var result = new List<StudentsAllData>();

            while (reader.Read())
            {
                string student_full_name = (string)reader["student_full_name"];
                string faculty_name = (string)reader["faculty_name"];
                string university_name = (string)reader["university_name"];
                string discipline_name = (string)reader["discipline_name"];
                int grade = (int)reader["grade"];
                result.Add(new StudentsAllData(student_full_name, faculty_name, university_name, discipline_name, grade));

                if (result.Count == sliceSize)
                {
                    yield return result;
                    result.Clear();
                }
            }

            if (result.Count > 0)
            {
                yield return result;
            }
        }
    }
}
