namespace Server.Database.Model
{
    public class Faculty
    {
        public Faculty(string faculty_name, int university_id, int faculty_id)
        {
            this.faculty_name = faculty_name;
            this.university_id = university_id;
            id_faculty = faculty_id;
        }

        public string faculty_name { get; set; }
        public int university_id { get; set; }

        public int id_faculty { get; set; }
    }
}

