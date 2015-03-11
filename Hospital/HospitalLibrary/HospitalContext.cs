using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary
{
	public class HospitalContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<Card> Cards { get; set; }
		public DbSet<Session> Sessions { get; set; }
		public DbSet<Diagnosis> Diagnoses { get; set; }
	}
}
