using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace wpfExample1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// first Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // this.DataContext = new MvvmTest();
            tvwFolder.DataContext = new DirectoryStructureViewModel();
            //var d = new DirectoryStructureViewModel();
            //var item1 = d.Items[0];
            //d.Items[0].ExpandCommand.Execute(null);
        }
            
    }
}
