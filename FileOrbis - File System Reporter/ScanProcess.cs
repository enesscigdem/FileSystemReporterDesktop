using DocumentFormat.OpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FileOrbis___File_System_Reporter
{
    public delegate void FileScannedCallback(string filePath, string fileName, DateTime fileCreateDate);
    public delegate void lblScannedMessage();
    public delegate void lblPathMessage(string fileInfo);
    public class ScanProcess : Fileİnformation
    {
        public FileScannedCallback FileScannedCallback { get; set; }
        public lblScannedMessage lblScannedMessage { get; set; }
        public lblPathMessage lblPathMessage { get; set; }
        ScanProcess scanProcess;
        private static List<Fileİnformation> fileInformations = new List<Fileİnformation>();
        public ScanProcess(Form1 form)
        {
            frm = form;
            fileInformations = new List<Fileİnformation>();
        }
        private Form1 frm;

        public List<Fileİnformation> GetFileInformations()
        {
            return fileInformations;
        }
        string WhListBox;
        public void ScanFiles(string[] files, DateTime dateTime, string checkedDate, DateTime fileDate)
        {
            foreach (string file in files)
            {
                IDateOptions dateOptions = new CreationDateOptions();
                Fileİnformation fileInfo = new Fileİnformation();
                fileInfo.FilePath = file;
                fileInfo.FileName = Path.GetFileName(file);
                fileInfo.FileCreateDate = dateOptions.GetCreateDate(file);
                fileInfo.FileModifiedDate = dateOptions.GetModifiedDate(file);
                fileInfo.FileAccessDate = dateOptions.GetAccessDate(file);

                fileInformations.Add(fileInfo);

                frm.GetDateType(checkedDate, file);

                if (fileDate > dateTime)
                {
                    // cal back
                    WhListBox = "listbox1";
                    FileScannedCallback?.Invoke(fileInfo.FilePath, fileInfo.FileName, fileInfo.FileCreateDate);
                }
                else
                {
                    WhListBox = "listbox2";
                    FileScannedCallback?.Invoke(fileInfo.FilePath, fileInfo.FileName, fileInfo.FileCreateDate);
                }

                processedFiles++;
                frm.progressBar1.Value = processedFiles;

                lblScannedMessage?.Invoke(); // call back 2

                lblPathMessage?.Invoke(fileInfo.FilePath); // call back 3

                Application.DoEvents();

                frm.lblTotalTime.Text = $"Scan was completed. Total elapsed time: {stopwatch.Elapsed.TotalSeconds} seconds";
                frm.IsItDoneScan();
            }
        }
        //public void ShowFileInformations()
        //{
        //    if (fileInformations.Count > 0)
        //    {
        //        StringBuilder message = new StringBuilder();
        //        foreach (Fileİnformation fileInfo in fileInformations)
        //        {
        //            message.AppendLine($"File Path: {fileInfo.FilePath}");
        //            message.AppendLine($"File Name: {fileInfo.FileName}");
        //            message.AppendLine($"File Create Date: {fileInfo.FileCreateDate}");
        //            message.AppendLine();
        //        }

        //        MessageBox.Show(message.ToString(), "File Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("No file information available.", "File Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        int processedFiles, totalFiles;
        Stopwatch stopwatch;
        public void ScanOperation(string selectedFolder, DateTime dateTime, string checkedDate, DateTime fileDate)
        {
            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);

                totalFiles = files.Length;
                processedFiles = 0;

                frm.progressBar1.Maximum = totalFiles;
                frm.progressBar1.Value = 0;

                stopwatch = new Stopwatch();
                stopwatch.Start();
                scanProcess = this;
                FileScannedCallback = AddFileToListBox;
                lblScannedMessage = UpdateLblScan;
                lblPathMessage = UpdateLblPath;
                ScanFiles(files, dateTime, checkedDate, fileDate);
                stopwatch.Stop();
            }
            else
            {
                MessageBox.Show("Please select a valid folder.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void AddFileToListBox(string filePath, string fileName, DateTime fileCreateDate)
        {
            if (frm.InvokeRequired) // ana iş parçacığı dışından erişilmeye çalışılıp çalışılmadığını belirlemek için kullanılır.
            {
                frm.Invoke(new Action<string, string, DateTime>(AddFileToListBox), filePath, fileName, fileCreateDate);
                return;
            }
            if (WhListBox == "listbox1")
                frm.listBox1.Items.Add(filePath + fileName + fileCreateDate);
            else
                frm.listBox2.Items.Add(filePath + fileName + fileCreateDate);
        }
        private void UpdateLblScan()
        {
            if (frm.InvokeRequired)
            {
                frm.Invoke(new Action(UpdateLblScan));
                return;
            }
            frm.lblScannedItem.Text = $"{processedFiles} / {totalFiles} items were scanned.";
        }
        private void UpdateLblPath(string fileInfo)
        {
            if (frm.InvokeRequired)
            {
                frm.Invoke (new Action<string>(UpdateLblPath));
                return;
            }
            frm.lblPath.Text = fileInfo;
        }
    }
}
