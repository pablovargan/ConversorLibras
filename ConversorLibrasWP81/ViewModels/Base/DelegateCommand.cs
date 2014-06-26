namespace ConversorLibrasWP81.ViewModels.Base
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        private Action execute;
        private Func<bool> canExecute;

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecute != null)
            {
                return this.canExecute();
            }

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.execute();
        }

        public void RaiseCanExecuteChanged()
        {
            var tmpHandler = CanExecuteChanged;
            if (tmpHandler != null)
            {
                tmpHandler(this, new EventArgs());
            }
        }
    }
}
