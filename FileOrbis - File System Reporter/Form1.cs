using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileOrbis___File_System_Reporter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public DateTime selectedDate, createDate, modifiedDate, accessDate, fileDate;
        public string fileName, fileDirectory, selectedFileName;
        public long fileSize;
        public void GetDateType(string dateType, string file)
        {
            //Move Directory and Excell Process functions use this func.
            selectedDate = dtDateOption.Value;
            switch (dateType)
            {
                case "Created":
                    fileDate = File.GetCreationTime(file);
                    break;
                case "Modified":
                    fileDate = File.GetLastWriteTime(file);
                    break;
                case "Accessed":
                    fileDate = File.GetLastAccessTime(file);
                    break;
                default:
                    throw new ArgumentException("Invalid date type.");
            }
        }

        #region Disabled Checked Radio Buttons,CheckBoxs
        public void DisabledChecked()
        {
            txtTargetPath.Enabled = false;
            btnTargetPath.Enabled = false;
            rdCreatedDate.Checked = true;
            rdScan.Checked = true;
            rdTxt.Checked = true;
            chEmptyFolders.Enabled = false;
            chNtfsPermission.Enabled = false;
            chOverWrite.Enabled = false;
        }
        #endregion

        //For Move,Copy
        #region Enabled Checked Radio Buttons,CheckBoxs
        public void EnabledChecked()
        {
            txtTargetPath.Enabled = true;
            btnTargetPath.Enabled = true;
            chEmptyFolders.Enabled = true;
            chNtfsPermission.Enabled = true;
            chOverWrite.Enabled = true;
        }
        public void IsItDoneScan()
        {
            rdMove.Enabled = true;
            rdCopy.Enabled = true;
        }
        #endregion

        #region view to listbox
        public string GetListBoxItem(string fileDirectory, string fileName, DateTime createDate, DateTime modifiedDate, DateTime accessDate, long fileSize)
        {
            string listItem = $"{fileDirectory + "\\" + fileName}";
            listItem += $"\n  Create Date: {createDate}";
            listItem += $"\n  Modified Date: {modifiedDate}";
            listItem += $"\n  Access Date: {accessDate}";
            listItem += $"\n  File Size (bytes): {fileSize}";

            return listItem;
        }
       
        public void Fileİnformations(string file)
        {
            fileName = Path.GetFileName(file);
            fileDirectory = Path.GetDirectoryName(file);
            createDate = File.GetCreationTime(file);
            modifiedDate = File.GetLastWriteTime(file);
            accessDate = File.GetLastAccessTime(file);
            fileSize = new FileInfo(file).Length;
        }
        #endregion

        #region Get Selected Data Type
        public string GetSelectedDateType()
        {
            if (rdCreatedDate.Checked)
                return "Created";
            else if (rdModifiedDate.Checked)
                return "Modified";
            else if (rdAccessedDate.Checked)
                return "Accessed";
            else
                throw new ArgumentException("The date type is not selected.");
        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            dtDateOption.Format = DateTimePickerFormat.Custom;
            dtDateOption.CustomFormat = "dd MMMM yyyy h:mm";
            DisabledChecked();
            rdMove.Enabled = false;
            rdCopy.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            #region Select a path for scan
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            DialogResult result = folderDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selectedFolder = folderDialog.SelectedPath;
                selectedFileName = Path.GetFileName(selectedFolder);
                txtSourcePath.Text = selectedFolder;
            }
            #endregion
        }

        private void button3_Click(object sender, EventArgs e)
        {
            #region Scan Process 
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            if (rdScan.Checked)
            {
                ScanProcess scanProcess = new ScanProcess(this); // Pass the form instance
                string selectedFolder = txtSourcePath.Text;
                if (rdCreatedDate.Checked == true)
                    scanProcess.ScanOperation(selectedFolder, "Created");
                if (rdModifiedDate.Checked == true)
                    scanProcess.ScanOperation(selectedFolder, "Accessed");
                if (rdAccessedDate.Checked == true)
                    scanProcess.ScanOperation(selectedFolder, "Modified");
            }

            #endregion

            #region MoveProcess
            if (rdMove.Checked)
            {
                MoveProcess moveProcess = new MoveProcess(this);
                if (chOverWrite.Checked)
                {
                    try
                    {
                        string sourceFolderPath = txtSourcePath.Text;
                        string destinationFolderPath = txtTargetPath.Text + "\\" + selectedFileName;
                        if (Directory.Exists(destinationFolderPath))
                            DeleteDirectory(destinationFolderPath);

                        moveProcess.MoveDirectoryByDate(sourceFolderPath, destinationFolderPath, GetSelectedDateType());
                        MessageBox.Show("Folder '" + sourceFolderPath + "' has been successfully moved from location '" + sourceFolderPath + "' to '" + destinationFolderPath + "'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred during the folder copy operation: " + ex.Message, "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        string sourceFolderPath = txtSourcePath.Text;
                        string destinationFolderPath = txtTargetPath.Text + "\\" + selectedFileName;
                        Directory.CreateDirectory(destinationFolderPath);
                        moveProcess.MoveDirectoryByDate(sourceFolderPath, destinationFolderPath, GetSelectedDateType());
                        MessageBox.Show("Folder '" + sourceFolderPath + "' has been successfully moved from location '" + sourceFolderPath + "' to '" + destinationFolderPath + "'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred during the folder copy operation: " + ex.Message, "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            #endregion

            #region Copy Process
            if (rdCopy.Checked)
            {
                if (chOverWrite.Checked)
                {
                    try
                    {
                        bool copyPermissions = chNtfsPermission.Checked;
                        string sourceFolderPath = txtSourcePath.Text;
                        string destinationFolderPath = txtTargetPath.Text + "\\" + selectedFileName;

                        DeleteDirectory(destinationFolderPath); // overwrite işlemi .
                        CopyDirectory(sourceFolderPath, destinationFolderPath, copyPermissions);

                        MessageBox.Show("Folder '" + sourceFolderPath + "' has been successfully copied to the location '" + destinationFolderPath + "' and overwritten.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred during the folder copy operation: " + ex.Message, "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        bool copyPermissions = chNtfsPermission.Checked;
                        string sourceFolderPath = txtSourcePath.Text;
                        string destinationFolderPath = txtTargetPath.Text + "\\" + selectedFileName;

                        CopyDirectory(sourceFolderPath, destinationFolderPath, copyPermissions);
                        MessageBox.Show("Folder '" + sourceFolderPath + "' has been copied to the location '" + destinationFolderPath + "'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred during the folder copy operation: " + ex.Message, "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            #endregion
        }

        #region Copy Directory

        private void CopyDirectory(string sourceDir, string destinationDir, bool copyPermissions)
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

        #endregion
        #region Delete Directory
        public void DeleteDirectory(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string directory in directories)
            {
                DeleteDirectory(directory);
            }

            Directory.Delete(path, false);
        }

        #endregion
        private void button4_Click(object sender, EventArgs e)
        {
            #region report process 
            if (rdTxt.Checked)
            {
                TxtProcess txtProcess = new TxtProcess();
                txtProcess.SaveTxt(txtSourcePath.Text,fileDirectory,fileName,createDate,modifiedDate,accessDate,fileSize);
            }
            else if (rdExcel.Checked)
            {
                ExcelProcess excelProcess = new ExcelProcess();
                excelProcess.SaveExcel(txtSourcePath.Text,GetSelectedDateType());
            }
            else
            {
                MessageBox.Show("Please select an option.", "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            #region Select a path for move 
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string selectedFolderMove = folderDialog.SelectedPath;
                txtTargetPath.Text = selectedFolderMove;
            }
            #endregion
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMove.Checked)
                EnabledChecked();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (rdScan.Checked)
                DisabledChecked();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCopy.Checked)
                EnabledChecked();
        }
    }
}
