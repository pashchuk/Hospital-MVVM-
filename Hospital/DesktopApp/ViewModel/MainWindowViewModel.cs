using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesktopApp.Commands;
using DesktopApp.Model;
using DesktopApp.View;
using Application = System.Windows.Application;

namespace DesktopApp.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private string _username, _password,
			_regPwd, _confPwd;
		private Doctor _doc;

		public string UserName
		{
			get { return _username; }
			set
			{
				_username = value;
				OnPropertyChanged("UserName");
			}
		}

		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				OnPropertyChanged("Password");
			}
		}

		#region Register Property

		public string Login
		{
			get { return _doc.Login; }
			set
			{
				_doc.Login = value;
				OnPropertyChanged("Login");
			}
		}
		public string Email
		{
			get { return _doc.Email; }
			set
			{
				_doc.Email = value;
				OnPropertyChanged("Email");
			}
		}
		public string RegPassword
		{
			get { return _regPwd; }
			set
			{
				_regPwd = value;
				OnPropertyChanged("RegPassword");
			}
		}
		public string ConfirmPassword
		{
			get { return _confPwd; }
			set
			{
				_confPwd = value;
				OnPropertyChanged("ConfirmPassword");
				if (string.Equals(_regPwd, _confPwd))
					_doc.Password = value;
			}
		}
		public string FirstName
		{
			get { return _doc.FirstName; }
			set
			{
				_doc.FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}
		public string LastName
		{
			get { return _doc.LastName; }
			set
			{
				_doc.LastName = value;
				OnPropertyChanged("LastName");
			}
		}
		public string MiddleName
		{
			get { return _doc.MiddleName; }
			set
			{
				_doc.MiddleName = value;
				OnPropertyChanged("MiddleName");
			}
		}
		public int Age
		{
			get { return _doc.Age; }
			set { _doc.Age = value; }
		}
		public string Sex
		{
			get { return _doc.Sex; }
			set
			{
				_doc.Sex = value;
				OnPropertyChanged("Sex");
			}
		}
		public string Address
		{
			get { return _doc.Address; }
			set
			{
				_doc.Address = value;
				OnPropertyChanged("Address");
			}
		}
		public string Office
		{
			get { return _doc.Office; }
			set
			{
				_doc.Office = value;
				OnPropertyChanged("Office");
			}
		}
		public string Phone
		{
			get { return _doc.Phone; }
			set
			{
				_doc.Phone = value;
				OnPropertyChanged("Phone");
			}
		}

		#endregion

		bool CheckPwds()
		{
			return string.Equals(_regPwd, _confPwd);
		}

		public RelayCommand LogInCommand { get; private set; }
		public RelayCommand RegisterCommand { get; private set; }
		public RelayCommand CancelCommand { get; private set; }

		public MainWindowViewModel()
		{
			LogInCommand = new RelayCommand(LoginExecute, LoginCanExecute);
			_username = string.Empty;
			_password = string.Empty;
		}

		private bool LoginCanExecute()
		{
			return !string.IsNullOrWhiteSpace(UserName)
			       && !string.IsNullOrWhiteSpace(Password);
		}

		private async void LoginExecute()
		{
			try
			{
				var db = HospitalContext.GetContext();
				var user = await Task.Run(() =>
					(from a in db.Doctors
						where a.Login == UserName
						      && a.Password == Password
						select a).First());
				if (user == null)
				{
					MessageBox.Show("Incorrect username or password", "Login failed", MessageBoxButtons.OK);
					return;
				}
				var newWindow = new WorkWindow();
				newWindow.DataContext = WorkWindowViewModel.Login(user);
				newWindow.Show();
				Application.Current.MainWindow.Hide();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
