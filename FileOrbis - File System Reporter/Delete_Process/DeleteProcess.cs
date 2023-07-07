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
        public void DeleteDirectory(string path, List<Fileİnformation> fileInformations)
        {
            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);

            foreach (Fileİnformation fileInfo in fileInformations)
            {
                File.SetAttributes(fileInfo.FilePath, FileAttributes.Normal);
                File.Delete(fileInfo.FilePath);
            }

            foreach (string directory in directories)
            {
                DeleteDirectory(directory, fileInformations);
            }

            Directory.Delete(path, false);
        }
    }
}
