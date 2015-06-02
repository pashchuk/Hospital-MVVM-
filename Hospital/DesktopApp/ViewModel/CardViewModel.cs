using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DesktopApp.Commands;
using DesktopApp.Model;

namespace DesktopApp.ViewModel
{
	public class CardViewModel : ViewModelBase
	{
		private Card _card;
		public string Name
		{
			get { return string.Format("{0} {1}", _card.Patient.FirstName, _card.Patient.LastName); }
			set
			{
				var item = value.Split(' ');
				if (item.Length > 0)
					_card.Patient.FirstName = item[0];
				else
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

		public Card GetCard()
		{
			return _card;
		}

		public void UpdateValues()
		{
			Name = string.Format("{0} {1}", _card.Patient.FirstName, _card.Patient.LastName);
			Age = _card.Patient.Age;
			Sex = _card.Patient.Sex;
		}

	}
}
