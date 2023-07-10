using FileOrbis___File_System_Reporter.Date_Process;
using FileOrbis___File_System_Reporter.File_İnformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileOrbis___File_System_Reporter
{
    public class MoveProcess
    {
        DateType dt = new DateType();
        public void MoveOperation(string dateType, bool rdMoveCheck, bool chOverWriteCheck, string sourcePath, string targetPath, string selectedFileName, bool chEmptyFoldersCheck, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations)
        {
            if (rdMoveCheck)
            {
                DeleteProcess deleteProcess = new DeleteProcess();
                try
                {
                    string sourceFolderPath = sourcePath;
                    string destinationFolderPath = Path.Combine(targetPath, selectedFileName);
                    if (chOverWriteCheck)
                    {
                        if (Directory.Exists(destinationFolderPath))
                            deleteProcess.DeleteDirectory(destinationFolderPath, fileInformations);
                    }
                    MoveDirectoryByDate(sourceFolderPath, destinationFolderPath, dateType, chEmptyFoldersCheck, fileDate, selectedDate, fileInformations, folderInformations);
                    MessageBox.Show("Folder '" + sourceFolderPath + "' has been successfully moved from location '" + sourceFolderPath + "' to '" + destinationFolderPath + "'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during the folder move operation: " + ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void MoveDirectoryByDate(string sourceFolder, string targetDirectory, string dateType, bool chEmptyFoldersCheck, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations)
        {
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            foreach (Folderİnformation folderInfo in folderInformations)
            {
                MoveFolderContents(sourceFolder,folderInfo.FolderPath, Path.Combine(targetDirectory, folderInfo.FolderName), dateType, chEmptyFoldersCheck, fileDate, selectedDate, fileInformations, folderInformations);
            }

            if (Directory.GetFileSystemEntries(sourceFolder).Length == 0)
            {
                Directory.Delete(sourceFolder);
            }
        }

        private void MoveFolderContents(string sourceFolder,string sourceDir, string targetDir, string dateType, bool chEmptyFoldersCheck, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations)
        {
            DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(sourceDir);
            DirectoryInfo targetDirectoryInfo = Directory.CreateDirectory(targetDir);

            try
            {
                foreach (FileInfo file in sourceDirectoryInfo.GetFiles())
                {
                    fileDate = dt.GetDateType(dateType, file.FullName);

                    if (fileDate > selectedDate)
                    {
                        string targetFilePath = Path.Combine(targetDirectoryInfo.FullName, file.Name);
                        file.CopyTo(targetFilePath, true);
                        file.Delete();
                    }
                }
            }
            catch
            {
                Directory.Delete(sourceFolder);
            }


            foreach (DirectoryInfo subDirectory in sourceDirectoryInfo.GetDirectories())
            {
                string subDirectoryName = subDirectory.Name;
                string targetSubDirectory = Path.Combine(targetDirectoryInfo.FullName, subDirectoryName);

                MoveFolderContents(sourceFolder, subDirectory.FullName, targetSubDirectory, dateType, chEmptyFoldersCheck, fileDate, selectedDate, fileInformations, folderInformations);

                //if (Directory.GetFileSystemEntries(subDirectory.FullName).Length == 0)
                //{
                //    subDirectory.Delete();
                //}
            }

            if (Directory.GetFileSystemEntries(sourceDir).Length == 0)
            {
                sourceDirectoryInfo.Delete();
            }
        }

    }
}
