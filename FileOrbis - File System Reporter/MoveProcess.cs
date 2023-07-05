using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter
{
    public class MoveProcess
    {
        Form1 frm = new Form1();
        public MoveProcess(Form1 form)
        {
            frm = form;
        }
        public void MoveDirectoryByDate(string sourceFolder, string targetDirectory, string dateType)
        {
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            string[] files = Directory.GetFiles(sourceFolder);

            foreach (string file in files)
            {
                frm.GetDateType(dateType, file);

                if (frm.fileDate > frm.selectedDate)
                {
                    string fileName = Path.GetFileName(file);
                    string targetFile = Path.Combine(targetDirectory, fileName);
                    File.Move(file, targetFile);
                }
            }

            string[] subDirectoryies = Directory.GetDirectories(sourceFolder);
            foreach (string subDirectory in subDirectoryies)
            {
                string subDirectoryName = Path.GetFileName(subDirectory);
                string[] subDirectoryFiles = Directory.GetFiles(subDirectory);
                string targetSubDirectory = Path.Combine(targetDirectory, subDirectoryName);
                if (subDirectoryFiles.Length >= 1)
                {
                    MoveDirectoryByDate(subDirectory, targetSubDirectory, dateType);
                }
                else
                {
                    if (frm.chEmptyFolders.Checked)
                    {
                        MoveDirectoryByDate(subDirectory, targetSubDirectory, dateType);
                    }
                    else
                        continue;
                }
            }

            Directory.Delete(sourceFolder, recursive: true);
        }

    }
}
