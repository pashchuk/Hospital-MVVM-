using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using DesktopApp.Commands;
using DesktopApp.Model;
using DesktopApp.View;
using DevOne.Security.Cryptography.BCrypt;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DesktopApp.ViewModel
{
	public class MainWindowViewModel : ViewModelBase
	{
		private string _username;
		private Doctor _doc;
		private Visibility _loginVisibility, _registerVisibility;

		public string UserName
		{
			get { return _username; }
			set
			{
				_username = value;
				OnPropertyChanged("UserName");
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

		public Visibility LoginVisibility
		{
			get { return _loginVisibility; }
			set
			{
				_loginVisibility = value;
				OnPropertyChanged("LoginVisibility");
			}
		}
		public Visibility RegisterVisibility
		{
			get { return _registerVisibility; }
			set
			{
				_registerVisibility = value;
				OnPropertyChanged("RegisterVisibility");
			}
		}

		public RelayCommand LogInCommand { get; private set; }
		public RelayCommand RegisterNewClickCommand { get; private set; }
		public RelayCommand RegisterCommand { get; private set; }
		public RelayCommand CancelCommand { get; private set; }

		public MainWindowViewModel()
		{
			LogInCommand = new RelayCommand(LoginExecute, LoginCanExecute);
			RegisterNewClickCommand = new RelayCommand(RegisterNewClickExecute);
			RegisterCommand = new RelayCommand(RegisterExecute, RegisterCanExecute);
			CancelCommand = new RelayCommand(CancelExecute);
			_username = string.Empty;
			_doc = new Doctor();
			LoginVisibility = Visibility.Visible;
			RegisterVisibility = Visibility.Collapsed;
		}

		private bool LoginCanExecute()
		{
			return !string.IsNullOrWhiteSpace(UserName);
		}
		private async void LoginExecute(object passwordBox)
		{
			var box = passwordBox as PasswordBox;
			try
			{
				var db = HospitalContext.GetContext();
				var user = await Task.Run(() =>
				{
					var col = from a in db.Doctors
						where a.Login == UserName
						      && a.Password == box.Password
						select a;
					return col.Count() != 0 ? col.First() : null;
				});
				if (user == null)
				{
					MessageBox.Show("Incorrect username or password", "Login failed", MessageBoxButtons.OK);
					box.Password = string.Empty;
					UserName = string.Empty;
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
		private void RegisterNewClickExecute()
		{
			LoginVisibility = Visibility.Collapsed;
			RegisterVisibility = Visibility.Visible;
		}
		private bool RegisterCanExecute()
		{
			return true;
		}
		private void RegisterExecute(object values)
		{
			var val = (object[]) values;
			var pwd = val[0] as PasswordBox;
			var confPwd = val[1] as PasswordBox;
			if (!string.Equals(confPwd.Password, pwd.Password))
			{
				MessageBox.Show("Password is not equals", "Validation",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				pwd.Password = string.Empty;
				confPwd.Password = string.Empty;
				return;
			}
			if (!ValidateRegisterData()) return;
			_doc.Password = pwd.Password;
			try
			{
				HospitalContext.GetContext().Doctors.Add(_doc);
				HospitalContext.GetContext().SaveChanges();
				MessageBox.Show("User register succesfuly");
				CancelCommand.Execute(null);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}
		private void CancelExecute()
		{
			LoginVisibility = Visibility.Visible;
			RegisterVisibility = Visibility.Collapsed;
			FflushForm();
		}

		private bool ValidateRegisterData()
		{
			return true;
		}

		private void FflushForm()
		{
			Login = string.Empty;
			Email = string.Empty;
			FirstName = string.Empty;
			LastName = string.Empty;
			MiddleName = string.Empty;
			Age = 0;
			Sex = string.Empty;
			Address = string.Empty;
			Office = string.Empty;
			Phone = string.Empty;
		}
	}
}
