﻿using DocumentFormat.OpenXml;
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
        string WhListBox;
        int processedFiles, totalFiles;
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
        public (List<Fileİnformation> files, List<Folderİnformation> folders) ScanFiles(string sourcePath, DateTime dateTime, string checkedDate, DateTime fileDate, int threadCount)
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
                    fileInformations.Add(fileInfo);

                processedFiles++;

                if (processedFiles % Math.Max(totalFiles / 10, 1) == 0 || processedFiles == totalFiles)
                {
                    ProgressBarCallBack?.Invoke(processedFiles, totalFiles); // callback

                    lblScannedMessage?.Invoke(processedFiles, totalFiles);

                    lblPathMessage?.Invoke(fileInfo.FilePath);

                    Application.DoEvents();

                    lblTotalTımeCallBack?.Invoke(stopwatch);
                }

            });

            //ProgressBarCallBack?.Invoke(processedFiles, totalFiles); // callback
            //lblScannedMessage?.Invoke(processedFiles, totalFiles);
            //lblPathMessage?.Invoke(sourcePath);
            //lblTotalTımeCallBack?.Invoke(stopwatch);

            MessageBox.Show("Dosyalar bitti");
            Parallel.ForEach(Directory.GetDirectories(sourcePath, "*.*", SearchOption.AllDirectories), new ParallelOptions { MaxDegreeOfParallelism = threadCount }, dirPath =>
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
        public (List<Fileİnformation> files, List<Folderİnformation> folders) ScanOperation(string selectedFolder, DateTime dateTime, string checkedDate, DateTime fileDate, int threadCount)
        {
            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                try
                {
                    //string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);
                    //string[] subDirectories = Directory.GetDirectories(selectedFolder, "*", SearchOption.AllDirectories);

                    totalFiles = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories).Count();
                    processedFiles = 0;

                    stopwatch = new Stopwatch();
                    stopwatch.Start();

                    var (fileInformations, folderInformations) = ScanFiles(selectedFolder, dateTime, checkedDate, fileDate, threadCount);

                    stopwatch.Stop();

                    return (fileInformations, folderInformations);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return (null, null);
                }

            }
            else
            {
                MessageBox.Show("Please select a valid folder.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return (null, null);
            }
        }
    }
}