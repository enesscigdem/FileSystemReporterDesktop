using FileOrbis___File_System_Reporter.Date_Process;
using FileOrbis___File_System_Reporter.File_İnformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    string destinationFolderPath = targetPath + "\\" + selectedFileName;
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
                    MessageBox.Show("An error occurred during the folder copy operation: " + ex.Message, "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                fileDate = dt.GetDateType(dateType, folderInfo.FolderPath);

                if (fileDate > selectedDate)
                {
                    string fileName = Path.GetFileName(folderInfo.FolderPath);
                    string targetFile = Path.Combine(targetDirectory, fileName);
                    string subDirectoryName = Path.GetFileName(folderInfo.FolderPath);
                    string[] subDirectoryFiles = Directory.GetFiles(folderInfo.FolderPath);
                    string targetSubDirectory = Path.Combine(targetDirectory, subDirectoryName);

                    if (!Directory.Exists(targetFile))
                        Directory.CreateDirectory(targetFile);
                    if (subDirectoryFiles.Length == 0 && !chEmptyFoldersCheck)
                        Directory.Delete(targetFile);
                    for (int i = 0; i < subDirectoryFiles.Length; i++)
                    {
                        string subFileName = Path.GetFileName(subDirectoryFiles[i]);
                        File.Move(subDirectoryFiles[i], targetSubDirectory + "\\" + subFileName);
                        File.Delete(subDirectoryFiles[i]);
                    }
                    Directory.Delete(folderInfo.FolderPath);
                }
            }
            Directory.Delete(sourceFolder);

        }
    }
}
