using FileOrbis___File_System_Reporter.Date_Process;
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
        public void MoveOperation(string dateType, bool rdMoveCheck, bool chOverWriteCheck, string sourcePath, string targetPath, string selectedFileName, bool chEmptyFoldersCheck, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations)
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
                    MoveDirectoryByDate(sourceFolderPath, destinationFolderPath, dateType, chEmptyFoldersCheck, fileDate, selectedDate, fileInformations);
                    MessageBox.Show("Folder '" + sourceFolderPath + "' has been successfully moved from location '" + sourceFolderPath + "' to '" + destinationFolderPath + "'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during the folder copy operation: " + ex.Message, "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void MoveDirectoryByDate(string sourceFolder, string targetDirectory, string dateType, bool chEmptyFoldersCheck, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations)
        {
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            string[] files = Directory.GetFiles(sourceFolder);

            foreach (string file in files) 
            {
                fileDate = dt.GetDateType(dateType, file);

                if (fileDate > selectedDate)
                {
                    string fileName = Path.GetFileName(file);
                    string targetFile = Path.Combine(targetDirectory, fileName);
                    File.Move(file, targetFile);
                }
            }

            string[] subDirectoryies = Directory.GetDirectories(sourceFolder);
            foreach (string subDirectory in subDirectoryies)
            {
                string subDirectoryName = Path.GetFileName(subDirectory);
                string[] subDirectoryFiles = Directory.GetFiles(subDirectory);
                string targetSubDirectory = Path.Combine(targetDirectory, subDirectoryName);
                if (subDirectoryFiles.Length >= 1)
                {
                    MoveDirectoryByDate(subDirectory, targetSubDirectory, dateType, chEmptyFoldersCheck, fileDate, selectedDate, fileInformations);
                }
                else
                {
                    if (chEmptyFoldersCheck)
                    {
                        MoveDirectoryByDate(subDirectory, targetSubDirectory, dateType, chEmptyFoldersCheck, fileDate, selectedDate, fileInformations);
                    }
                    else
                        continue;
                }
            }

            Directory.Delete(sourceFolder, recursive: true);
        }

    }
}
