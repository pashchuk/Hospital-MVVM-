using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using DesktopApp.Commands;
using DesktopApp.Model;
using DesktopApp.View;
using db = DesktopApp.Model.HospitalContext;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace DesktopApp.ViewModel
{
	public class WorkWindowViewModel : ViewModelBase
	{
		private static volatile WorkWindowViewModel _viewmodel;
		private static object _lock = new object();

		public static WorkWindowViewModel GetViewModel()
		{
			if(_viewmodel!=null) return _viewmodel;
			lock(_lock)
				if(_viewmodel==null)
					_viewmodel = new WorkWindowViewModel();
			return _viewmodel;
		}

		private WorkWindowViewModel()
		{
			InitCommands();
			var dbs = db.GetContext();

//			dbs.Cards.Load();
//
//
//			var patient1 = new Patient()
//			{
//				Address = "Kyiv, Ukraine",
//				Age = 20,
//				Email = "as@asd.asd",
//				FirstName = "Левицький",
//				LastName = "Віталій",
//				MiddleName = "Андрійович",
//				Sex = "Male",
//				Phone = "0961234567",
//				Password = "1111"
//			};
//			var patient2 = new Patient()
//			{
//				Address = "Kyiv, Ukraine",
//				Age = 20,
//				Email = "as@asd.asd",
//				FirstName = "Лddfdий",
//				LastName = "dfdf",
//				MiddleName = "Аdfdfч",
//				Sex = "Male",
//				Phone = "0961234567",
//				Password = "1111"
//			};
//			var doc = new Doctor()
//			{
//				Address = "Kyiv, Ukraine",
//				Age = 20,
//				Email = "as@asd.asd",
//				FirstName = "Попов",
//				LastName = "Іван",
//				MiddleName = "Іванович",
//				Sex = "Male",
//				Phone = "0961234567",
//				Password = "1111",
//				Office = "travmatolog"
//			};
//			var card = new Card() {Patient = patient1, Name = "Temp"};
//			var card2 = new Card() {Patient = patient2, Name = "Temp2"};
//			doc.Cards.Add(card);
//			doc.Cards.Add(card2);
//			dbs.Cards.Add(card);
//			dbs.Cards.Add(card2);
//			dbs.Doctors.Add(doc);
//			dbs.SaveChanges();
//
			dbs.LoadAll();
			dbs.SaveChanges();

			CardsViews = new ObservableCollection<CardView>();
			foreach (var card in dbs.Cards)
			{
				var view = new CardView() {DataContext = new CardViewModel(card)};
				ConfigureAnimation(view);
				CardsViews.Add(view);
			}
			_selectedCard = dbs.Cards.Local[0];
		}

		#region Private Fields

		private ObservableCollection<CardView> _cardsViews;
		private FullCardViewModel _fullCardViewModel;
		private Card _selectedCard;
		private CardView _selectedCardView, _prevSelectedCardView;

		#endregion

		#region Public Properties

		public string UserName { get; set; }
		public ObservableCollection<CardView> CardsViews
		{
			get { return _cardsViews; }
			set
			{
				_cardsViews = value;
				OnPropertyChanged("CardsViews");
			}
		}
		public FullCardViewModel FullCardViewModel
		{
			get { return _fullCardViewModel; }
			set
			{
				_fullCardViewModel = value;
				OnPropertyChanged("FullCardViewModel");
			}
		}

		public RelayCommand OpenFullCardCommand { get; private set; }
		public RelayCommand SaveChangesCommand { get; private set; }
		public RelayCommand DeleteCardCommand { get; private set; }
		public RelayCommand AddNewCardCommand { get; private set; }

		
		#endregion

		#region Command Methods

		void InitCommands()
		{
			OpenFullCardCommand = new RelayCommand(OpenFullCardExecute, OpenFullCardCanExecute);
			SaveChangesCommand = new RelayCommand(SaveChangesExecute, SaveChangesCanExecute);
			DeleteCardCommand = new RelayCommand(DeleteCardExecute, DeleteCardCanExecute);
			AddNewCardCommand = new RelayCommand(AddNewCardExecute, AddNewCardCanExecute);
		}

		private bool SaveChangesCanExecute()
		{
			throw new NotImplementedException();
		}
		private void SaveChangesExecute()
		{
			throw new NotImplementedException();
		}
		private bool DeleteCardCanExecute()
		{
			return _selectedCard != null && _selectedCardView != null;
		}
		private void DeleteCardExecute()
		{
			if (MessageBox.Show("Are you realy want to delete this card?", "Warning",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
				return;
			CardsViews.Remove(_selectedCardView);
			db.GetContext().Cards.Remove(_selectedCard);
			if (CardsViews.Count != 0)
			{
				_selectedCardView = CardsViews[0];
				_selectedCard = (_selectedCardView.DataContext as CardViewModel).GetCard();
				OpenFullCardCommand.Execute(null);
			}
			db.GetContext().SaveChangesAsync();
		}
		private bool AddNewCardCanExecute()
		{
			return true;
		}
		private void AddNewCardExecute()
		{
			var card = new Card();
			var efCard = db.GetContext().Cards.Add(card);
			var cardView = new CardView() {DataContext = new CardViewModel(card)};
			ConfigureAnimation(cardView);
			db.GetContext().SaveChangesAsync();
		}
		private bool OpenFullCardCanExecute()
		{
			return _selectedCard != null;
		}
		private void OpenFullCardExecute()
		{
			FullCardViewModel = new FullCardViewModel(_selectedCard);
		}

		#endregion

		#region Private Methods

		private void ConfigureAnimation(CardView view)
		{
			view.MouseEnter += view_MouseEnter;
			view.MouseLeave += view_MouseLeave;
			view.MouseLeftButtonDown += view_MouseLeftButtonDown;
		}
		void view_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			var view = sender as CardView;
			_prevSelectedCardView = _selectedCardView;
			_selectedCardView = view;
			view.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(0x7f, 0x9f, 0x9f));
			view.MouseLeave -= view_MouseLeave;
			view.MouseEnter -= view_MouseEnter;
			if (_prevSelectedCardView != null)
			{
				_prevSelectedCardView.Rectangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);
				_prevSelectedCardView.MouseEnter += view_MouseEnter;
				_prevSelectedCardView.MouseLeave += view_MouseLeave;
			}
			_selectedCard = (view.DataContext as CardViewModel).GetCard();
			OpenFullCardCommand.Execute(null);
		}
		void view_MouseLeave(object sender, MouseEventArgs e)
		{
			var view = sender as CardView;
			view.Rectangle.Fill = new SolidColorBrush(Colors.DarkSlateGray);
		}
		void view_MouseEnter(object sender, MouseEventArgs e)
		{
			var view = sender as CardView;
			view.Rectangle.Fill = new SolidColorBrush(Color.FromRgb(0x4f, 0x6f, 0x6f));
		}

		#endregion
		
	}
}
