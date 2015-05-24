﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Model
{
	public class Note
	{
		#region Properties

		public int NoteId { get; set; }
		public string NoteText { get; set; }
		public DateTime Date { get; set; }
 

		#endregion

		#region Navigation Properties

		public virtual Doctor Doctor { get; set; }
		public virtual Card Card { get; set; }

		#endregion
	}
	
}
