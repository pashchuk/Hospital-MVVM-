using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
	public class Card
	{
		#region Properties
		public int Id { get; private set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; private set; }
		 

		#endregion

		#region Navigation Properties
		[Required]
		public virtual Patient Patient { get; set; }
		public virtual Doctor OwnerDoctor { get; set; }
		public virtual List<Session> Sesions { get; private set; }

		#endregion

		public Card()
		{
			Sesions = new List<Session>();
			CreationDate = DateTime.Now;
		}
	}
}
