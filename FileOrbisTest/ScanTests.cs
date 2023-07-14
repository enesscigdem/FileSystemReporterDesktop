using FileOrbis___File_System_Reporter;
using FileOrbis___File_System_Reporter.File_İnformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FileOrbisTest
{
    [TestClass]
    public class ScanTests
    {
        string SourcePath = @"C:\Users\Eness\OneDrive\Desktop\büyük dosya";
        int ThreadCount = 1000; // Set a thread count
        int TotalFiles = 6256; // Enter the total number of files of the path you will choose.
        int FileCount, FolderCount;

        List<Fileİnformation> fileInformations;
        List<Folderİnformation> folderInformations;
        ScanProcess scanProcess = new ScanProcess();

        [TestInitialize]
        public void Returns_İnformations()
        {
            scanProcess.EnableUIUpdates(false);
            (fileInformations, folderInformations) = scanProcess.ScanFiles(SourcePath, ThreadCount, TotalFiles);
            FileCount = fileInformations.Count;
            FolderCount = folderInformations.Count;
        }
        [TestMethod]
        public void Returns_File_Count_Valid()
        {
            Assert.IsTrue(FileCount > 0);
        }
        [TestMethod]
        public void Returns_Folder_Count_Valid()
        {
            Assert.IsTrue(FolderCount > 0);
        }
        [TestMethod]
        public void Valid_Source_Path()
        {
            Assert.IsTrue(Directory.Exists(SourcePath));
        }
        [TestMethod]
        public void Returns_File_Is_Not_Null()
        {
            Assert.IsNotNull(fileInformations);
        }
        [TestMethod]
        public void Returns_Folder_Is_Not_Null()
        {
            Assert.IsNotNull(folderInformations);
        }
        [TestMethod]
        public void Returns_Expected_File_Count()
        {
            Assert.AreEqual(TotalFiles, fileInformations.Count);
        }
        [TestMethod]
        public void Invalid_Source_Path()
        {
            string invalidSourcePath = "C:\\InvalidPath";

            Assert.ThrowsException<DirectoryNotFoundException>(() =>
            {
                scanProcess.ScanFiles(invalidSourcePath, ThreadCount, TotalFiles);
            });
        }
    }
}
