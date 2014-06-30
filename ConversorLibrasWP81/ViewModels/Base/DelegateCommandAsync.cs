namespace ConversorLibrasWP81.ViewModels.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class DelegateCommandAsync : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<Task<bool>> _canExecute;

        public DelegateCommandAsync(Func<Task> execute) : this(execute, null) { }

        public DelegateCommandAsync(Func<Task> execute, Func<Task<bool>> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute().Result;

            return true;
        }

        public event EventHandler CanExecuteChanged;
        
        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            var tmpHandle = CanExecuteChanged;
            if (tmpHandle != null)
                tmpHandle(this, new EventArgs());
        }

        
    }
}
