namespace wpfExample1
{
   public class DirectoryItem
    {
        /// <summary>
        /// The Type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path to this Item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this directory item
        /// </summary>
        public string Name { get {
                return DirectoryStructure.GetFileFolderName(this.FullPath);
            } }
    }
}
