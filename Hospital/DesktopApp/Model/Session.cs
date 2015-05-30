using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
	public class Session
	{
		public int Id { get; private set; }
		public bool IsAgain { get; set; }
		public DateTime Date { get; set; }

		public virtual Card Card { get; set; }
		public virtual Diagnosis Diagnosis { get; set; }
	}
}
