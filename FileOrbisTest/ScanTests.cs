using FileOrbis___File_System_Reporter;
using FileOrbis___File_System_Reporter.File_İnformation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace FileOrbisTest
{
    [TestClass]
    public class ScanTests
    {
        private const string SourcePath = "C:\\Users\\Eness\\OneDrive\\Desktop";
        private const int ThreadCount = 1; // Set a thread count
        private const int TotalFiles = 100; // Enter the total number of files of the path you will choose.
        private ScanProcess scanProcess;
        [TestInitialize]
        public void Initialize()
        {
            scanProcess = new ScanProcess();
        }
        [TestMethod]
        public void ReturnsFileFolderİnformation()
        {
            // Act
            var (fileInformations, folderInformations) = scanProcess.ScanFiles(SourcePath, ThreadCount, TotalFiles);

            // Assert
            Assert.IsTrue(fileInformations.Count > 0);
            Assert.IsTrue(folderInformations.Count > 0);

            Assert.IsTrue(Directory.Exists(SourcePath));
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
        public void InvalidSourcePath_ThrowsException()
        {
            string invalidSourcePath = "C:\\InvalidPath";

            Assert.ThrowsException<DirectoryNotFoundException>(() =>
            {
                scanProcess.ScanFiles(invalidSourcePath, ThreadCount, TotalFiles);
            });
        }
    }
}
