using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Client.ViewModels
{
    public class ViewModelCommand : ICommand
    {
        //feilds
        private  Action<object> execute;
        private Func<object,bool> canExecute;
        //Constructors
        //event
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            execute = executeAction;
            canExecute = canExecuteAction;
        }
        public ViewModelCommand(Action<object> executeAction)
        {
            execute = executeAction;
            canExecute = null;
        }



        //methods
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }

    }
}

