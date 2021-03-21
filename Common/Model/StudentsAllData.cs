using System;

namespace Common.Model
{
	public class StudentsAllData
    {
        public StudentsAllData(string student_full_name, string faculty_name, string university_name, string discipline_name, int grade)
        {
            this.student_full_name = student_full_name ?? throw new ArgumentNullException(nameof(student_full_name));
            this.faculty_name = faculty_name ?? throw new ArgumentNullException(nameof(faculty_name));
            this.university_name = university_name ?? throw new ArgumentNullException(nameof(university_name));
            this.discipline_name = discipline_name ?? throw new ArgumentNullException(nameof(discipline_name));
            this.grade = grade;
        }

        public string student_full_name { get; set; }

        public string faculty_name { get; set; }

        public string university_name { get; set; }

        public string discipline_name { get; set; }

        public int grade { get; set; }
    }
}
