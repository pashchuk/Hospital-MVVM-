using System.Data.Entity;

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
