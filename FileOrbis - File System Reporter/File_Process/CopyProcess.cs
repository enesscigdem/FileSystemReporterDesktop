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
        public void CopyOperation(string sourcePath, string targetPath, string selectedFileName, bool OverWriteCheck, bool NtfsPermissionCheck, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, DateTime fileDate, DateTime selectedDate, bool chEmptyFoldersCheck, IDateOptions dateOptions)
        {

            bool copyPermissions = NtfsPermissionCheck;
            string sourceFolderPath = sourcePath;
            string destinationFolderPath = targetPath + "\\" + selectedFileName;
            if (!Directory.Exists(destinationFolderPath))
                Directory.CreateDirectory(destinationFolderPath);
            CopyFiles(sourceFolderPath, destinationFolderPath, copyPermissions, fileDate, selectedDate, chEmptyFoldersCheck, OverWriteCheck, fileInformations, folderInformations, dateOptions);

        }
        public void CopyFiles(string sourcePath, string targetPath, bool copyPermissions, DateTime fileDate, DateTime selectedDate, bool chEmptyFoldersCheck, bool OverWriteCheck, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, IDateOptions dateOptions)
        {
            try
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

                    if (fileDate > selectedDate && OverWriteCheck)
                        File.Copy(newPath.FilePath, newPath.FilePath.Replace(sourcePath, targetPath), true);
                    else if (fileDate > selectedDate && !OverWriteCheck)
                        File.Copy(newPath.FilePath, newPath.FilePath.Replace(sourcePath, targetPath), false);

                    if (copyPermissions)
                    {
                        FileInfo sourceFileInfo = new FileInfo(newPath.FilePath);
                        FileSecurity sourceFileSecurity = sourceFileInfo.GetAccessControl();
                        FileSecurity destFileSecurity = new FileSecurity();
                        destFileSecurity.SetSecurityDescriptorBinaryForm(sourceFileSecurity.GetSecurityDescriptorBinaryForm());
                        File.SetAccessControl(newPath.FilePath.Replace(sourcePath, targetPath), destFileSecurity);
                    }
                }
                MessageBox.Show("Folder '" + sourcePath + "' has been successfully copied to the location '" + targetPath + "'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
