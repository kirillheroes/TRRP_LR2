namespace Server.Database.Model
{
    public class Student
    {
        public Student(string full_name, int student_id, int faculty_id)
        {
            this.student_full_name = full_name;
            id_student = student_id;
            id_faculty = faculty_id;
        }

        public string student_full_name { get; set; }
        public int id_student { get; set; }

        public int id_faculty { get; set; }
    }
}
