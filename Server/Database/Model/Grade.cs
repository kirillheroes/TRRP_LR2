using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Database.Model
{
	class Grade
	{
        public Grade(int id_grade, int id_student, int id_discipline, int grade)
        {
            student_id = id_student;
            this.id_grade = id_grade;
            discipline_id = id_discipline;
            score = grade;
        }

        public int id_grade { get; set; }
        public int student_id { get; set; }
        public int discipline_id { get; set; }
        public int score { get; set; }
    }
}
