using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopApp.Model;

namespace DesktopApp.ViewModel
{
	class SessionViewModel :ViewModelBase
	{
		private Session _session;
		private bool _state;

		public int Id { get; private set; }

		public string Diagnosis
		{
			get { return _session.Diagnosis.Description; }
			set
			{
				_session.Diagnosis.Description = value;
				OnPropertyChanged("Diagnosis");
			}
		}

		public bool State
		{
			get { return _state; }
			set
			{
				_state = value;
				OnPropertyChanged("State");
			}
		}

		public DateTime Date
		{
			get { return _session.Date; }
		}

		public Session GetSession()
		{
			return _session;
		}

		public SessionViewModel(Session session, int id)
		{
			_session = session;
			Id = id;
			_state = false;
		}

		public SessionViewModel(Session session) : this(session, 1)
		{
		}
	}
}
