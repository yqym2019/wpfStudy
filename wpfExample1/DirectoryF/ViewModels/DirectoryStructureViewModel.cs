using System.Collections.ObjectModel;
using System.Linq;

namespace wpfExample1
{
    /// <summary>
    /// The view model for the applicationo main Directory view
    /// </summary>
    public class DirectoryStructureViewModel
    {
        #region Public Properties
        /// <summary>
        /// A list of all directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            //Get the logical drives
            var children = DirectoryStructure.GetLogicalDrives();

            //Create the view models from the data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(drive=>new DirectoryItemViewModel(drive.FullPath,DirectoryItemType.Drive))
                );
        }
        #endregion
    }
}
