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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApp.View
{
	/// <summary>
	/// Interaction logic for CardView.xaml
	/// </summary>
	public partial class CardView : UserControl
	{
		public CardView()
		{
			InitializeComponent();
		}

		public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register(
			"UserName", typeof (string), typeof (CardView), new PropertyMetadata(default(string)));

		public string UserName
		{
			get { return (string) GetValue(UserNameProperty); }
			set { SetValue(UserNameProperty, value); }
		}

		public static readonly DependencyProperty UserSexProperty = DependencyProperty.Register(
			"UserSex", typeof (string), typeof (CardView), new PropertyMetadata(default(string)));

		public string UserSex
		{
			get { return (string) GetValue(UserSexProperty); }
			set { SetValue(UserSexProperty, value); }
		}

		public static readonly DependencyProperty UserAgeProperty = DependencyProperty.Register(
			"UserAge", typeof (int), typeof (CardView), new PropertyMetadata(default(int)));

		public int UserAge
		{
			get { return (int) GetValue(UserAgeProperty); }
			set { SetValue(UserAgeProperty, value); }
		}
	}
}
