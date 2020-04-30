using System;
using System.Windows.Input;

namespace CustomWindowChrome
{
    public class RelayCommand : ICommand
    {
        #region Private Members
        private Action mAction;
        #endregion

        #region Public Events
        /// <summary>
        /// The events thats fired when the <see cref="CanExecute(object)"/> values has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        public RelayCommand(Action action)
        {
            mAction = action;
        }
        #endregion

        #region Execute Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();
        }
        #endregion
    }
}
