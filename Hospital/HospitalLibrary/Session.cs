namespace HospitalLibrary
{
	public class Session
	{
		#region Properties

		public int Id { get; set; }
		public bool Result { get; set; }

		#endregion

		#region Navigation Properties

		public virtual Card Card { get; set; }
		public virtual Diagnosis Diagnosis { get; set; }

		#endregion
	}
}
