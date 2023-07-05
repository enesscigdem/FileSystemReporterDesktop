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
        public void SaveTxt(string sourcepath,string fileDirectory,string fileName, DateTime createDate, DateTime modifiedDate, DateTime accessDate, long fileSize)
        {
            string selectedFolder = sourcepath;

            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);

                string fileNameAfterDate = string.Format("output{0:dd-MM-yyyy_HH.mm.ss}.txt", DateTime.Now);
                string textFilePath = Path.Combine(Application.StartupPath, "output", fileNameAfterDate);

                using (StreamWriter sw = new StreamWriter(textFilePath))
                {
                    foreach (string file in files)
                    {
                        Form1 form1 = new Form1();
                        form1.Fileİnformations(file);

                        string item = $"{fileDirectory + "\\" + fileName}";
                        item += $"  Create Date: {createDate}";
                        item += $"  Modified Date: {modifiedDate}";
                        item += $"  Access Date: {accessDate}";
                        item += $"\n  File Size (bytes): {fileSize}";

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
