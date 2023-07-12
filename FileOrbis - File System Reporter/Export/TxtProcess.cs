using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileOrbis___File_System_Reporter
{
    public class TxtProcess
    {
        public void SaveTxt(string sourcepath, List<Fileİnformation> fileInformations)
        {
            string selectedFolder = sourcepath;

            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                string fileNameAfterDate = string.Format("output{0:dd-MM-yyyy_HH.mm.ss}.txt", DateTime.Now);
                string textFilePath = Path.Combine(Application.StartupPath, "output", fileNameAfterDate);

                using (StreamWriter sw = new StreamWriter(textFilePath))
                {
                    foreach (Fileİnformation fileInfo in fileInformations)
                    {
                        string item = $"{fileInfo.FilePath + "\\" + fileInfo.FileName}";
                        item += $"  Create Date: {fileInfo.FileCreateDate}";
                        item += $"  Modified Date: {fileInfo.FileModifiedDate}";
                        item += $"  Access Date: {fileInfo.FileAccessDate}";
                        item += $"\n  File Size (bytes): {fileInfo.FileSize}";

                        sw.WriteLine(item);
                    }
                }

                MessageBox.Show("Data saved in text file.", "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a valid folder.", "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
