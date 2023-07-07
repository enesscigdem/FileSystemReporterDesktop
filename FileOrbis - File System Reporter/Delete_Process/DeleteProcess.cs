using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter
{
    public class DeleteProcess
    {
        Form1 frm = new Form1();
        public DeleteProcess(Form1 form)
        {
            frm = form;
        }
        public void DeleteDirectory(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string directory in directories)
            {
                DeleteDirectory(directory);
            }

            Directory.Delete(path, false);
        }
    }
}
