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
//			GenerateTestDataBaseData();
			InitializeComponent();
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
			card1.Sesions.Add(new Session() { Diagnosis = diagnosis1 });
			card1.Sesions.Add(new Session() { Diagnosis = diagnosis2 });
			card1.Sesions[0].Notes.AddRange(new List<Note>() { note2_1, note1_1, note4_1, note3_1 });
			card1.Sesions[1].Notes.AddRange(new List<Note>() { note4_1, note2_1, note3_1, note4_1, note2_1 });
			var card2 = new Card()
			{
				Patient = patient2,
				Name = "Card2",
				OwnerDoctor = doc2,
			};
			card2.Sesions.Add(new Session() { Diagnosis = diagnosis2 });
			card2.Sesions.Add(new Session() { Diagnosis = diagnosis5 });
			card2.Sesions[0].Notes.AddRange(new List<Note>() { note1_2, note2_2, note3_2, note4_2 });
			var card3 = new Card()
			{
				Patient = patient3,
				Name = "Card3",
				OwnerDoctor = doc3,
			};
			card3.Sesions.Add(new Session() { Diagnosis = diagnosis3 });
			card3.Sesions[0].Notes.AddRange(new List<Note>() { note3_3, note1_3, note2_3, note4_3 });
			var card4 = new Card()
			{
				Patient = patient4,
				Name = "Card4",
				OwnerDoctor = doc4,
			};
			card4.Sesions.Add(new Session() { Diagnosis = diagnosis4 });
			card4.Sesions.Add(new Session() { Diagnosis = diagnosis2 });
			card4.Sesions[0].Notes.AddRange(new List<Note>() { note4_4, note3_4, note2_4, note1_4 });
			card4.Sesions[1].Notes.AddRange(new List<Note>() { note4_4, note3_4, note2_4, note1_4, note4_4, note2_4, note3_4 });
			var card5 = new Card()
			{
				Patient = patient5,
				Name = "Card5",
				OwnerDoctor = doc1,
			};
			card5.Sesions.Add(new Session() { Diagnosis = diagnosis4 });
			card5.Sesions.Add(new Session() { Diagnosis = diagnosis1 });
			card5.Sesions[0].Notes.AddRange(new List<Note>()
			{
				note2_5,
				note1_5,
				note3_5,
				note4_5,
				note2_5,
				note1_5,
				note3_5,
				note4_5
			});
			card5.Sesions[1].Notes.AddRange(new List<Note>() { note2_5, note1_5, note3_5, note4_5 });
			var card6 = new Card()
			{
				Patient = patient6,
				Name = "Card6",
				OwnerDoctor = doc2,
			};
			var ses1 = new Session() {Diagnosis = diagnosis5};
			var ses2 = new Session() {Diagnosis = diagnosis4};
			ses1.Notes.AddRange(new List<Note>() { note2_6, note1_6, note4_6, note3_6 });
//			ses2.Notes.AddRange(new List<Note>() { note2_6, note1_6, note4_6, note3_6 });
			card6.Sesions.AddRange(new List<Session>() {ses1});
//			card6.Sesions.Add(new Session() { Diagnosis = diagnosis5 });
//			card6.Sesions.Add(new Session() { Diagnosis = diagnosis4 });
//			card6.Sesions[0].Notes.AddRange(new List<Note>() { note2_6, note1_6, note4_6, note3_6 });
//			card6.Sesions[1].Notes.AddRange(new List<Note>() { note2_6, note1_6, note4_6, note3_6, note2_6, note1_6 });
//			HospitalContext.GetContext().Cards.AddRange(new List<Card>() { card1, card2, card3, card4, card5, card6 });
			HospitalContext.GetContext().Cards.Add(card6);
			HospitalContext.GetContext().SaveChangesAsync();
		}
	}
}
