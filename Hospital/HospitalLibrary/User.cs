using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary
{
	public class User
	{
		#region Properties

		public int Id { get; set; }
		public string Name { get; set; }
		public int AccesLevel { get; set; }
		public string Phone { get; set; }
		public string Password { get; set; }

		#endregion

		#region Navigation Properties

		public virtual IEnumerable<Note> Notes { get; set; } 

		#endregion
	}
}
