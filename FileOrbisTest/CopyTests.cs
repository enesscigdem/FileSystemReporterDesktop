using FileOrbis___File_System_Reporter;
using FileOrbis___File_System_Reporter.DateOptions;
using FileOrbis___File_System_Reporter.File_İnformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;

namespace FileOrbisTest
{
    [TestClass]
    public class CopyTests
    {
        string sourcePath = @"C:\Users\Eness\OneDrive\Desktop\test2";
        string targetPath = @"C:\Users\Eness\OneDrive\Desktop\test3";
        int ThreadCount = 1;
        int TotalFiles = 100;
        string selectedFileName = "test2", destinationFolderPath;
        List<Fileİnformation> fileInformations = new List<Fileİnformation>();
        List<Folderİnformation> folderInformations = new List<Folderİnformation>();
        DateTime filedate = DateTime.Now;
        DateTime selectedDate = DateTime.Now.AddDays(-7);
        IDateOptions dateOptions = new CreatedDateOption();
        CopyProcess copyProcess = new CopyProcess();
        ScanProcess scanProcess = new ScanProcess();

        [TestInitialize]
        public void ReturnFileİnformations()
        {
            scanProcess.EnableUIUpdates(false);
            (fileInformations, folderInformations) = scanProcess.ScanFiles(sourcePath, ThreadCount, TotalFiles);
        }

        [TestMethod]
        public void IsValid_SourcePath()
        {
            Assert.IsTrue(Directory.Exists(sourcePath));
        }

        [TestMethod]
        public void IsValid_TargetPath()
        {
            Assert.IsTrue(Directory.Exists(targetPath));
        }
        private void Validate_Copy_FilesAndFolders()
        {
            destinationFolderPath = Path.Combine(targetPath, selectedFileName);
            Assert.IsTrue(Directory.Exists(destinationFolderPath));

            foreach (Fileİnformation newPath in fileInformations)
            {
                if (filedate > selectedDate)
                {
                    string newFilePath = newPath.FilePath.Replace(sourcePath, destinationFolderPath);
                    Assert.IsTrue(File.Exists(newFilePath));
                }
            }

            foreach (Folderİnformation newPath in folderInformations)
            {
                if (filedate > selectedDate)
                {
                    string newFolderPath = newPath.FolderPath.Replace(sourcePath, destinationFolderPath);
                    Assert.IsTrue(Directory.Exists(newFolderPath));
                }
            }
        }

        [TestMethod]
        public void Is_Success_Copies_FilesAndFolders()
        {
            copyProcess.CopyOperation(sourcePath, targetPath, selectedFileName, false, false, fileInformations, folderInformations, filedate, selectedDate, false, dateOptions);
            Validate_Copy_FilesAndFolders();
        }

        [TestMethod]
        public void PermissionsEnable_Copy_FilesAndFolders()
        {
            copyProcess.CopyOperation(sourcePath, targetPath, selectedFileName, false, true, fileInformations, folderInformations, filedate, selectedDate, false, dateOptions);
            Validate_Copy_FilesAndFolders();
        }
        [TestMethod]
        public void OverWriteEnable_Copy_FilesAndFolders()
        {
            copyProcess.CopyOperation(sourcePath, targetPath, selectedFileName, true, false, fileInformations, folderInformations, filedate, selectedDate, false, dateOptions);
            Validate_Copy_FilesAndFolders();
        }
        [TestMethod]
        public void EmptyFolderEnable_Copy_FilesAndFolders()
        {
            copyProcess.CopyOperation(sourcePath, targetPath, selectedFileName, false, false, fileInformations, folderInformations, filedate, selectedDate, true, dateOptions);
            Validate_Copy_FilesAndFolders();
        }
        [TestMethod]
        public void EmptyFolder_OverWrite_Enable_Copy_FilesAndFolders()
        {
            copyProcess.CopyOperation(sourcePath, targetPath, selectedFileName, true, false, fileInformations, folderInformations, filedate, selectedDate, true, dateOptions);
            Validate_Copy_FilesAndFolders();
        }
        [TestMethod]
        public void EmptyFolder_Permission_Enable_Copy_FilesAndFolders()
        {
            copyProcess.CopyOperation(sourcePath, targetPath, selectedFileName, false, true, fileInformations, folderInformations, filedate, selectedDate, true, dateOptions);
            Validate_Copy_FilesAndFolders();
        }
        [TestMethod]
        public void OverWrite_Permission_Enable_Copy_FilesAndFolders()
        {
            copyProcess.CopyOperation(sourcePath, targetPath, selectedFileName, true, true, fileInformations, folderInformations, filedate, selectedDate, false, dateOptions);
            Validate_Copy_FilesAndFolders();
        }
    }
}
