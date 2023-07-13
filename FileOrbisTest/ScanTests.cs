using FileOrbis___File_System_Reporter;
using FileOrbis___File_System_Reporter.File_İnformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Windows.Forms;

namespace FileOrbisTest
{
    [TestClass]
    public class ScanTests
    {
        string SourcePath = "C:\\Users\\Eness\\OneDrive\\Desktop\\büyük dosya";
        int ThreadCount = 1; // Set a thread count
        int TotalFiles = 6256; // Enter the total number of files of the path you will choose.
        ScanProcess scanProcess = new ScanProcess();

        [TestMethod]
        public void ReturnsFileCountValid()
        {
            // Act
            var (fileInformations, _) = scanProcess.ScanFiles(SourcePath, ThreadCount, TotalFiles);
            // Assert
            Assert.IsTrue(fileInformations.Count > 0);
        }
        [TestMethod]
        public void ReturnsFolderCountValid()
        {
            var (_, folderInformations) = scanProcess.ScanFiles(SourcePath, ThreadCount, TotalFiles);
            Assert.IsTrue(folderInformations.Count > 0);
        }
        [TestMethod]
        public void ValidSourcePath()
        {
            Assert.IsTrue(Directory.Exists(SourcePath));
        }
        [TestMethod]
        public void ReturnsFileIsNotNull()
        {
            var (fileInformations, _) = scanProcess.ScanFiles(SourcePath, ThreadCount, TotalFiles);
            Assert.IsNotNull(fileInformations);

        }
        [TestMethod]
        public void ReturnsFolderIsNotNull()
        {
            var (_, folderInformations) = scanProcess.ScanFiles(SourcePath, ThreadCount, TotalFiles);
            Assert.IsNotNull(folderInformations);
        }
        [TestMethod]
        public void ReturnsExpectedFileCount()
        {
            var (fileInformations, _) = scanProcess.ScanFiles(SourcePath, ThreadCount, TotalFiles);
            Assert.AreEqual(TotalFiles, fileInformations.Count);
        }
        [TestMethod]
        public void ReturnsExpectedFolderCount()
        {
            var (_, folderInformations) = scanProcess.ScanFiles(SourcePath, ThreadCount, TotalFiles);
            Assert.IsTrue(folderInformations.Count > 0);
        }
        [TestMethod]
        public void InvalidSourcePath()
        {
            string invalidSourcePath = "C:\\InvalidPath";

            Assert.ThrowsException<DirectoryNotFoundException>(() =>
            {
                scanProcess.ScanFiles(invalidSourcePath, ThreadCount, TotalFiles);
            });
        }
    }
}
