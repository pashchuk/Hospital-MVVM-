using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{

	[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
	public class HospitalContext : DbContext
	{
		public HospitalContext()
			: base("HospitalContext")
		{
			this.Configuration.ValidateOnSaveEnabled = false;
		}

		static HospitalContext()
		{
			DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Card> Cards { get; set; }
		public DbSet<Patient> Patients{ get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<Session> Sessions { get; set; }
		public DbSet<Diagnosis> Diagnoses { get; set; }

		public void LoadAll()
		{
			this.Cards.Load();
			this.Patients.Load();
			this.Doctors.Load();
			this.Notes.Load();
			this.Sessions.Load();
			this.Diagnoses.Load();
		}
	}
}
