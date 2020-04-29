using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace wpfExample1
{
    //实现一个 数据转换接口 用于 树头信息与图片的转换
    /// <summary>
    /// Converts a full path to a specific image type of a drive,folder or file
    /// </summary>
    [ValueConversion(typeof(DirectoryItemType),typeof(BitmapImage))]
    public class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {           
            //默认是文件图标 当为底层磁盘 | 文件夹的时候 则变更图标
            var image = "Images/file.png";

            switch((DirectoryItemType)value)
            {
                case DirectoryItemType.Drive: image = "Images/drive.png";break;
                case DirectoryItemType.Folder: image = "Images/folder.png";break;
            }
            
            var pathinfo = Environment.CurrentDirectory + "../../../" + image;

            return new BitmapImage(new Uri(pathinfo));//$"pack://application:,,,/Images/drive.png"r           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
