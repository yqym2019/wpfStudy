using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CustomWindowChrome
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class WindowViewModel:BaseViewModel
    {
        #region Private Member
        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindows;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// The radius of the edges of the window   
        /// </summary>
        private int mWindowRadius = 10;
        #endregion

        #region Public Properties
        public string Test { get; set; } = "Hello String";

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 10;

        public Thickness ResizeBorderThickness
        {
            get { return new Thickness(ResizeBorder+OuterMarginSize); }
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize
        {
            get
            {
                return mWindows.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            }
            set
            {
                mOuterMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness
        {
            get { return new Thickness(OuterMarginSize); }
        }        

        /// <summary>
        /// The radius of the edges of the window  
        /// </summary>
        public int WindowRadius
        {
            get
            {
                return mWindows.WindowState == WindowState.Maximized ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius
        {
            get { return new CornerRadius(WindowRadius); }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            mWindows = window;

            //Listen out for the window resizing
            mWindows.StateChanged += (sender, e) =>
              {
                  //Fire off events for all properties that are affected by a resize
                  OnPropertyChanged(nameof(ResizeBorderThickness));
                  OnPropertyChanged(nameof(OuterMarginSize));
                  OnPropertyChanged(nameof(OuterMarginSizeThickness));
                  OnPropertyChanged(nameof(WindowRadius));
                  OnPropertyChanged(nameof(WindowCornerRadius));
              };
        }
        #endregion
    }
}
