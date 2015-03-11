using System;

namespace HospitalLibrary
{
	public class Note
	{
		#region Properties

		public int Id { get; set; }
		public bool IsHidden { get; set; }
		public DateTime Date { get; set; }
		public string NoteText { get; set; }

		#endregion

		#region Navigation Properties

		public virtual User Author { get; set; }
		public virtual Card Card { get; set; }

		#endregion
	}
}
