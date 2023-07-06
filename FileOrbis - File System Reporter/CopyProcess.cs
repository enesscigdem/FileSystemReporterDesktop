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
    public class CopyProcess
    {
        Form1 frm = new Form1();
        public CopyProcess(Form1 form)
        {
            frm = form;
        }
        public void CopyOperation(string sourcePath, string targetPath,string selectedFileName,bool OverWriteCheck, bool NtfsPermissionCheck,bool rdCopyCheck)
        {
            DeleteProcess deleteProcess = new DeleteProcess(frm);
            if (rdCopyCheck)
            {
                try
                {
                    bool copyPermissions = NtfsPermissionCheck;
                    string sourceFolderPath = sourcePath;
                    string destinationFolderPath = targetPath+ "\\" + selectedFileName;
                    if (OverWriteCheck)
                        deleteProcess.DeleteDirectory(destinationFolderPath); // overwrite işlemi .
                    CopyDirectory(sourceFolderPath, destinationFolderPath, copyPermissions);

                    MessageBox.Show("Folder '" + sourceFolderPath + "' has been successfully copied to the location '" + destinationFolderPath + "' and overwritten.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred during the folder copy operation: " + ex.Message, "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void CopyDirectory(string sourceDir, string destinationDir, bool copyPermissions)
        {
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            string[] files = Directory.GetFiles(sourceDir);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string destFile = Path.Combine(destinationDir, fileName);
                File.Copy(file, destFile);

                if (copyPermissions)
                {
                    FileSecurity sourceFileSecurity = File.GetAccessControl(file); // permission process
                    FileSecurity destFileSecurity = File.GetAccessControl(destFile);
                    destFileSecurity.SetSecurityDescriptorBinaryForm(sourceFileSecurity.GetSecurityDescriptorBinaryForm());
                    File.SetAccessControl(destFile, destFileSecurity);
                }
            }

            string[] subDirectories = Directory.GetDirectories(sourceDir);
            foreach (string subDir in subDirectories)
            {
                string dirName = Path.GetFileName(subDir);
                string destSubDir = Path.Combine(destinationDir, dirName);
                CopyDirectory(subDir, destSubDir, copyPermissions);

                if (copyPermissions)
                {
                    DirectorySecurity sourceDirSecurity = Directory.GetAccessControl(subDir);
                    DirectorySecurity destDirSecurity = Directory.GetAccessControl(destSubDir);
                    destDirSecurity.SetSecurityDescriptorBinaryForm(sourceDirSecurity.GetSecurityDescriptorBinaryForm());
                    Directory.SetAccessControl(destSubDir, destDirSecurity);
                }
            }
        }

    }
}
