using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace wpfExample1
{
    /// <summary>
    /// A helper class to query information about directories
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Get all logical drives on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();               
        }

        /// <summary>
        /// Gets the directories top-level content
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            //create empty list
            var items = new List<DirectoryItem>();
            #region Get Folders
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                {
                    items.AddRange(dirs.Select(dir=>new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
                }
            }
            catch { }         

            #endregion

            #region Get Files
            var files = new List<string>();
            try
            {
                var dirs = Directory.GetFiles(fullPath);
                if (dirs.Length > 0)
                {
                    items.AddRange(dirs.Select(file=>new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
                }
            }
            catch { }

            #endregion

            return items;
        }

        #region Helpers
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
        #endregion
    }
}
