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
        string selectedFileName = "test2";
        bool OverWriteCheck = false;
        bool ntfsPermissionCheck = false;
        bool chEmptyFoldersCheck = false;
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

        [TestMethod]
        public void IsSuccesscopiesFilesAndFolders()
        {
            Assert.IsTrue(Directory.Exists(sourcePath) && Directory.Exists(targetPath));
            copyProcess.CopyOperation(sourcePath, targetPath, selectedFileName, false, false, fileInformations, folderInformations, filedate, selectedDate, false, dateOptions);

        }

        [TestMethod]
        public void CopyPermissionsEnable()
        {
            Assert.IsTrue(Directory.Exists(sourcePath) && Directory.Exists(targetPath));
            copyProcess.CopyOperation(sourcePath, targetPath, selectedFileName, false, true, fileInformations, folderInformations, filedate, selectedDate, false, dateOptions);
        }
    }
}
