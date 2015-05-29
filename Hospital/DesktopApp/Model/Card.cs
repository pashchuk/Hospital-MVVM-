using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
		public virtual User Patient { get; set; }
		public virtual Doctor OwnerDoctor { get; set; }

		#endregion

		public Card()
		{
			CreationDate = DateTime.Now;
		}
	}
}
