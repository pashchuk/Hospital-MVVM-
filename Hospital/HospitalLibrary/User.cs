using System.Collections.Generic;

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

		public virtual List<Note> Notes { get; set; } 

		#endregion
	}
}
