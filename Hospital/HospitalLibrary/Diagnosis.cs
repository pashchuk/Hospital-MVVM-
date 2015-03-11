using System.Collections.Generic;

namespace HospitalLibrary
{
	public class Diagnosis
	{
		#region Properties

		public int Id { get; private set; }
		public string Name { get; set; }

		#endregion

		#region Navigation Properties

		public virtual List<Session> Sessions { get; set; } 

		#endregion

		public Diagnosis()
		{
			Sessions = new List<Session>();
		}
	}
}
