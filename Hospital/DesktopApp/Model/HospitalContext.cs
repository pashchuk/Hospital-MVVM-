using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
	public class HospitalContext : DbContext
	{
		public DbSet<Card> Cards { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Note> Notes { get; set; }
	}
}
