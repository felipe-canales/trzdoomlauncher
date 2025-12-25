using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace DoomLoader
{
    public sealed class DirectoryTreeViewModel
    {
        private List<string> ACCEPTED_EXTENSIONS = new List<string> { ".pk3", ".wad" };

        public ObservableCollection<DirectoryTree> Tree { get; set; }

        public DirectoryTreeViewModel()
        {
            Configuration configuration = new Configuration();
            if (!string.IsNullOrEmpty(configuration.pwad_dir))
                Tree = PopulateTree(configuration.pwad_dir).Children;
            else
                Tree = new ObservableCollection<DirectoryTree>();
        }

        public DirectoryTree PopulateTree(string path, DirectoryTree? parent = null)
        {
            DirectoryTree parent1 = new DirectoryTree();
            parent1.path = Path.GetFileName(path);
            parent1.Children = new ObservableCollection<DirectoryTree>();
            parent1.parent = parent;
            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                foreach (string directory in Directory.GetDirectories(path))
                    parent1.Children.Add(PopulateTree(directory, parent1));
                foreach (string file in Directory.GetFiles(path))
                {
                    if (ACCEPTED_EXTENSIONS.Contains(Path.GetExtension(file).ToLower()))
                        parent1.Children.Add(PopulateTree(file, parent1));
                }
            }
            return parent1;
        }
    }
}
