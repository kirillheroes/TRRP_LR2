using Npgsql;

namespace Server.Database
{
	public class StudentsServiceProvider
    {
        private readonly string connectionString;

        public StudentsServiceProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int InsertUniversity(string university_name)
        {
            using var sConn = new NpgsqlConnection(connectionString);
            sConn.Open();

            var query = @"select id_university from university where name_university = @university_name;";
            var command = new NpgsqlCommand(query, sConn);
            command.Parameters.AddWithValue("@university_name", university_name);

            var id = (int?)command.ExecuteScalar();
            if (id is null)
            {
                var queryInsert = @"insert into university (name_university) values (@university_name) returning id_university";
                var commandInsert = new NpgsqlCommand(queryInsert, sConn);
                commandInsert.Parameters.AddWithValue("@university_name", university_name);
                id = (int)commandInsert.ExecuteScalar();
            }
            return (int)id;
        }

        public int InsertFaculty(string name_faculty, int id_university)
        {
            using var sConn = new NpgsqlConnection(connectionString);
            sConn.Open();

            var query = @"select id_faculty from faculty where name_faculty = @faculty_name and university_id = @id_university;";
            var command = new NpgsqlCommand(query, sConn);
            command.Parameters.AddWithValue("@faculty_name", name_faculty);
            command.Parameters.AddWithValue("@id_university", id_university);

            var id = (int?)command.ExecuteScalar();
            if (id is null)
            {
                var queryInsert = @"insert into faculty (name_faculty, university_id) values (@faculty_name, @id_university) returning id_faculty";
                var commandInsert = new NpgsqlCommand(queryInsert, sConn);
                commandInsert.Parameters.AddWithValue("@faculty_name", name_faculty);
                commandInsert.Parameters.AddWithValue("@id_university", id_university);
                id = (int)commandInsert.ExecuteScalar();
            }
            return (int)id;
        }

        public int InsertStudent(string fio, int id_faculty)
        {
            using var sConn = new NpgsqlConnection(connectionString);
            sConn.Open();

            var query = @"select id_student from student where full_name = @student_full_name;";
            var command = new NpgsqlCommand(query, sConn);
            command.Parameters.AddWithValue("@student_full_name", fio);

            var id = (int?)command.ExecuteScalar();
            if (id is null)
            {
                var queryInsert = @"insert into student (full_name, faculty_id) values (@student_full_name, @id_faculty) returning id_student";
                var commandInsert = new NpgsqlCommand(queryInsert, sConn);
                commandInsert.Parameters.AddWithValue("@student_full_name", fio);
                commandInsert.Parameters.AddWithValue("@id_faculty", id_faculty);
                id = (int)commandInsert.ExecuteScalar();
            }
            return (int)id;
        }

        public int InsertSubject(string name_subject)
        {
            using var sConn = new NpgsqlConnection(connectionString);
            sConn.Open();

            var query = @"select id_discipline from discipline where name_discipline = @discipline_name;";
            var command = new NpgsqlCommand(query, sConn);
            command.Parameters.AddWithValue("@discipline_name", name_subject);

            var id = (int?)command.ExecuteScalar();
            if (id is null)
            {
                var queryInsert = @"insert into discipline (name_discipline) values (@discipline_name) returning id_discipline";
                var commandInsert = new NpgsqlCommand(queryInsert, sConn);
                commandInsert.Parameters.AddWithValue("@discipline_name", name_subject);
                id = (int)commandInsert.ExecuteScalar();
            }
            return (int)id;
        }

        public int InsertGrade(int grade, int id_student, int id_subject)
        {
            using var sConn = new NpgsqlConnection(connectionString);
            sConn.Open();

            var query = @"select id_grade from grade where score = @grade and student_id = @id_student and discipline_id = @id_discipline;";
            var command = new NpgsqlCommand(query, sConn);
            command.Parameters.AddWithValue("@grade", grade);
            command.Parameters.AddWithValue("@id_student", id_student);
            command.Parameters.AddWithValue("@id_discipline", id_subject);

            var id = (int?)command.ExecuteScalar();
            if (id is null)
            {
                var queryInsert = @"insert into grade (student_id, discipline_id, score) values (@id_student, @id_discipline, @grade) returning id_grade";
                var commandInsert = new NpgsqlCommand(queryInsert, sConn);
                commandInsert.Parameters.AddWithValue("@id_student", id_student);
                commandInsert.Parameters.AddWithValue("@id_discipline", id_subject);
                commandInsert.Parameters.AddWithValue("@grade", grade);
                id = (int)commandInsert.ExecuteScalar();
            }
            return (int)id;
        }
    }
}
