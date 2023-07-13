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
        public void CopiesFilesAndFolders()
        {
            Assert.IsTrue(Directory.Exists(sourcePath) && Directory.Exists(targetPath));
            copyProcess.CopyFiles(sourcePath, targetPath, ntfsPermissionCheck, filedate, selectedDate, chEmptyFoldersCheck, OverWriteCheck, fileInformations, folderInformations, dateOptions);

            foreach (Fileİnformation file in fileInformations)
            {
                string targetFilePath = file.FilePath.Replace(sourcePath, targetPath);
                Assert.IsTrue(File.Exists(targetFilePath));
            }

            foreach (Folderİnformation folder in folderInformations)
            {
                string targetFolderPath = folder.FolderPath.Replace(sourcePath, targetPath);
                Assert.IsTrue(Directory.Exists(targetFolderPath));
            }
        }

        [TestMethod]
        public void CopyPermissionsEnable()
        {
            Assert.IsTrue(Directory.Exists(sourcePath) && Directory.Exists(targetPath));
            ntfsPermissionCheck = true; chEmptyFoldersCheck = true; OverWriteCheck = true;
            copyProcess.CopyFiles(sourcePath, targetPath, ntfsPermissionCheck, filedate, selectedDate, chEmptyFoldersCheck, OverWriteCheck, fileInformations, folderInformations, dateOptions);
            foreach (Fileİnformation file in fileInformations)
            {
                string targetFilePath = file.FilePath.Replace(sourcePath, targetPath);
                Assert.IsTrue(File.Exists(targetFilePath)); // Check if copied file exists

                FileInfo sourceFileInfo = new FileInfo(file.FilePath);
                FileInfo targetFileInfo = new FileInfo(targetFilePath);
                FileSecurity sourceFileSecurity = sourceFileInfo.GetAccessControl();
                FileSecurity targetFileSecurity = targetFileInfo.GetAccessControl();

                CollectionAssert.AreEqual(sourceFileSecurity.GetSecurityDescriptorBinaryForm(), targetFileSecurity.GetSecurityDescriptorBinaryForm());
            }

            foreach (Folderİnformation folder in folderInformations)
            {
                string targetFolderPath = folder.FolderPath.Replace(sourcePath, targetPath);
                Assert.IsTrue(Directory.Exists(targetFolderPath)); // Check if copied folder exists

                DirectoryInfo sourceDirInfo = new DirectoryInfo(folder.FolderPath);
                DirectoryInfo targetDirInfo = new DirectoryInfo(targetFolderPath);
                DirectorySecurity sourceDirSecurity = sourceDirInfo.GetAccessControl();
                DirectorySecurity targetDirSecurity = targetDirInfo.GetAccessControl();

                CollectionAssert.AreEqual(sourceDirSecurity.GetSecurityDescriptorBinaryForm(), targetDirSecurity.GetSecurityDescriptorBinaryForm());
            }
        }
    }
}
