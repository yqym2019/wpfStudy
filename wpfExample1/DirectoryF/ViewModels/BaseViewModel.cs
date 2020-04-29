using PropertyChanged;
using System.ComponentModel;
using System.Runtime.Remoting.Channels;

namespace wpfExample1
{
    /// <summary>
    /// A Base view model that fires Property Changed events as needs
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
    }
}
