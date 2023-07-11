using FileOrbis___File_System_Reporter.Date_Process;
using FileOrbis___File_System_Reporter.DateOptions;
using FileOrbis___File_System_Reporter.File_İnformation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileOrbis___File_System_Reporter
{
    public class CopyProcess
    {
        DateType dt = new DateType(); // burda bir kere oluşturduktan sonra copy işlemi yaparken döngü içinde dt den gelen değeri dt.CreatedDateOption yapacaksın örneğin //
        public void CopyOperation(string sourcePath, string targetPath, string selectedFileName, bool OverWriteCheck, bool NtfsPermissionCheck, bool rdCopyCheck, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, DateTime fileDate, DateTime selectedDate, string dateType, bool chEmptyFoldersCheck, IDateOptions dateOptions)
        {
            DeleteProcess deleteProcess = new DeleteProcess();
            if (rdCopyCheck)
            {
                try
                {
                    bool copyPermissions = NtfsPermissionCheck;
                    string sourceFolderPath = sourcePath;
                    string destinationFolderPath = targetPath + "\\" + selectedFileName;
                    if (OverWriteCheck)
                        deleteProcess.DeleteDirectory(destinationFolderPath, fileInformations); // overwrite işlemi .
                    CopyFiles(sourceFolderPath, destinationFolderPath, copyPermissions, fileDate, selectedDate, dateType, chEmptyFoldersCheck, fileInformations, folderInformations, dateOptions);

                    MessageBox.Show("Folder '" + sourceFolderPath + "' has been successfully copied to the location '" + destinationFolderPath + "'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during the folder copy operation: " + ex.Message, "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void CopyFiles(string sourcePath, string targetPath, bool copyPermissions, DateTime fileDate, DateTime selectedDate, string dateType, bool chEmptyFoldersCheck, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, IDateOptions dateOptions)
        {
            foreach (Folderİnformation dirPath in folderInformations)
            {
                string targetDirPath = dirPath.FolderPath.Replace(sourcePath, targetPath);

                if (chEmptyFoldersCheck || Directory.GetFiles(dirPath.FolderPath).Length > 0 || Directory.GetDirectories(dirPath.FolderPath).Length > 0)
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
                    File.Copy(newPath.FilePath, newPath.FilePath.Replace(sourcePath, targetPath), true);

                if (copyPermissions)
                {
                    FileInfo sourceFileInfo = new FileInfo(newPath.FilePath);
                    FileSecurity sourceFileSecurity = sourceFileInfo.GetAccessControl();
                    FileSecurity destFileSecurity = new FileSecurity();
                    destFileSecurity.SetSecurityDescriptorBinaryForm(sourceFileSecurity.GetSecurityDescriptorBinaryForm());
                    File.SetAccessControl(newPath.FilePath.Replace(sourcePath, targetPath), destFileSecurity);
                }
            }
        }
    }
}
