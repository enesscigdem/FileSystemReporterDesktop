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
        DeleteProcess deleteProcess = new DeleteProcess();
        public void MoveOperation(string dateType, bool rdMoveCheck, bool chOverWriteCheck, string sourcePath, string targetPath, string selectedFileName, bool chEmptyFoldersCheck, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, IDateOptions dateOptions)
        {
            try
            {
                string sourceFolderPath = sourcePath;
                string destinationFolderPath = Path.Combine(targetPath, selectedFileName);
                if (chOverWriteCheck)
                {
                    if (Directory.Exists(destinationFolderPath))
                        deleteProcess.DeleteDirectory(destinationFolderPath, fileInformations);
                }
                MoveFiles(sourceFolderPath, destinationFolderPath, fileDate, selectedDate, dateType, chEmptyFoldersCheck, fileInformations, folderInformations, dateOptions);
                MessageBox.Show("Folder '" + sourceFolderPath + "' has been successfully moved from location '" + sourceFolderPath + "' to '" + destinationFolderPath + "'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during the folder move operation: " + ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MoveFiles(string sourcePath, string targetPath, DateTime fileDate, DateTime selectedDate, string dateType, bool chEmptyFoldersCheck, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, IDateOptions dateOptions)
        {
            foreach (Folderİnformation dirPath in folderInformations)
            {
                string targetDirPath = dirPath.FolderPath.Replace(sourcePath, targetPath);

                // burada 2. ve 3. koşullar için direk şunu kullanabilirsin. Directory.GetFileSystemEntries.count>0 
                if (chEmptyFoldersCheck || Directory.GetFileSystemEntries(dirPath.FolderPath).Count() > 0)
                {
                    fileDate = dateOptions.SetDate(dirPath.FolderPath);
                    if (fileDate > selectedDate)
                        Directory.CreateDirectory(targetDirPath);
                }
            }

            foreach (Fileİnformation newPath in fileInformations)
            {
                fileDate = dateOptions.SetDate(newPath.FilePath);
                if (fileDate > selectedDate)
                {
                    string newFilePath = newPath.FilePath.Replace(sourcePath, targetPath);
                    File.Move(newPath.FilePath, newFilePath);
                }
            }
            deleteProcess.DeleteDirectory(sourcePath, fileInformations);
        }
    }
}
