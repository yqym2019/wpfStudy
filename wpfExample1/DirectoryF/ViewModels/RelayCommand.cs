using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpfExample1.DirectoryF.ViewModels
{
    /// <summary>
    /// A basic command that runs an Action
    /// </summary>
    class RelayCommand : ICommand
    {
        #region Private Members
        private Action mAction;
        #endregion

        #region Public Events
        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> values has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        public RelayCommand(Action action)
        {
            this.mAction = action;
        }
        #endregion

        #region Command Methods

        /// <summary>
        /// a relay Command can always exexcute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the commands Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion
    }
}
