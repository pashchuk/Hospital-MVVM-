using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopApp.Commands;
using DesktopApp.Model;
using DesktopApp.View;

namespace DesktopApp.ViewModel
{
	public class WorkWindowViewModel : ViewModelBase
	{
		public WorkWindowViewModel()
		{
			InitCommands();
			using (var db = new HospitalContext())
			{
//				db.Cards.Load();
//				
//
//				var patient1 = new Patient()
//				{
//					Address = "Kyiv, Ukraine",
//					Age = 20,
//					Email = "as@asd.asd",
//					FirstName = "Левицький",
//					LastName = "Віталій",
//					MiddleName = "Андрійович",
//					Sex = "Male",
//					Phone = "0961234567",
//					Password = "1111"
//				};
//				var patient2 = new Patient()
//				{
//					Address = "Kyiv, Ukraine",
//					Age = 20,
//					Email = "as@asd.asd",
//					FirstName = "Лddfdий",
//					LastName = "dfdf",
//					MiddleName = "Аdfdfч",
//					Sex = "Male",
//					Phone = "0961234567",
//					Password = "1111"
//				};
//				var doc = new Doctor()
//				{
//					Address = "Kyiv, Ukraine",
//					Age = 20,
//					Email = "as@asd.asd",
//					FirstName = "Попов",
//					LastName = "Іван",
//					MiddleName = "Іванович",
//					Sex = "Male",
//					Phone = "0961234567",
//					Password = "1111",
//					Office = "travmatolog"
//				};
//				var card = new Card() {Patient = patient1, Name = "Temp"};
//				var card2 = new Card() { Patient = patient2, Name = "Temp2"};
//				doc.Cards.Add(card);
//				doc.Cards.Add(card2);
//				db.Cards.Add(card);
//				db.Cards.Add(card2);
//				db.Doctors.Add(doc);
//				db.SaveChanges();
//
				db.LoadAll();
				db.SaveChanges();

				CardsViews = new ObservableCollection<CardView>();
				foreach (var card in db.Cards)
				{
					CardsViews.Add(new CardView() {DataContext = new CardViewModel(card)});
				}
				_selectedCard = db.Cards.Local[0];
			}
		}

		#region Private Fields

		private ObservableCollection<CardView> _cardsViews;
		private FullCardViewModel _fullCardViewModel;
		private Card _selectedCard;

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

		#endregion

		#region Command Methods

		void InitCommands()
		{
			OpenFullCardCommand = new RelayCommand(OpenFullCardExecute, OpenFullCardCanExecute);
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
		

		
		
		
		
	}
}
