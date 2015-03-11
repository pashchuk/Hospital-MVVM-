using System.Collections.Generic;

namespace HospitalLibrary
{
	public class Card
	{
		#region Properties

		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public SexType Sex { get; set; }
		public string Note { get; set; }
		public bool IsAgain { get; set; }

		#endregion

		#region Navigation Properties

		public virtual IEnumerable<Note> Notes { get; set; }
		public virtual IEnumerable<Session> Sessions { get; set; } 

		#endregion
	}
}
