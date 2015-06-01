using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using DesktopApp.Commands;
using DesktopApp.Model;
using DesktopApp.View;
using MessageBox = System.Windows.Forms.MessageBox;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace DesktopApp.ViewModel
{
	public class FullCardViewModel : ViewModelBase
	{
		private Card _card;
		private bool _state = false;
		private Visibility _dataVisibility = Visibility.Hidden;

		private ObservableCollection<NoteView> _noteViews; 

		private Note _selectedNote;
		private NoteView _selectedNoteView, _prevSelectedNoteView;

		public string DoctorName
		{
			get { return string.Format("{0} {1}",_card.OwnerDoctor.FirstName,_card.OwnerDoctor.LastName); }
		}
		public string FirstName
		{
			get { return _card.Patient.FirstName; }
			set
			{
				_card.Patient.FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}
		public string LastName
		{
			get { return _card.Patient.LastName; }
			set
			{
				_card.Patient.LastName = value;
				OnPropertyChanged("LastName");
			}
		}
		public string MiddleName
		{
			get { return _card.Patient.MiddleName; }
			set
			{
				_card.Patient.MiddleName = value;
				OnPropertyChanged("MiddleName");
			}
		}
		public int Age
		{
			get { return _card.Patient.Age; }
			set
			{
				_card.Patient.Age = value;
				OnPropertyChanged("Age");
			}
		}
		public string Address
		{
			get { return _card.Patient.Address; }
			set
			{
				_card.Patient.Address = value;
				OnPropertyChanged("Address");
			}
		}
		public string Email
		{
			get { return _card.Patient.Email; }
			set
			{
				_card.Patient.Email = value;
				OnPropertyChanged("Email");
			}
		}
		public string Sex
		{
			get { return _card.Patient.Sex; }
			set
			{
				_card.Patient.Sex = value;
				OnPropertyChanged("Sex");
			}
		}
		public string Phone
		{
			get { return _card.Patient.Phone; }
			set
			{
				_card.Patient.Phone = value;
				OnPropertyChanged("Phone");
			}
		}

		public bool State
		{
			get { return _state; }
			set
			{
				_state = value;
				OnPropertyChanged("State");
			}
		}
		public Visibility DataVisibility
		{
			get { return _dataVisibility;}
			set
			{
				_dataVisibility = value;
				OnPropertyChanged("DataVisibility");
			}
		}

		public ObservableCollection<NoteView> NoteViews
		{
			get { return _noteViews; }
			set
			{
				_noteViews = value;
				OnPropertyChanged("NoteViews");
			}
		}
		public RelayCommand ModifyCardCommand { get; private set; }
		public RelayCommand DeleteCardCommand { get; private set; }
		public RelayCommand AddNewCardCommand { get; private set; }
		public RelayCommand ModifyNoteCommand { get; private set; }
		public RelayCommand DeleteNoteCommand { get; private set; }
		public RelayCommand AddNewNoteCommand { get; private set; }

		#region Commands

		void InitCommands()
		{
			ModifyCardCommand = new RelayCommand(ModifyCardExecute, ModifyCardCanExecute);
			DeleteCardCommand = new RelayCommand(DeleteCardExecute, DeleteCardCanExecute);
			AddNewCardCommand = new RelayCommand(AddNewCardExecute, AddNewCardCanExecute);
		}

		#region Card commands

		bool ModifyCardCanExecute()
		{
			return !State;
		}
		void ModifyCardExecute()
		{
			State = true;
		}
		bool DeleteCardCanExecute()
		{
			return WorkWindowViewModel.GetViewModel().DeleteCardCommand.CanExecute(null);
		}
		void DeleteCardExecute()
		{
			WorkWindowViewModel.GetViewModel().DeleteCardCommand.Execute(null);
		}
		bool AddNewCardCanExecute()
		{
			return WorkWindowViewModel.GetViewModel().AddNewCardCommand.CanExecute(null);
		}
		void AddNewCardExecute()
		{
			WorkWindowViewModel.GetViewModel().AddNewCardCommand.Execute(null);
		}

		#endregion

		#region Note Commands

		private bool DeleteNoteCanExecute()
		{
			return _selectedNote != null && _selectedNoteView != null;
		}
		private void DeleteNoteExecute()
		{
			if (MessageBox.Show("Are you realy want to delete this Note?", "Warning",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
				return;
			NoteViews.Remove(_selectedNoteView);
			HospitalContext.GetContext().Notes.Remove(_selectedNote);
			if (NoteViews.Count != 0)
			{
				_selectedNoteView = NoteViews[0];
				_selectedNote = (_selectedNoteView.DataContext as NoteViewModel).GetNote();
			}
			HospitalContext.GetContext().SaveChangesAsync();
		}
		private bool AddNewNoteCanExecute()
		{
			return true;
		}
		private void AddNewNoteExecute()
		{
			var Note = HospitalContext.GetContext().Notes.Add(new Note());
			var NoteView = new NoteView() { DataContext = new NoteViewModel(Note) };
			ConfigureAnimation(NoteView);
			HospitalContext.GetContext().SaveChangesAsync();
			_selectedNote = Note;
			_selectedNote.Doctor = WorkWindowViewModel.GetViewModel().LoginedUser as Doctor;
			_selectedNoteView = NoteView;
			(_selectedNoteView.DataContext as NoteViewModel).ChangeStateToModify();
		}

		void InitNotes()
		{
			var colection = new ObservableCollection<NoteView>();
			var notes = (from item in _card.Notes
						 orderby item.Date descending 
						 select item).Take(30);
			foreach (var note in notes)
			{
				var view = new NoteView() { DataContext = new NoteViewModel(note) };
				ConfigureAnimation(view);
				colection.Add(view);
			}
			NoteViews = colection;
		}

		#endregion

		#endregion

		private void ConfigureAnimation(NoteView view)
		{

		}
//		void view_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//		{
//			var view = sender as CardView;
//			_prevSelectedCardView = _selectedCardView;
//			_selectedCardView = view;
//			view.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(0x7f, 0x9f, 0x9f));
//			view.MouseLeave -= view_MouseLeave;
//			view.MouseEnter -= view_MouseEnter;
//			if (_prevSelectedCardView != null)
//			{
//				_prevSelectedCardView.Rectangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);
//				_prevSelectedCardView.MouseEnter += view_MouseEnter;
//				_prevSelectedCardView.MouseLeave += view_MouseLeave;
//			}
//			_selectedCard = (view.DataContext as CardViewModel).GetCard();
//			OpenFullCardCommand.Execute(null);
//		}
//		void view_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
//		{
//			var view = sender as CardView;
//			view.Rectangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);
//		}
//		void view_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
//		{
//			var view = sender as CardView;
//			view.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(0x4f, 0x6f, 0x6f));
//		}

		public FullCardViewModel(Card card)
		{
			_card = card;
			_dataVisibility = Visibility.Visible;
			InitCommands();
			InitNotes();
		}
	}
}
