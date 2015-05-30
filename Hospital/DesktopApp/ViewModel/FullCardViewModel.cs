﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApp.Model;

namespace DesktopApp.ViewModel
{
	class FullCardViewModel : ViewModelBase
	{
		private Card _card;

		public string DoctorName
		{
			get { return _card.OwnerDoctor.FirstName + _card.OwnerDoctor.LastName; }
		}
		public string FirstName
		{
			get { return _card.Patient.FirstName; }
			set
			{
				_card.Patient.FirstName = value;
				OnPropertyChanged("FirstName");
			}
		}
		public string LastName
		{
			get { return _card.Patient.LastName; }
			set
			{
				_card.Patient.LastName = value;
				OnPropertyChanged("LastName");
			}
		}
		public string MiddleName
		{
			get { return _card.Patient.MiddleName; }
			set
			{
				_card.Patient.MiddleName = value;
				OnPropertyChanged("MiddleName");
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
		public string Address
		{
			get { return _card.Patient.Address; }
			set
			{
				_card.Patient.Address = value;
				OnPropertyChanged("Address");
			}
		}
		public string Email
		{
			get { return _card.Patient.Email; }
			set
			{
				_card.Patient.Email = value;
				OnPropertyChanged("Email");
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
		public string Phone
		{
			get { return _card.Patient.Phone; }
			set
			{
				_card.Patient.Phone = value;
				OnPropertyChanged("Phone");
			}
		}




		public FullCardViewModel(Card card)
		{
			_card = card;
		}
	}
}
