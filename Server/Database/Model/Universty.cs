using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Database.Model
{
	class Universty
	{
		public Universty(string university_name, int university_id)
		{
			this.university_name = university_name;
			id_university = university_id;
		}
		public string university_name { get; set; }
		public int id_university { get; set; }
	}
}
