using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DesktopApp.Commands;
using DesktopApp.Model;

namespace DesktopApp.ViewModel
{
	public class CardViewModel : ViewModelBase
	{
		private Card _card;

		public RelayCommand OpenCardCommand { get; private set; }
		public string Name
		{
			get { return _card.Patient.FirstName; }
			set
			{
				_card.Patient.FirstName = value;
				OnPropertyChanged("Name");
			}
		}
		public int Age
		{
			get { return _card.Patient.Age; }
			set
			{
				_card.Patient.Age = value;
				OnPropertyChanged("Age");
			}
		}
		public string Sex
		{
			get { return _card.Patient.Sex; }
			set
			{
				_card.Patient.Sex = value;
				OnPropertyChanged("Sex");
			}
		}
		public CardViewModel(Card card)
		{
			_card = card;
		}

	}
}
