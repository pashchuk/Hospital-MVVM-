using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;

namespace DesktopApp.Commands
{
	public class RelayCommand : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		private Action methodToExecute;
		private Action<object> methodToExecuteWithParams; 
		private Func<bool> canExecuteEvaluator;

		public RelayCommand(Action<object> methodToExecute, Func<bool> canExecuteEvaluator)
		{
			this.methodToExecuteWithParams = methodToExecute;
			this.canExecuteEvaluator = canExecuteEvaluator;
		}
		public RelayCommand(Action methodToExecute, Func<bool> canExecuteEvaluator)
		{
			this.methodToExecute = methodToExecute;
			this.canExecuteEvaluator = canExecuteEvaluator;
		}
		public RelayCommand(Action methodToExecute)
			: this(methodToExecute, null)
		{
		}
		public RelayCommand(Action<object> methodToExecute)
			: this(methodToExecute, null)
		{
		}
		public bool CanExecute(object parameter)
		{
			if (this.canExecuteEvaluator == null)
			{
				return true;
			}
			else
			{
				bool result = this.canExecuteEvaluator.Invoke();
				return result;
			}
		}
		public void Execute(object parameter)
		{
			if (methodToExecuteWithParams != null)
				this.methodToExecuteWithParams.Invoke(parameter);
			else
				this.methodToExecute.Invoke();
		}
	}
}
