using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DesktopApp.Commands;
using DesktopApp.Model;

namespace DesktopApp.ViewModel
{
	public class FullCardViewModel : ViewModelBase
	{
		private Card _card;
		private bool _state = false;
		private Visibility _dataVisibility = Visibility.Hidden;

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
		public RelayCommand ModifyCardCommand { get; private set; }
		public RelayCommand DeleteCardCommand { get; private set; }
		public RelayCommand AddNewCardCommand { get; private set; }

		#region Commands

		void InitCommands()
		{
			ModifyCardCommand = new RelayCommand(ModifyCardExecute, ModifyCardCanExecute);
			DeleteCardCommand = new RelayCommand(DeleteCardExecute, DeleteCardCanExecute);
			AddNewCardCommand = new RelayCommand(AddNewCardExecute, AddNewCardCanExecute);
		}

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



		public FullCardViewModel(Card card)
		{
			InitCommands();
			_card = card;
			_dataVisibility = Visibility.Visible;
		}
	}
}
