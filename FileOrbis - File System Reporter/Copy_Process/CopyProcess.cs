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

            // İlk klasörün içindeki dosyaları kopyala

            foreach (Folderİnformation folderInfo in folderInformations)
            {
                CopyFolderContents(folderInfo.FolderPath, destinationDir, selectedDate, dateType, chEmptyFoldersCheck, copyPermissions);
            }
        }

        private void CopyFolderContents(string sourceDir, string targetDir, DateTime selectedDate, string dateType, bool chEmptyFoldersCheck, bool copyPermissions)
        {
            DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(sourceDir);

            if (!sourceDirectoryInfo.Exists)
                return;

            DirectoryInfo targetDirectoryInfo = Directory.CreateDirectory(Path.Combine(targetDir, sourceDirectoryInfo.Name));

            foreach (FileInfo file in sourceDirectoryInfo.GetFiles())
            {
                DateTime fileDate = dt.GetDateType(dateType, file.FullName);

                if (fileDate > selectedDate)
                {
                    string targetFilePath = Path.Combine(targetDirectoryInfo.FullName, file.Name);
                    file.CopyTo(targetFilePath, true);

                    if (copyPermissions)
                    {
                        FileSecurity sourceFileSecurity = file.GetAccessControl();
                        FileSecurity destFileSecurity = new FileSecurity();
                        destFileSecurity.SetSecurityDescriptorBinaryForm(sourceFileSecurity.GetSecurityDescriptorBinaryForm());
                        File.SetAccessControl(targetFilePath, destFileSecurity);
                    }
                }
            }

            foreach (DirectoryInfo subDirectory in sourceDirectoryInfo.GetDirectories())
            {
                CopyFolderContents(subDirectory.FullName, targetDirectoryInfo.FullName, selectedDate, dateType, chEmptyFoldersCheck, copyPermissions);
            }
        }
    }
}
