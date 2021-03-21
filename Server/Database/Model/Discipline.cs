using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Database.Model
{
	class Discipline
	{
		public Discipline (string discipline_name, int discipline_id)
		{
			this.discipline_name = discipline_name;
			id_discipline = discipline_id;
		}
		public string discipline_name { get; set; }
		public int id_discipline{ get; set; }
	}
}
