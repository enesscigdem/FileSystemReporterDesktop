using FileOrbis___File_System_Reporter;
using FileOrbis___File_System_Reporter.DateOptions;
using FileOrbis___File_System_Reporter.File_İnformation;
using FileOrbis___File_System_Reporter.File_Process;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace FileOrbisTest
{
    [TestClass]
    public class MoveTests
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
        IFileOperation moveProcess = new MoveProcess();
        ScanProcess scanProcess = new ScanProcess();

        [TestInitialize]
        public void ReturnFileİnformations()
        {
            scanProcess.EnableUIUpdates(false);
            //Assert.IsTrue(Directory.Exists(sourcePath) && Directory.Exists(targetPath));
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

        private void ValidateMovedFilesAndFolders()
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
                    if (newPath.subDirectoryFiles.Count() != 0)
                    {
                        string newFolderPath = newPath.FolderPath.Replace(sourcePath, destinationFolderPath);
                        Assert.IsTrue(Directory.Exists(newFolderPath));
                    }

                }
            }
        }

        [TestMethod]
        public void IsSuccess_Moves_FilesAndFolders()
        {
            moveProcess.Execute(sourcePath,targetPath,selectedFileName,false,false,false,filedate,selectedDate,fileInformations,folderInformations,dateOptions);
            ValidateMovedFilesAndFolders(); // Doğru bir şekilde taşındığını doğruluyoruz.
        }

        [TestMethod]
        public void OverWriteEnable_Move_FilesAndFolders()
        {
            moveProcess.Execute(sourcePath,targetPath,selectedFileName, true, false,false,filedate,selectedDate,fileInformations,folderInformations,dateOptions);
            ValidateMovedFilesAndFolders();
        }

        [TestMethod]
        public void EmptyFolderEnable_Move_FilesAndFolders()
        {
            moveProcess.Execute(sourcePath,targetPath,selectedFileName,false,false, true, filedate,selectedDate,fileInformations,folderInformations,dateOptions);
            ValidateMovedFilesAndFolders();
        }

        [TestMethod]
        public void EmptyFolder_OverWrite_Enable_Move_FilesAndFolders()
        {
            moveProcess.Execute(sourcePath,targetPath,selectedFileName, true, false, true, filedate,selectedDate,fileInformations,folderInformations,dateOptions);
            ValidateMovedFilesAndFolders();
        }
        [TestMethod]
        public void Is_Source_Folder_Deleted_AfterMove()
        {
            moveProcess.Execute(sourcePath,targetPath,selectedFileName,false,false,false,filedate,selectedDate,fileInformations,folderInformations,dateOptions);
            Assert.IsFalse(Directory.Exists(sourcePath));
        }
    }
}
