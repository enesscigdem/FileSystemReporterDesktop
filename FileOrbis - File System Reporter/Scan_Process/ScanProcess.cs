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
    public delegate void FileScannedCallback(string filePath, string fileName, DateTime fileCreateDate,string WhListBox);
    public delegate void lblScannedMessage(int processedFiles, int totalFiles);
    public delegate void lblTotalTımeCallBack(Stopwatch stopwatch);
    public delegate void lblPathMessage(string fileInfo);
    public delegate void ProgressBarCallBack(int processedFiles, int totalFiles);
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
        private List<Fileİnformation> fileInformations = new List<Fileİnformation>();

        public List<Fileİnformation> ScanFiles(string[] files, DateTime dateTime, string checkedDate, DateTime fileDate, int threadCount)
        {
            Parallel.ForEach(files, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, file =>
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
                    FileScannedCallback?.Invoke(fileInfo.FilePath, fileInfo.FileName, fileInfo.FileCreateDate,WhListBox);
                }
                else
                {
                    WhListBox = "listbox2";
                    FileScannedCallback?.Invoke(fileInfo.FilePath, fileInfo.FileName, fileInfo.FileCreateDate,WhListBox);
                }

                processedFiles++;

                ProgressBarCallBack?.Invoke(processedFiles,totalFiles); // call back

                lblScannedMessage?.Invoke(processedFiles, totalFiles);

                lblPathMessage?.Invoke(fileInfo.FilePath);

                Application.DoEvents();

                lblTotalTımeCallBack?.Invoke(stopwatch);
            });
            return fileInformations;
        }
        public List<Fileİnformation> ScanOperation(string selectedFolder, DateTime dateTime, string checkedDate, DateTime fileDate, int threadCount)
        {
            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);

                totalFiles = files.Length;
                processedFiles = 0;

                stopwatch = new Stopwatch();
                stopwatch.Start();
                scanProcess = this;
                var fileList = ScanFiles(files, dateTime, checkedDate, fileDate,threadCount);
                stopwatch.Stop();
                return fileList;
            }
            else
            {
                MessageBox.Show("Please select a valid folder.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }
    }
}
