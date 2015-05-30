using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
	public class Doctor : User
	{
		public string Office { get; set; }
		public virtual List<Card> Cards { get; private set; }

		public Doctor()
		{
			Cards = new List<Card>();
		}
	}
}
