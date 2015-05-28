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
			var model = new WorkWindowViewModel() {UserName = "Pashchuk Eduard Fedorovich"};
			this.DataContext = model;
			InitializeComponent();
		}
	}
}
