using DocumentFormat.OpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FileOrbis___File_System_Reporter
{
    public class ScanProcess
    {
        private Form1 frm;

        public ScanProcess(Form1 form)
        {
            frm = form;
        }

        public void ScanOperation(string selectedFolder, string DateType)
        {
            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);

                int totalFiles = files.Length;
                int processedFiles = 0;

                frm.progressBar1.Maximum = totalFiles;
                frm.progressBar1.Value = 0;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                foreach (string file in files)
                {
                    frm.Fileİnformations(file);
                    frm.GetDateType(DateType, file);
                    if (frm.fileDate > frm.selectedDate)
                    {
                        string listItem = frm.GetListBoxItem(frm.fileDirectory, frm.fileName, frm.createDate, frm.modifiedDate, frm.accessDate, frm.fileSize);
                        frm.listBox1.Items.Add(listItem);
                    }
                    else
                    {
                        string listItem = frm.GetListBoxItem(frm.fileDirectory, frm.fileName, frm.createDate, frm.modifiedDate, frm.accessDate, frm.fileSize);
                        frm.listBox2.Items.Add(listItem);
                    }

                    processedFiles++;
                    frm.progressBar1.Value = processedFiles;

                    frm.lblPath.Text = "PATH: " + frm.fileDirectory + "\\" + frm.fileName;
                    frm.lblScannedItem.Text = $"{processedFiles} / {totalFiles} items were scanned.";

                    Application.DoEvents();

                    frm.lblTotalTime.Text = $"Scan was completed. Total elapsed time: {stopwatch.Elapsed.TotalSeconds} seconds";
                    frm.IsItDoneScan();
                }
                stopwatch.Stop();
            }
            else
            {
                MessageBox.Show("Please select a valid folder.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
