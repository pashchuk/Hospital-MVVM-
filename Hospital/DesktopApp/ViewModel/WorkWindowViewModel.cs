using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Security.Policy;
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

		public static WorkWindowViewModel Login(User loginUser)
		{
			_viewmodel = new WorkWindowViewModel(loginUser);
			return _viewmodel;
		}

		private WorkWindowViewModel():this(null)
		{
		}

		private WorkWindowViewModel(User loginUser)
		{
			if (loginUser != null)
				_loginedUser = loginUser;
			db.GetContext().LoadAll();

			InitAll();
		}

		#region Private Fields

		private ObservableCollection<CardView> _cardsViews;
		
		private FullCardViewModel _fullCardViewModel;

		private Card _selectedCard, _modifiedCard;
		private CardView _selectedCardView, _prevSelectedCardView, _modifiedCardCiew;

		private User _loginedUser;

		#endregion

		#region Public Properties

		public string UserType
		{
			get { return (_loginedUser is Doctor) ? "Doctor" : "Patient"; }
		}
		public User LoginedUser { get { return _loginedUser; } }
		public string UserName
		{
			get { return string.Format("{0} {1}", 
				_loginedUser.FirstName, _loginedUser.LastName); }
		}

		
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
			return true;
		}
		private void SaveChangesExecute()
		{
			(_selectedCardView.DataContext as CardViewModel).UpdateValues();
		}

		#region Card Commands

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
			var card = new Card()
			{
				OwnerDoctor = LoginedUser as Doctor, 
				Name = "test", 
				Patient = new Patient()
			};
			var cardView = new CardView() { DataContext = new CardViewModel(card) };
			ConfigureAnimation(cardView);
			_selectedCard = card;
			_selectedCardView = cardView;
			CardsViews.Add(cardView);
			OpenFullCardExecute(true);
		}
		private bool OpenFullCardCanExecute()
		{
			return _selectedCard != null;
		}
		private void OpenFullCardExecute()
		{
			FullCardViewModel = new FullCardViewModel(_selectedCard);
		}
		private void OpenFullCardExecute(bool status)
		{
			FullCardViewModel = new FullCardViewModel(_selectedCard, status);
		}

		#endregion


		#endregion

		#region Private Methods

		void InitAll()
		{
			InitCommands();
			InitCards();
		}

		void InitCards()
		{
			var colection = new ObservableCollection<CardView>();
			var cards = (from item in HospitalContext.GetContext().Cards
				orderby item.CreationDate descending
				select item).Take(10);
			if (cards.Count() > 0) _selectedCard = cards.First();
			foreach (var card in cards)
			{
				var view = new CardView() {DataContext = new CardViewModel(card)};
				ConfigureAnimation(view);
				colection.Add(view);
			}
			CardsViews= colection;
			OpenFullCardCommand.Execute(null);
		}


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
			if (_prevSelectedCardView != null && !ReferenceEquals(_prevSelectedCardView,_selectedCardView))
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
