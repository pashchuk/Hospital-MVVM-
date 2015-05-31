using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApp.Model;

namespace DesktopApp.ViewModel
{
	public class NoteViewModel : ViewModelBase
	{
		private Note _note;
		private bool _state;

		public string NoteText
		{
			get { return _note.NoteText; }
			set
			{
				_note.NoteText = value;
				OnPropertyChanged("NoteText");
			}
		}
		public string Author
		{
			get { return string.Format("{0} {1}",
				_note.Doctor.FirstName, _note.Doctor.LastName); }
		}
		public DateTime Date
		{
			get { return _note.Date; }
			set
			{
				_note.Date = value;
				OnPropertyChanged("Date");
			}
		}

		public bool State
		{
			get { return _state; }
			set
			{
				_state = value;
				OnPropertyChanged("Sate");
			}
		}

		public void ChangeStateToModify()
		{
			State = true;
		}

		public void Cancel()
		{
			State = false;
		}

		public void SaveChanges()
		{
			State = false;
			Date = DateTime.Now;
			HospitalContext.GetContext().SaveChanges();
		}

		public NoteViewModel(Note note)
		{
			_note = note;
			State = false;
		}

		public Note GetNote()
		{
			return _note;
		}
	}
}
