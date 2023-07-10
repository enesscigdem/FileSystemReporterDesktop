using FileOrbis___File_System_Reporter.Date_Process;
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
        DateType dt = new DateType();
        public void CopyOperation(string sourcePath, string targetPath, string selectedFileName, bool OverWriteCheck, bool NtfsPermissionCheck, bool rdCopyCheck, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, DateTime fileDate, DateTime selectedDate, string dateType, bool chEmptyFoldersCheck)
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
                    CopyDirectory(sourceFolderPath, destinationFolderPath, copyPermissions, fileInformations, folderInformations, fileDate, selectedDate, dateType, chEmptyFoldersCheck);

                    MessageBox.Show("Folder '" + sourceFolderPath + "' has been successfully copied to the location '" + destinationFolderPath + "' and overwritten.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during the folder copy operation: " + ex.Message, "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void CopyDirectory(string sourceDir, string destinationDir, bool copyPermissions, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, DateTime fileDate, DateTime selectedDate, string dateType, bool chEmptyFoldersCheck)
        {
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            foreach (Folderİnformation folderInfo in folderInformations)
            {
                fileDate = dt.GetDateType(dateType, folderInfo.FolderPath);

                if (fileDate > selectedDate)
                {
                    string fileName = Path.GetFileName(folderInfo.FolderPath);
                    string targetFile = Path.Combine(destinationDir, fileName);
                    string subDirectoryName = Path.GetFileName(folderInfo.FolderPath);
                    string[] subDirectoryFiles = Directory.GetFiles(folderInfo.FolderPath);
                    string targetSubDirectory = Path.Combine(destinationDir, subDirectoryName);

                    if (!Directory.Exists(targetFile))
                        Directory.CreateDirectory(targetFile);
                    if (subDirectoryFiles.Length == 0 && !chEmptyFoldersCheck)
                        Directory.Delete(targetFile);
                    for (int i = 0; i < subDirectoryFiles.Length; i++)
                    {
                        string subFileName = Path.GetFileName(subDirectoryFiles[i]);
                        File.Copy(subDirectoryFiles[i], targetSubDirectory + "\\" + subFileName);

                        if (copyPermissions)
                        {
                            FileSecurity sourceFileSecurity = File.GetAccessControl(subDirectoryFiles[i]);
                            FileSecurity destFileSecurity = File.GetAccessControl(targetSubDirectory + "\\" + subFileName);
                            destFileSecurity.SetSecurityDescriptorBinaryForm(sourceFileSecurity.GetSecurityDescriptorBinaryForm());
                            File.SetAccessControl(targetSubDirectory + "\\" + subFileName, destFileSecurity);
                        }
                    }
                    if (copyPermissions && chEmptyFoldersCheck)
                    {
                        DirectorySecurity sourceDirSecurity = Directory.GetAccessControl(folderInfo.FolderPath);
                        DirectorySecurity destDirSecurity = Directory.GetAccessControl(targetSubDirectory);
                        destDirSecurity.SetSecurityDescriptorBinaryForm(sourceDirSecurity.GetSecurityDescriptorBinaryForm());
                        Directory.SetAccessControl(targetSubDirectory, destDirSecurity);
                    }
                }
            }
        }

    }
}
