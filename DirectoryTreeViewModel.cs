using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoomLoader
{
    public sealed class DirectoryTreeViewModel
    {
        public ObservableCollection<DirectoryTree> Tree { get; set; }

        public DirectoryTreeViewModel()
        {
            Configuration configuration = new Configuration();
            if (!string.IsNullOrEmpty(configuration.pwad_dir))
                this.Tree = this.PopulateTree(configuration.pwad_dir).Children;
            else
                this.Tree = new ObservableCollection<DirectoryTree>();
        }

        public DirectoryTree PopulateTree(string path, DirectoryTree? parent = null)
        {
            DirectoryTree parent1 = new DirectoryTree();
            parent1.path = Path.GetFileName(path);
            parent1.Children = new ObservableCollection<DirectoryTree>();
            parent1.parent = parent;
            if (File.GetAttributes(path).HasFlag((Enum)FileAttributes.Directory))
            {
                foreach (string directory in Directory.GetDirectories(path))
                    parent1.Children.Add(this.PopulateTree(directory, parent1));
                foreach (string file in Directory.GetFiles(path))
                    parent1.Children.Add(this.PopulateTree(file, parent1));
            }
            return parent1;
        }
    }
}
