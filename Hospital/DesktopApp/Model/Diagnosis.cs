using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
	public class Diagnosis
	{
		public int Id { get; private set; }
		public string Description { get; set; }

		public virtual List<Session> Sessions { get; private set; }

		public Diagnosis()
		{
			Sessions = new List<Session>();
		}
	}
}
