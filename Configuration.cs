using System;
using System.Collections.Generic;
using System.IO;

namespace DoomLoader
{
    internal class Configuration
    {
        private const string CONFIG = "doomloader.ini";
        public string pwad_dir;
        public List<string> ports;

        public Configuration()
        {
            this.ports = new List<string>();
            this.pwad_dir = string.Empty;
            if (File.Exists("doomloader.ini"))
            {
                foreach (string readLine in File.ReadLines("doomloader.ini"))
                {
                    if (string.IsNullOrEmpty(this.pwad_dir))
                        this.pwad_dir = readLine;
                    else
                        this.ports.Add(readLine);
                }
                if (!string.IsNullOrEmpty(this.pwad_dir) && this.ports.Count != 0)
                    return;
                this.clear();
            }
            else
            {
                File.Create("doomloader.ini").Close();
                this.clear();
            }
        }

        private void clear()
        {
            this.ports.Clear();
            this.pwad_dir = string.Empty;
        }

        public bool check_integrity() => this.ports.Count > 0 && !string.IsNullOrEmpty(this.pwad_dir);

        public void save()
        {
            List<string> configText = new List<string>()
            {
                this.pwad_dir
            };
            this.ports.ForEach((Action<string>)(port => configText.Add(port)));
            File.WriteAllLines("doomloader.ini", configText.ToArray());
        }
    }
}
