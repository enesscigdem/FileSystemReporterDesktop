using DocumentFormat.OpenXml;
using FileOrbis___File_System_Reporter.Date_Process;
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
    public delegate void lblTotalTımeCallBack();
    public delegate void lblPathMessage(string fileInfo);
    public delegate void ProgressBarCallBack();
    public class ScanProcess : Fileİnformation
    {
        public ScanProcess(Form1 form)
        {
            frm = form;
            fileInformations = new List<Fileİnformation>();
        }
        private Form1 frm;
        string WhListBox;
        int processedFiles, totalFiles;
        Stopwatch stopwatch;
        DateType dt = new DateType();
        public FileScannedCallback FileScannedCallback { get; set; }
        public lblScannedMessage lblScannedMessage { get; set; }
        public lblTotalTımeCallBack lblTotalTımeCallBack { get; set; }
        public lblPathMessage lblPathMessage { get; set; }
        public ProgressBarCallBack ProgressBarCallBack { get; set; }
        ScanProcess scanProcess;
        private  List<Fileİnformation> fileInformations = new List<Fileİnformation>();

        public List<Fileİnformation> ScanFiles(string[] files, DateTime dateTime, string checkedDate, DateTime fileDate)
        {
            Parallel.ForEach(files, file =>
            {
                IDateOptions dateOptionsCr = new CreationDateOptions();
                IDateOptions dateOptionsMd = new ModifiedDateOptions();
                IDateOptions dateOptionsAc = new AccessDateOptions();

                Fileİnformation fileInfo = new Fileİnformation();
                fileInfo.FilePath = file;
                fileInfo.FileName = Path.GetFileName(file);
                fileInfo.FileCreateDate = dateOptionsCr.SetDate(file);
                fileInfo.FileModifiedDate = dateOptionsMd.SetDate(file);
                fileInfo.FileAccessDate = dateOptionsAc.SetDate(file);
                FileInfo fileInformation = new FileInfo(file);
                fileInfo.FileSize = fileInformation.Length;


                fileInformations.Add(fileInfo);

                fileDate = dt.GetDateType(checkedDate, file);

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

                ProgressBarCallBack?.Invoke(); // call back

                lblScannedMessage?.Invoke();

                lblPathMessage?.Invoke(fileInfo.FilePath);

                Application.DoEvents();

                lblTotalTımeCallBack?.Invoke();
            });
            return fileInformations;
        }
        public List<Fileİnformation> ScanOperation(string selectedFolder, DateTime dateTime, string checkedDate, DateTime fileDate)
        {
            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);

                totalFiles = files.Length;
                processedFiles = 0;

                stopwatch = new Stopwatch();
                stopwatch.Start();
                scanProcess = this;
                FileScannedCallback = AddFileToListBox;
                lblScannedMessage = UpdateLblScan;
                lblPathMessage = UpdateLblPath;
                lblTotalTımeCallBack = UpdateLblTotalTıme;
                ProgressBarCallBack = UpdateProgressBar;
                var fileList= ScanFiles(files, dateTime, checkedDate, fileDate);
                stopwatch.Stop();
                return fileList;
            }
            else
            {
                MessageBox.Show("Please select a valid folder.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
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
                frm.Invoke(new Action<string>(UpdateLblPath), fileInfo);
                return;
            }
            frm.lblPath.Text = fileInfo;
        }
        private void UpdateLblTotalTıme()
        {
            if (frm.InvokeRequired)
            {
                frm.Invoke(new Action(UpdateLblTotalTıme));
                return;
            }
            frm.lblTotalTime.Text = $"Scan was completed. Total elapsed time: {stopwatch.Elapsed.TotalSeconds} seconds";
        }
        private void UpdateProgressBar()
        {
            if (frm.InvokeRequired)
            {
                frm.Invoke(new Action(UpdateProgressBar));
                return;
            }
            frm.progressBar1.Maximum = totalFiles;
            frm.progressBar1.Value = processedFiles;
        }
    }
}
