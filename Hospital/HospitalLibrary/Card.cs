using System.Collections.Generic;

namespace HospitalLibrary
{
	public class Card
	{
		#region Properties

		public int Id { get;private set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public SexType Sex { get; set; }
		public string Note { get; set; }
		public bool IsAgain { get; set; }

		#endregion

		#region Navigation Properties

		public virtual List<Note> Notes { get; set; }
		public virtual List<Session> Sessions { get; set; } 

		#endregion

		public Card()
		{
			Notes = new List<Note>();
			Sessions = new List<Session>();
		}
	}
}
