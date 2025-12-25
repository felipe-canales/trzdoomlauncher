using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;

namespace DoomLoader
{
    public partial class MainWindow : Window
    {
        /*private Configuration config = new Configuration();
        private List<string> iwads = new List<string>()
        {
            "doom.wad",
            "doom2.wad",
            "fredoom.wad",
            "heretic.wad",
            "hexdd.wad",
            "hexen.wad",
            "plutonia.wad",
            "strife.wad",
            "tnt.wad"
        };
        private Dictionary<string, string> ports;
        internal MultiselectTreeView pwad_select;
        internal System.Windows.Controls.TextBox pwad_text;
        internal System.Windows.Controls.ComboBox port_select;
        internal System.Windows.Controls.ComboBox iwad_select;
        public MainWindow()
        {
            InitializeComponent();
            if (!config.check_integrity())
            {
                System.Windows.MessageBox.Show("You need to set directories for IWADs, PWADs and the path for at least one doom port");
            }
            ports = new Dictionary<string, string>();
            ReloadPWads();
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {
            string port = ports[(string)port_select.SelectedItem];
            IList selectedItems = pwad_select.SelectedItems;
            string str1 = "-iwad " + (string)iwad_select.SelectedItem;

            foreach (DirectoryTree directoryTree1 in selectedItems)
            {
                // Build path
                DirectoryTree directoryTree2;
                string str2 = (directoryTree2 = directoryTree1).path;
                string format = "{0}\\{1}";
                while (directoryTree2.parent != null)
                {
                    directoryTree2 = directoryTree2.parent;
                    str2 = string.Format(format, directoryTree2.path, str2);
                }
                string str3 = string.Format(format, Path.GetDirectoryName(config.pwad_dir), str2);
                str1 = str1 + " -file \"" + str3 + "\"";
            }
            string currentDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(Path.GetDirectoryName(port));
            Process.Start(new ProcessStartInfo()
            {
                FileName = Path.GetFileName(port),
                WorkingDirectory = Path.GetDirectoryName(port),
                Arguments = str1
            });
            Directory.SetCurrentDirectory(currentDirectory);
        }
        private void ReloadPWads()
        {
            ports.Clear();
            config.ports.ForEach(p => ports.Add(Path.GetFileNameWithoutExtension(p), p));
            pwad_text.Text = config.pwad_dir;
            port_select.ItemsSource = ports.Keys;
            port_select.SelectedIndex = 0;
            iwad_select.ItemsSource = iwads;
            iwad_select.SelectedIndex = 0;
        }

        private void AddPort(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? nullable = openFileDialog.ShowDialog();
            if (nullable.GetValueOrDefault() == nullable.HasValue)
            {
                System.Windows.MessageBox.Show(openFileDialog.FileName);
            }
            config.ports.Add(openFileDialog.FileName);
            config.save();
            ReloadPWads();
        }

        private void EditIWADDir(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            System.Windows.MessageBox.Show(folderBrowserDialog.SelectedPath);
            config.save();
            ReloadPWads();
        }

        private void EditPWADDir(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            System.Windows.MessageBox.Show(folderBrowserDialog.SelectedPath);
            config.pwad_dir = folderBrowserDialog.SelectedPath;
            config.save();
            ReloadPWads();
        }*/
    }
}
