using System.Collections.ObjectModel;

namespace DoomLoader
{
    public sealed class DirectoryTree
    {
        public string path { get; set; }

        public DirectoryTree? parent { get; set; }

        public ObservableCollection<DirectoryTree> Children { get; set; }

        public void Add(DirectoryTree item) => this.Children.Add(item);
    }
}
