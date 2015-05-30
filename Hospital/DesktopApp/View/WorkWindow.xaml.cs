using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DesktopApp.Model;
using DesktopApp.ViewModel;

namespace DesktopApp.View
{
	/// <summary>
	/// Interaction logic for WorkWindow.xaml
	/// </summary>
	public partial class WorkWindow : Window
	{
		public WorkWindow()
		{
			var user = HospitalContext.GetContext().Doctors.First();
			if (user != null)
				this.DataContext = WorkWindowViewModel.Login(user);
			InitializeComponent();
		}
	}
}
