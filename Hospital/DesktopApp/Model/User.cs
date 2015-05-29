using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
	public abstract class User
	{
		[Required]
		public int Id { get; private set; }
		public int Age { get; set; }
		public string Sex { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone { get; set; }
	}

	public class Patient : User
	{
		public virtual Card Card { get; set; }
	}
}
