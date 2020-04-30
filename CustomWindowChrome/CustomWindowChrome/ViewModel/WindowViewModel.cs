using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 400;

        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 400;

        public string Test { get; set; } = "Hello String";

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder { get; set; } = 10;

        public Thickness ResizeBorderThickness
        {
            get { return new Thickness(ResizeBorder+OuterMarginSize); }
        }

        public Thickness InnerContentPadding
        {
            get { return new Thickness(ResizeBorder); }
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

        /// <summary>
        /// The height of the title bar / caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight); } }
      
        #endregion

        #region Commands
        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }
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

            MinimizeCommand = new RelayCommand(() => mWindows.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindows.WindowState = WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindows.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindows, GetMousePosition() ));
        }
        #endregion

        #region Private Helpers  
        [DllImport("user32.dll")]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        /// <summary>
        /// 获取鼠标当前物理坐标
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            //Win32Point w32Mouse = new Win32Point();
            //GetCursorPos(ref w32Mouse);
            //return new Point(w32Mouse.X+mWindows.Left, w32Mouse.Y+mWindows.Top);

            //Position of the mouse relative to the window
            var pos = Mouse.GetPosition(mWindows);

            //Add the window position so its a "ToScreen"
            return new Point(pos.X + mWindows.Left, pos.Y + mWindows.Top);
        }
        #endregion
    }
}
