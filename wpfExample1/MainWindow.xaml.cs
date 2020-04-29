using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        /// <summary>
        /// first load function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           foreach(var drive in  Directory.GetLogicalDrives())
            {
                //内部属性设置
                TreeViewItem tvwItem = new TreeViewItem()
                {
                    Header = drive,//节点头
                    Tag = drive, //节点值                   
                };

                tvwItem.Items.Add(null);
                tvwItem.Expanded += TvwItem_Expanded;//添加展开事件

                tvwFolder.Items.Add(tvwItem);
            }
        }

        private void TvwItem_Expanded(object sender, RoutedEventArgs e)
        {
            #region Initial Checks

            var item = sender as TreeViewItem;
            Debug.WriteLine(item.Tag);

            if (item.Items.Count != 1 || item.Items[0] != null) return;

            //非手动添加的第一项
            item.Items.Clear();
            #endregion

            #region Get Folders
            //get full path from tag
            var fullPath = item.Tag.ToString();

            //record all directory info
            var directories = new List<String>();

            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                {
                    directories.AddRange(dirs);
                }
            }
            catch { }

            //foreach directories and add TreeViewItem
            directories.ForEach(dire =>
            {
                var subItem = new TreeViewItem()
                {
                    //System.IO.Path.GetDirectoryName(dire) , //获取文件路径
                    Header = GetFileFolderName(dire), 
                    Tag = dire
                };

                subItem.Items.Add(null);
                subItem.Expanded += TvwItem_Expanded;
                item.Items.Add(subItem);

            });
            #endregion

            #region Get Files
            var files = new List<string>();
            try
            {
                var dirs = Directory.GetFiles(fullPath);
                if (dirs.Length > 0)
                {
                    files.AddRange(dirs);
                }
            }
            catch { }

            //foreach directories and add TreeViewItem
            files.ForEach(filePath =>
            {
                var subItem = new TreeViewItem()
                {
                    Header = GetFileFolderName(filePath), //System.IO.Path.GetDirectoryName(dire) , //获取文件路径
                    Tag = filePath
                };
                               
                item.Items.Add(subItem);
            });

            #endregion           
        }

        /// <summary>
        /// find the file or folder name from a full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileFolderName(string path)
        {
            //if we have no path,return empty
            if (string.IsNullOrEmpty(path)) return string.Empty;

            //Make all slashes back slashes
            var normalizedPath = path.Replace('/', '\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');

            //若不存在反斜杠 则说明 这就是文件 否则返回最终文件名部分
            if (lastIndex <= 0) return path;

            return path.Substring(lastIndex + 1);
        }
    }
}
