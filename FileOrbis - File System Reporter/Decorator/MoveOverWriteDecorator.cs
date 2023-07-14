using FileOrbis___File_System_Reporter.File_İnformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileOrbis___File_System_Reporter.File_Process
{
    internal class MoveOverWriteDecorator: Decorator
    {
        protected IFileOperation fileOperation;

        private bool overwriteCheck;

        public MoveOverWriteDecorator(IFileOperation fileOperation, bool overwriteCheck) : base(fileOperation)
        {
            this.overwriteCheck = overwriteCheck;
        }

        public override void Execute(string sourcePath, string targetPath, string selectedFileName, bool overwriteCheck, bool copyPermission, bool emptyFoldersCheck, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, IDateOptions dateOptions)
        {
            if (this.overwriteCheck)
            {
                OverWriteMove(targetPath);
            }

            base.Execute(sourcePath, targetPath, selectedFileName, overwriteCheck, copyPermission, emptyFoldersCheck, fileDate, selectedDate, fileInformations, folderInformations, dateOptions);
        }

        private void OverWriteMove(string targetPath)
        {
            if (Directory.Exists(targetPath))
            {
                DeleteProcess deleteProcess = new DeleteProcess();
                deleteProcess.DeleteDirectory(targetPath);
            }
        }
    }
}
