using FileOrbis___File_System_Reporter.File_İnformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrbis___File_System_Reporter.File_Process
{
    public abstract class Decorator : IFileOperation
    {
        protected IFileOperation _fileOperation;

        public Decorator(IFileOperation fileOperation)
        {
            this._fileOperation= fileOperation;
        }

        public virtual void Execute(string sourcePath, string targetPath, string selectedFileName, bool overwriteCheck, bool copyPermission, bool emptyFoldersCheck, DateTime fileDate, DateTime selectedDate, List<Fileİnformation> fileInformations, List<Folderİnformation> folderInformations, IDateOptions dateOptions)
        {
            _fileOperation.Execute(sourcePath, targetPath, selectedFileName, overwriteCheck, copyPermission, emptyFoldersCheck, fileDate, selectedDate, fileInformations, folderInformations, dateOptions);
        }
    }
}