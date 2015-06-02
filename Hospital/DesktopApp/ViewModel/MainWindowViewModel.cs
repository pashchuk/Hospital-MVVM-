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
		private string _username, _password;

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
