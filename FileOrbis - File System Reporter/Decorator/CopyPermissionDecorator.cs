using FileOrbis___File_System_Reporter.File_İnformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter.File_Process
{
    class CopyPermissionDecorator : Decorator
    {
        protected IFileOperation fileOperation;

        public CopyPermissionDecorator(IFileOperation fileOperation) : base(fileOperation)
        {
            this.fileOperation = fileOperation;
        }
        private void CopyPermission(string sourcePath, string targetPath, List<Fileİnformation> fileInformations)
        {
            foreach (Fileİnformation newPath in fileInformations)
            {
                FileInfo sourceFileInfo = new FileInfo(newPath.FilePath);
                FileSecurity sourceFileSecurity = sourceFileInfo.GetAccessControl();
                FileSecurity destFileSecurity = new FileSecurity();
                destFileSecurity.SetSecurityDescriptorBinaryForm(sourceFileSecurity.GetSecurityDescriptorBinaryForm());
                File.SetAccessControl(newPath.FilePath.Replace(sourcePath, targetPath), destFileSecurity);
            }
        }
        public override void Execute(string sourcePath, string targetPath, string selectedFileName, bool overwriteCheck, bool emptyFoldersCheck, bool copyPermission, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, IDateOptions dateOptions)
        {
            if (copyPermission)
            {
                CopyPermission(sourcePath, targetPath, fileInformations);
            }
            base.Execute(sourcePath, targetPath, selectedFileName, overwriteCheck, emptyFoldersCheck, copyPermission, fileDate, selectedDate, fileInformations, folderInformations, dateOptions);
        }
    }
}
