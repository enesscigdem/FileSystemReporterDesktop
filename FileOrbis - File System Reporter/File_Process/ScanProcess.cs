using DocumentFormat.OpenXml;
using FileOrbis___File_System_Reporter.Date_Process;
using FileOrbis___File_System_Reporter.DateOptions;
using FileOrbis___File_System_Reporter.File_İnformation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FileOrbis___File_System_Reporter
{
    public delegate void FileScannedCallback(string filePath, string fileName, DateTime fileCreateDate, string WhListBox);
    public delegate void lblScannedMessage(int processedFiles, int totalFiles);
    public delegate void lblTotalTımeCallBack(Stopwatch stopwatch);
    public delegate void lblPathMessage(string fileInfo);
    public delegate void ProgressBarCallBack(int processedFiles, int totalFiles);
    public class ScanProcess
    {
        public ScanProcess()
        {
            fileInformations = new List<Fileİnformation>();
            folderInformations = new List<Folderİnformation>();
        }
        int processedFiles;
        Stopwatch stopwatch;
        DateType dt = new DateType();
        public FileScannedCallback FileScannedCallback { get; set; }
        public lblScannedMessage lblScannedMessage { get; set; }
        public lblTotalTımeCallBack lblTotalTımeCallBack { get; set; }
        public lblPathMessage lblPathMessage { get; set; }
        public ProgressBarCallBack ProgressBarCallBack { get; set; }
        private List<Fileİnformation> fileInformations = new List<Fileİnformation>();
        private List<Folderİnformation> folderInformations = new List<Folderİnformation>();
        private object fileInformationLock = new object();
        private object folderInformationLock = new object();

        IDateOptions dateOptionsMd = new ModifiedDateOptions();
        IDateOptions dateOptionsCr = new CreatedDateOption();
        IDateOptions dateOptionsAc = new AccessedDateOption();
        public (List<Fileİnformation> files, List<Folderİnformation> folders) ScanFiles(string sourcePath, int threadCount, int totalFiles)
        {
            fileInformations.Clear();
            folderInformations.Clear();
            Parallel.ForEach(Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories), new ParallelOptions { MaxDegreeOfParallelism = threadCount }, newPath =>
            {
                Fileİnformation fileInfo = new Fileİnformation();
                fileInfo.FilePath = newPath;
                fileInfo.FileName = Path.GetFileName(newPath);
                fileInfo.FileCreateDate = dateOptionsCr.SetDate(newPath);
                fileInfo.FileModifiedDate = dateOptionsMd.SetDate(newPath);
                fileInfo.FileAccessDate = dateOptionsAc.SetDate(newPath);
                fileInfo.FileSize = fileInfo.FileSize;

                lock (fileInformationLock)
                {
                    fileInformations.Add(fileInfo);
                    processedFiles++;
                }
                //if (Convert.ToInt16(stopwatch.Elapsed.TotalMilliseconds) % 100 == 0 || processedFiles == totalFiles || processedFiles > totalFiles)
                //{
                //    lblPathMessage?.Invoke(fileInfo.FilePath);
                //    lblTotalTımeCallBack?.Invoke(stopwatch);
                //    lblScannedMessage?.Invoke(processedFiles, totalFiles);
                //    ProgressBarCallBack?.Invoke(processedFiles, totalFiles);
                //}
            });
            MessageBox.Show("Scan Process is finished." + totalFiles);
            Parallel.ForEach(Directory.GetDirectories(sourcePath, "*.*", SearchOption.AllDirectories), new ParallelOptions
            {
                MaxDegreeOfParallelism = 1
            }, dirPath =>
                    {
                        Folderİnformation folderInfo = new Folderİnformation();
                        folderInfo.FolderName = Path.GetFileName(dirPath);
                        folderInfo.subDirectoryFiles = Directory.GetFiles(dirPath);
                        folderInfo.FolderPath = dirPath;

                        lock (folderInformationLock)
                            folderInformations.Add(folderInfo);

                    });
            
            return (fileInformations, folderInformations);
        }
        public (List<Fileİnformation> files, List<Folderİnformation> folders) ScanOperation(string selectedFolder, int threadCount, int totalfiles)
        {
            processedFiles = 0;

            stopwatch = new Stopwatch();
            stopwatch.Start();

            var (fileInformations, folderInformations) = ScanFiles(selectedFolder, threadCount, totalfiles);

            stopwatch.Stop();

            return (fileInformations, folderInformations);

        }
    }
}
