using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
	public class Card
	{
		#region Properties

		public int CardId { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		 

		#endregion

		#region Navigation Properties

		public User Patient { get; set; }
		public List<User> CanView { get; set; }

		#endregion
	}
}
