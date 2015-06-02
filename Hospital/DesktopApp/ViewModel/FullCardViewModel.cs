using System;
using System.Collections;
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
		internal enum ChangesState
		{
			CardCreated,
			CardChanged,
			Synchronized,
			NoteChanged,
			SessionChanged,
			NoteCreated,
			SessionCreated
		};
		private Card _card;
		private bool _state = false;
		private ChangesState _changes = ChangesState.Synchronized;
		private Visibility _dataVisibility = Visibility.Hidden;

		private ObservableCollection<NoteView> _noteViews;
		private ObservableCollection<SessionView> _sessionViews;

		private Note _selectedNote, _modifiedNote;
		private NoteView _selectedNoteView, _prevSelectedNoteView, _modifiedNoteView;
		private Session _selectedSession, _modifiedSession;
		private SessionView _selectedSessionView, _prevSelectedSessionView, _modifiedSessionView;

		#region Public Properties
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
		public ObservableCollection<SessionView> SessionViews
		{
			get { return _sessionViews; }
			set
			{
				_sessionViews = value;
				OnPropertyChanged("SessionViews");
			}
		}
		public RelayCommand ModifyCardCommand { get; private set; }
		public RelayCommand DeleteCardCommand { get; private set; }
		public RelayCommand AddNewCardCommand { get; private set; }
		public RelayCommand ModifySessionCommand { get; private set; }
		public RelayCommand DeleteSessionCommand { get; private set; }
		public RelayCommand AddNewSessionCommand { get; private set; }
		public RelayCommand ModifyNoteCommand { get; private set; }
		public RelayCommand DeleteNoteCommand { get; private set; }
		public RelayCommand AddNewNoteCommand { get; private set; }
		public RelayCommand SaveCommand { get; private set; }
		public RelayCommand CancelCommand { get; private set; }

		#endregion

		#region Commands

		void InitCommands()
		{
			ModifyCardCommand = new RelayCommand(ModifyCardExecute, ModifyCardCanExecute);
			DeleteCardCommand = new RelayCommand(DeleteCardExecute, DeleteCardCanExecute);
			AddNewCardCommand = new RelayCommand(AddNewCardExecute, AddNewCardCanExecute);
			ModifySessionCommand = new RelayCommand(ModifySessionExecute, ModifySessionCanExecute);
			DeleteSessionCommand = new RelayCommand(DeleteSessionExecute, DeleteSessionCanExecute);
			AddNewSessionCommand = new RelayCommand(AddNewSessionExecute, AddNewSessionCanExecute);
			ModifyNoteCommand = new RelayCommand(ModifyNoteExecute, ModifyNoteCanExecute);
			DeleteNoteCommand = new RelayCommand(DeleteNoteExecute, DeleteNoteCanExecute);
			AddNewNoteCommand = new RelayCommand(AddNewNoteExecute, AddNewNoteCanExecute);
			SaveCommand = new RelayCommand(SaveExecute, SaveCanExecute);
			CancelCommand = new RelayCommand(CancelExecute, CancelCanExecute);
		}

		#region Card commands

		bool ModifyCardCanExecute()
		{
			return _changes == ChangesState.Synchronized && !State;
		}
		void ModifyCardExecute()
		{
			State = true;
			_changes = ChangesState.CardChanged;
		}
		bool DeleteCardCanExecute()
		{
			return _changes == ChangesState.Synchronized 
				&& !State
				&& WorkWindowViewModel.GetViewModel()
				.DeleteCardCommand.CanExecute(null);
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
		private bool ModifyNoteCanExecute()
		{
			return _selectedNote != null
				&& _selectedNoteView != null
				&& _changes == ChangesState.Synchronized;
		}
		private void ModifyNoteExecute()
		{
			_modifiedNoteView = _selectedNoteView;
			_modifiedNote = _selectedNote;
			(_modifiedNoteView.DataContext as NoteViewModel).ChangeStateToModify();
			_changes = ChangesState.NoteChanged;
		}
		private bool DeleteNoteCanExecute()
		{
			return _selectedNote != null
				&& _selectedNoteView != null
				&& _changes == ChangesState.Synchronized;
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
			return _selectedSession != null
				&& _selectedSessionView!= null
				&& _changes == ChangesState.Synchronized;
		}
		private void AddNewNoteExecute()
		{
			var newsNote = new Note()
			{
				Doctor = WorkWindowViewModel.GetViewModel().LoginedUser as Doctor,
				Session = _selectedSession
			};
			var view = new NoteView() {DataContext = new NoteViewModel(newsNote)};
			ConfigureAnimation(view);
			_modifiedNoteView = view;
			_modifiedNote = newsNote;
			_selectedNote = newsNote;
			_prevSelectedNoteView = _selectedNoteView;
			_selectedNoteView = view;
			view.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(0x7f, 0x9f, 0x9f));
			if (_prevSelectedNoteView != null && !ReferenceEquals(_prevSelectedNoteView, _selectedNoteView))
			{
				_prevSelectedNoteView.Rectangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);
				_prevSelectedNoteView.MouseEnter += noteView_MouseEnter;
				_prevSelectedNoteView.MouseLeave += noteView_MouseLeave;
			}
			(view.DataContext as NoteViewModel).ChangeStateToModify();
			NoteViews.Insert(0, view);
			_changes = ChangesState.NoteCreated;
		}

		void InitNotes()
		{
			if (_selectedSession == null) return;
			var colection = new ObservableCollection<NoteView>();
			var notes = (from item in _selectedSession.Notes
						 orderby item.Date descending 
						 select item).Take(30);
			foreach (var note in notes)
			{
				var view = new NoteView() { DataContext = new NoteViewModel(note){} };
				ConfigureAnimation(view);
				colection.Add(view);
			}
			_selectedNoteView = null;
			_selectedNote = null;
			NoteViews = colection;
		}

		#endregion

		#region Session Commands
		private bool ModifySessionCanExecute()
		{
			return _selectedSession != null 
				&& _selectedSessionView != null 
				&& _changes == ChangesState.Synchronized;
		}
		private void ModifySessionExecute()
		{
			_modifiedSessionView = _selectedSessionView;
			_modifiedSession = _selectedSession;
			(_modifiedSessionView.DataContext as SessionViewModel).ChangeStateToModify();
			_changes = ChangesState.SessionChanged;
		}
		private bool DeleteSessionCanExecute()
		{
			return _selectedSession != null
				&& _selectedSessionView != null
				&& _changes == ChangesState.Synchronized;
		}
		private void DeleteSessionExecute()
		{
			if (MessageBox.Show("Are you realy want to delete this Session?", "Warning",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
				return;
			SessionViews.Remove(_selectedSessionView);
			HospitalContext.GetContext().Sessions.Remove(_selectedSession);
			if (SessionViews.Count != 0)
			{
				_selectedSessionView= SessionViews[0];
				_selectedSession = (_selectedSessionView.DataContext as SessionViewModel).GetSession();
			}
			HospitalContext.GetContext().SaveChangesAsync();
		}
		private bool AddNewSessionCanExecute()
		{
			return _changes == ChangesState.Synchronized;
		}
		private void AddNewSessionExecute()
		{
			var newsession = new Session() {Card = _card, Diagnosis = new Diagnosis()};
			var view = new SessionView() {DataContext = new SessionViewModel(newsession)};
			ConfigureAnimation(view);
			_modifiedSessionView = view;
			_modifiedSession = newsession;
			_selectedSession = newsession;
			_prevSelectedSessionView = _selectedSessionView;
			_selectedSessionView = view;
			view.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(0x7f, 0x9f, 0x9f));
			if (_prevSelectedSessionView != null && !ReferenceEquals(_prevSelectedSessionView, _selectedSessionView))
			{
				_prevSelectedSessionView.Rectangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);
				_prevSelectedSessionView.MouseEnter += sessionView_MouseEnter;
				_prevSelectedSessionView.MouseLeave += sessionView_MouseLeave;
			}
			(view.DataContext as SessionViewModel).ChangeStateToModify();
			SessionViews.Insert(0, view);
			InitNotes();
			_changes = ChangesState.SessionCreated;
		}

		void InitSession()
		{
			var colection = new ObservableCollection<SessionView>();
			var sessions = (from item in _card.Sesions
						 orderby item.Date descending
						 select item).Take(10);
			int counter = sessions.Count();
			if (counter > 0) _selectedSession = sessions.First();
			foreach (var session in sessions)
			{
				var view = new SessionView() {DataContext = new SessionViewModel(session, counter--)};
				ConfigureAnimation(view);
				colection.Add(view);
			}
			SessionViews = colection;
		}

		#endregion

		#region Other Commands

		private bool SaveCanExecute()
		{
			return _changes != ChangesState.Synchronized;
		}
		private void SaveExecute()
		{
			switch (_changes)
			{
				case ChangesState.SessionCreated:
					(_modifiedSessionView.DataContext as SessionViewModel).SaveChanges();
					HospitalContext.GetContext().Sessions.Add(_modifiedSession);
					ResetChanges();
					break;
				case ChangesState.SessionChanged:
					(_modifiedSessionView.DataContext as SessionViewModel).SaveChanges();
					ResetChanges();
					break;
				case ChangesState.NoteCreated:
					(_selectedNoteView.DataContext as NoteViewModel).SaveChanges();
					HospitalContext.GetContext().Notes.Add(_modifiedNote);
					ResetChanges();
					break;
				case ChangesState.NoteChanged:
					(_selectedNoteView.DataContext as NoteViewModel).SaveChanges();
					ResetChanges();
					break;
				case ChangesState.CardCreated:
					HospitalContext.GetContext().Cards.Add(_card);
					WorkWindowViewModel.GetViewModel().SaveChangesCommand.Execute(null);
					State = false;
					ResetChanges();
					break;
				case ChangesState.CardChanged:
					WorkWindowViewModel.GetViewModel().SaveChangesCommand.Execute(null);
					State = false;
					ResetChanges();
					break;
			}
			HospitalContext.GetContext().SaveChangesAsync();
		}
		private bool CancelCanExecute()
		{
			return _changes != ChangesState.Synchronized;
		}
		private void CancelExecute()
		{
			switch (_changes)
			{
				case ChangesState.SessionCreated:
					(_modifiedSessionView.DataContext as SessionViewModel).Cancel();
					SessionViews.Remove(_modifiedSessionView);
					_modifiedSessionView = null;
					ResetChanges();
					break;
				case ChangesState.SessionChanged:
					(_modifiedSessionView.DataContext as SessionViewModel).Cancel();
					ResetChanges();
					break;
				case ChangesState.NoteCreated:
					(_modifiedNoteView.DataContext as NoteViewModel).Cancel();
					NoteViews.Remove(_modifiedNoteView);
					ResetChanges();
					break;
				case ChangesState.NoteChanged:
					(_modifiedNoteView.DataContext as NoteViewModel).Cancel();
					ResetChanges();
					break;
				case ChangesState.CardCreated:
//					WorkWindowViewModel.GetViewModel().
					State = false;
					ResetChanges();
					break;
			}
			HospitalContext.GetContext().SaveChangesAsync();
		}
		void ResetChanges()
		{
			_changes = ChangesState.Synchronized;
		}

		#endregion


		#endregion

		#region Animation
		private void ConfigureAnimation(NoteView view)
		{
			view.MouseEnter += noteView_MouseEnter;
			view.MouseLeave += noteView_MouseLeave;
			view.MouseLeftButtonDown += noteView_MouseLeftButtonDown;
		}
		private void ConfigureAnimation(SessionView view)
		{
			view.MouseEnter += sessionView_MouseEnter;
			view.MouseLeave += sessionView_MouseLeave;
			view.MouseLeftButtonDown += sessionView_MouseLeftButtonDown;
		}

		void sessionView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var view = sender as SessionView;
			_prevSelectedSessionView = _selectedSessionView;
			_selectedSessionView = view;
			view.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(0x7f, 0x9f, 0x9f));
			view.MouseLeave -= sessionView_MouseLeave;
			view.MouseEnter -= sessionView_MouseEnter;
			if (_prevSelectedSessionView != null && !ReferenceEquals(_prevSelectedSessionView, _selectedSessionView))
			{
				_prevSelectedSessionView.Rectangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);
				_prevSelectedSessionView.MouseEnter += sessionView_MouseEnter;
				_prevSelectedSessionView.MouseLeave += sessionView_MouseLeave;
			}
			_selectedSession = (view.DataContext as SessionViewModel).GetSession();
			InitNotes();
		}
		void sessionView_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			var view = sender as SessionView;
			view.Rectangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);
		}
		void sessionView_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			var view = sender as SessionView;
			view.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(0x4f, 0x6f, 0x6f));
		}

		void noteView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var view = sender as NoteView;
			_prevSelectedNoteView = _selectedNoteView;
			_selectedNoteView = view;
			view.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(0x7f, 0x9f, 0x9f));
			view.MouseLeave -= noteView_MouseLeave;
			view.MouseEnter -= noteView_MouseEnter;
			if (_prevSelectedNoteView != null && !ReferenceEquals(_prevSelectedNoteView, _selectedNoteView))
			{
				_prevSelectedNoteView.Rectangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);
				_prevSelectedNoteView.MouseEnter += noteView_MouseEnter;
				_prevSelectedNoteView.MouseLeave += noteView_MouseLeave;
			}
			_selectedNote = (view.DataContext as NoteViewModel).GetNote();
		}
		void noteView_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			var view = sender as NoteView;
			view.Rectangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);
		}
		void noteView_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			var view = sender as NoteView;
			view.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(0x4f, 0x6f, 0x6f));
		}

		#endregion
		public FullCardViewModel(Card card, bool created = false)
		{
			_changes = created ? ChangesState.CardCreated : ChangesState.Synchronized;
			State = created;
			_card = card;
			_dataVisibility = Visibility.Visible;
			InitCommands();
			InitSession();
			InitNotes();
		}
	}
}
