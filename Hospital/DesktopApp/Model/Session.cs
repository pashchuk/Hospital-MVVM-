using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
	public class Session : ICloneable
	{
		public int Id { get; private set; }
		public DateTime Date { get; private set; }

		public virtual Card Card { get; set; }

		public virtual List<Note> Notes { get; private set; } 
		public virtual Diagnosis Diagnosis { get; set; }

		public Session()
		{
			Date = DateTime.Now;
			Notes = new List<Note>();
		}
		public object Clone()
		{
			var obj = MemberwiseClone() as Session;
			obj.Diagnosis = Diagnosis.Clone() as Diagnosis;
			return obj;
		}
	}
}
