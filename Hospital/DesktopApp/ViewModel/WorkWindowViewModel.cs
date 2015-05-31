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

		private void GenerateTestDataBaseData()
		{
			var patient1 = new Patient()
			{
				Address = "Kyiv, Ukraine",
				Age = 20,
				Email = "as@asd.asd",
				FirstName = "Левицький",
				LastName = "Віталій",
				MiddleName = "Андрійович",
				Sex = "Чоловік",
				Phone = "0961234567",
				Password = "1111"
			};
			var patient2 = new Patient()
			{
				Address = "Kyiv, Ukraine",
				Age = 28,
				Email = "prochorov@gmail.com",
				FirstName = "Прохоров",
				LastName = "Дмитро",
				MiddleName = "Олександрович",
				Sex = "Чоловік",
				Phone = "0996689799",
				Password = "1111"
			};
			var patient3 = new Patient()
			{
				Address = "Lutsk, Ukraine",
				Age = 18,
				Email = "dima.dimasik@ukr.net",
				FirstName = "Киричок",
				LastName = "Дмитро",
				MiddleName = "Віталійович",
				Sex = "Чоловік",
				Phone = "0966548231",
				Password = "1111"
			};
			var patient4 = new Patient()
			{
				Address = "Moscow, Russia",
				Age = 22,
				Email = "sasha.sasha@yandex.ru",
				FirstName = "Зубарчук",
				LastName = "Олександра",
				MiddleName = "Сергіївна",
				Sex = "Жінка",
				Phone = "+70544253691",
				Password = "1111"
			};
			var patient5 = new Patient()
			{
				Address = "Kovel, Ukraine",
				Age = 23,
				Email = "sergiy777@gmail.com",
				FirstName = "Приходько",
				LastName = "Сергій",
				MiddleName = "Анатолійович",
				Sex = "Чоловік",
				Phone = "0961234567",
				Password = "1111"
			};
			var patient6 = new Patient()
			{
				Address = "London, United Kingdom",
				Age = 35,
				Email = "jack.johnson@gmail.com",
				FirstName = "Jack",
				LastName = "Jonson",
				MiddleName = "Daniale",
				Sex = "Чоловік",
				Phone = "0996689799",
				Password = "1111"
			};
			var doc1 = new Doctor()
			{
				Address = "Lutsk, Ukraine",
				Age = 30,
				Email = "alex.popov@rambler.ru",
				FirstName = "Попов",
				LastName = "Олександр",
				MiddleName = "Іванович",
				Sex = "Чоловік",
				Phone = "0961234567",
				Password = "1111",
				Office = "Травматолог"
			};
			var doc2 = new Doctor()
			{
				Address = "Kyiv, Ukraine",
				Age = 35,
				Email = "max.nevdash@ukr.net",
				FirstName = "Невдащенко",
				LastName = "Максим",
				MiddleName = "Валентинович",
				Sex = "Чоловік",
				Phone = "0991538549",
				Password = "1111",
				Office = "Хірург"
			};
			var doc3 = new Doctor()
			{
				Address = "Kyiv, Ukraine",
				Age = 26,
				Email = "eva.gav@ukr.net",
				FirstName = "Опанасенко",
				LastName = "Євгенія",
				MiddleName = "Едуардівна",
				Sex = "Жінка",
				Phone = "0984267826",
				Password = "1111",
				Office = "Акушер"
			};
			var doc4 = new Doctor()
			{
				Address = "Redmomd, California ,USA",
				Age = 48,
				Email = "haus@med.com",
				FirstName = "Доктор",
				LastName = "Хаус",
				MiddleName = "Лікарович",
				Sex = "Чоловік",
				Phone = "096-666-66-66",
				Password = "1111",
				Office = "Лікар-псих"
			};
			var diagnosis1 = new Diagnosis()
			{
				Description = "Перелом стегна"
			};
			var diagnosis2 = new Diagnosis()
			{
				Description = "Струс мозку"
			};
			var diagnosis3 = new Diagnosis()
			{
				Description = "Забій лівої кисті"
			};
			var diagnosis4 = new Diagnosis()
			{
				Description = "Якийсь непонятний нікому діагноз"
			};
			var diagnosis5 = new Diagnosis()
			{
				Description = "Тільки доктор хаус знає що це за діагноз"
			};
			var note1_1 = new Note()
			{
				NoteText = "Негайно зробити біопсію і МРТ, пробу шлука, печінки і всіх органів",
				Doctor = doc4
			};
			var note1_2 = new Note()
			{
				NoteText = "Негайно зробити біопсію і МРТ, пробу шлука, печінки і всіх органів",
				Doctor = doc4
			};
			var note1_3 = new Note()
			{
				NoteText = "Негайно зробити біопсію і МРТ, пробу шлука, печінки і всіх органів",
				Doctor = doc4
			};
			var note1_4 = new Note()
			{
				NoteText = "Негайно зробити біопсію і МРТ, пробу шлука, печінки і всіх органів",
				Doctor = doc4
			};
			var note1_5 = new Note()
			{
				NoteText = "Негайно зробити біопсію і МРТ, пробу шлука, печінки і всіх органів",
				Doctor = doc4
			};
			var note1_6 = new Note()
			{
				NoteText = "Негайно зробити біопсію і МРТ, пробу шлука, печінки і всіх органів",
				Doctor = doc4
			};
			var note2_1 = new Note()
			{
				NoteText = "До акушера скарг немає!",
				Doctor = doc3
			};
			var note2_2 = new Note()
			{
				NoteText = "Є скарги!",
				Doctor = doc3
			};
			var note2_3 = new Note()
			{
				NoteText = "Є скарги!",
				Doctor = doc3
			};
			var note2_4 = new Note()
			{
				NoteText = "До акушера скарг немає!",
				Doctor = doc3
			};
			var note2_5 = new Note()
			{
				NoteText = "До акушера скарг немає!",
				Doctor = doc3
			};
			var note2_6 = new Note()
			{
				NoteText = "Є скарги!",
				Doctor = doc3
			};
			var note3_1 = new Note()
			{
				NoteText = "У хірурга всьо ок!",
				Doctor = doc2
			};
			var note3_2 = new Note()
			{
				NoteText = "Є підозра на апендицит!",
				Doctor = doc2
			};
			var note3_3 = new Note()
			{
				NoteText = "Проблеми із дихальними шляхами у носі пацієнта.",
				Doctor = doc2
			};
			var note3_4 = new Note()
			{
				NoteText = "Ця жінка хоче збільшити собі сіські! Буде проведена операція по збільшенню сісьок до 4 розміру!",
				Doctor = doc2
			};
			var note3_5 = new Note()
			{
				NoteText = "Є підозра на апендицит!",
				Doctor = doc2
			};
			var note3_6 = new Note()
			{
				NoteText = "Проблеми із дихальними шляхами у носі пацієнта.",
				Doctor = doc2
			};
			var note4_1 = new Note()
			{
				NoteText = "Посттравматична кукса лівої руки із попередньою ампутацією чотирьох пальців на рівні 2 суглобу!",
				Doctor = doc1
			};
			var note4_2 = new Note()
			{
				NoteText = "Цей штріх живий і здоровий як бик. йому тільки мішки з цементом на вокзалі розгружати!",
				Doctor = doc1
			};
			var note4_3 = new Note()
			{
				NoteText = "Посттравматична кукса лівої руки із попередньою ампутацією чотирьох пальців на рівні 2 суглобу!",
				Doctor = doc1
			};
			var note4_4 = new Note()
			{
				NoteText = "Відносно сильна біль у ногах.",
				Doctor = doc1
			};
			var note4_5 = new Note()
			{
				NoteText = "Перелом лівої руки",
				Doctor = doc1
			};
			var note4_6 = new Note()
			{
				NoteText = "Гематома на рівні переносиці! результат забою.",
				Doctor = doc1
			};
			var card1 = new Card()
			{
				Patient = patient1,
				Name = "Card1",
				OwnerDoctor = doc1,
			};
			card1.Notes.AddRange(new List<Note>() {note2_1, note1_1, note4_1, note3_1});
			card1.Sesions.Add(new Session(){Diagnosis = diagnosis1});
			card1.Sesions.Add(new Session() {Diagnosis = diagnosis2});
			var card2 = new Card()
			{
				Patient = patient2,
				Name = "Card2",
				OwnerDoctor = doc2,
			};
			card2.Notes.AddRange(new List<Note>() { note1_2, note2_2, note3_2, note4_2 });
			card2.Sesions.Add(new Session() { Diagnosis = diagnosis2 });
			card2.Sesions.Add(new Session() { Diagnosis = diagnosis5 });
			var card3 = new Card()
			{
				Patient = patient3,
				Name = "Card3",
				OwnerDoctor = doc3,
			};
			card3.Notes.AddRange(new List<Note>() { note3_3, note1_3, note2_3, note4_3 });
			card3.Sesions.Add(new Session() { Diagnosis = diagnosis3 });
			var card4 = new Card()
			{
				Patient = patient4,
				Name = "Card4",
				OwnerDoctor = doc4,
			};
			card4.Notes.AddRange(new List<Note>() { note4_4, note3_4, note2_4, note1_4 });
			card4.Sesions.Add(new Session() { Diagnosis = diagnosis4 });
			card4.Sesions.Add(new Session() { Diagnosis = diagnosis2 });
			var card5 = new Card()
			{
				Patient = patient5,
				Name = "Card5",
				OwnerDoctor = doc1,
			};
			card5.Notes.AddRange(new List<Note>() { note2_5, note1_5, note3_5, note4_5 });
			card5.Sesions.Add(new Session() { Diagnosis = diagnosis4 });
			card5.Sesions.Add(new Session() { Diagnosis = diagnosis1 });
			var card6 = new Card()
			{
				Patient = patient6,
				Name = "Card6",
				OwnerDoctor = doc2,
			};
			card6.Notes.AddRange(new List<Note>() { note2_6, note1_6, note4_6, note3_6 });
			card6.Sesions.Add(new Session() { Diagnosis = diagnosis5 });
			card6.Sesions.Add(new Session() { Diagnosis = diagnosis4 });
			db.GetContext().Cards.AddRange(new List<Card>() {card1, card2, card3, card4, card5, card6});
			db.GetContext().SaveChangesAsync();
		}

		private WorkWindowViewModel(User loginUser)
		{
			if (loginUser != null)
				_loginedUser = loginUser;
//			GenerateTestDataBaseData();
			db.GetContext().LoadAll();

			InitAll();
		}

		#region Private Fields

		private ObservableCollection<CardView> _cardsViews;
		
		private FullCardViewModel _fullCardViewModel;

		private Card _selectedCard;
		private CardView _selectedCardView, _prevSelectedCardView;

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
			throw new NotImplementedException();
		}
		private void SaveChangesExecute()
		{
			throw new NotImplementedException();
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
			var card = db.GetContext().Cards.Add(new Card());
			var cardView = new CardView() { DataContext = new CardViewModel(card) };
			ConfigureAnimation(cardView);
			db.GetContext().SaveChangesAsync();
			_selectedCard = card;
			_selectedCard.OwnerDoctor = _loginedUser as Doctor;
			_selectedCardView = cardView;
			OpenFullCardCommand.Execute(null);
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


		#endregion

		#region Private Methods

		void InitAll()
		{
			InitCommands();
			InitCards();
		}

		void InitCards()
		{
			CardsViews = new ObservableCollection<CardView>();
			var dbs = db.GetContext();
			foreach (var card in dbs.Cards)
			{
				var view = new CardView() { DataContext = new CardViewModel(card) };
				ConfigureAnimation(view);
				CardsViews.Add(view);
			}
			_selectedCard = dbs.Cards.Local[0];
			OpenFullCardCommand.Execute(null);
		}


		private void ConfigureAnimation(CardView view)
		{
			view.MouseEnter += view_MouseEnter;
			view.MouseLeave += view_MouseLeave;
			view.MouseLeftButtonDown += view_MouseLeftButtonDown;
		}
		private void ConfigureAnimation(NoteView view)
		{

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
