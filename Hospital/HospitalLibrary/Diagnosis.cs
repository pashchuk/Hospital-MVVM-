using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary
{
	public class Diagnosis
	{
		#region Properties

		public int Id { get; set; }
		public string Name { get; set; }

		#endregion

		#region Navigation Properties

		public virtual IEnumerable<Session> Sessions { get; set; } 

		#endregion
	}
}
