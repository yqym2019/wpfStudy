using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using wpfExample1.DirectoryF.ViewModels;

namespace wpfExample1
{
    /// <summary>
    /// A view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The Type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }


        /// <summary>
        /// The full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name
        {
            get
            {
                return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath);
            }
        }

        /// <summary>
        /// A list of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if this item can be expand
        /// </summary>
        public bool CanExpand { get 
            {
                return this.Type != DirectoryItemType.File;    
            } 
        }
    
        public bool IsExpanded
        {
            get
            {
                return this.Children?.Count(f => f != null) > 0;
            }
            set
            {
                //If the UI tells us to Expand
                if (value == true)
                    Expand();
                //If the UI tells us to close
                else
                    this.ClearChildren();
                   
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// The Command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="fullPath"> 路径 </param>
        /// <param name="type"> 类别 </param>
        public DirectoryItemViewModel(string fullPath,DirectoryItemType type)
        {
            //Create commands
            this.ExpandCommand = new RelayCommand(Expand);
          
            //Set path and type
            this.FullPath = fullPath;
            this.Type = type;

            //Setup the children as needed
            this.ClearChildren();
        }
        #endregion

        #region Helper Methods

        /// <summary>
        /// Removes all children from the list,adding a dummy item to show the expand icon if required 
        /// </summary>
        private void ClearChildren()
        {
            //clear items
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            //Show the expand arrow if we are not a file
            if (this.Type != DirectoryItemType.File)
                this.Children.Add(null);
        }

        #endregion

        /// <summary>
        /// Expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            if (this.Type == DirectoryItemType.File)
                return;
            //Find All Children
            var children = DirectoryStructure.GetDirectoryContents(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(
                        children.Select(content => new DirectoryItemViewModel(content.FullPath,content.Type))
                    );
        }
    }
}
