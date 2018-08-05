using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SileniumTestIntro_2
{
    class ResultWriter
    {
        private string path = "";

        public ResultWriter(string path)
        {
            this.path = path;
        }

        public void WriteStatus(string ID, string name, string status)
        {
            string text = "";
            text = String.Format("\n**************************\nTest ID - {0}; Name - {1}; Status - {2}\n", ID, name, status);

            if (File.Exists(path))
            {
                File.AppendAllText(path, text);
            }
        }

        public void CleanFile()
        {
            string text = "";
            if (File.Exists(path))
            {
                File.WriteAllText(path, text);
            }
        }
    }
}
