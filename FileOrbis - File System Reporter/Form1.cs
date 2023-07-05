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

        public DateTime fileDate;
        public DateTime selectedDate;
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
        #region Scan Process and view to listbox
        public string GetListBoxItem(string fileDirectory, string fileName, DateTime createDate, DateTime modifiedDate, DateTime accessDate, long fileSize)
        {
            string listItem = $"{fileDirectory + "\\" + fileName}";
            listItem += $"\n  Create Date: {createDate}";
            listItem += $"\n  Modified Date: {modifiedDate}";
            listItem += $"\n  Access Date: {accessDate}";
            listItem += $"\n  File Size (bytes): {fileSize}";

            return listItem;
        }
        public string fileName, fileDirectory;
        public DateTime createDate, modifiedDate, accessDate;
        public long fileSize;
        public void Fileİnformations(string file)
        {
            fileName = Path.GetFileName(file);
            fileDirectory = Path.GetDirectoryName(file);
            createDate = File.GetCreationTime(file);
            modifiedDate = File.GetLastWriteTime(file);
            accessDate = File.GetLastAccessTime(file);
            fileSize = new FileInfo(file).Length;
        }
        private void ScanProcess(string selectedFolder, string DateType)
        {
            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);

                listBox1.Items.Clear();

                int totalFiles = files.Length;
                int processedFiles = 0;

                progressBar1.Maximum = totalFiles;
                progressBar1.Value = 0;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                foreach (string file in files)
                {
                    Fileİnformations(file);
                    GetDateType(DateType, file);
                    if (fileDate > selectedDate)
                    {
                        string listItem = GetListBoxItem(fileDirectory, fileName, createDate, modifiedDate, accessDate, fileSize);
                        listBox1.Items.Add(listItem);
                    }
                    else
                    {
                        string listItem = GetListBoxItem(fileDirectory, fileName, createDate, modifiedDate, accessDate, fileSize);
                        listBox2.Items.Add(listItem);
                    }

                    processedFiles++;
                    progressBar1.Value = processedFiles;

                    lblPath.Text = "PATH : " + fileDirectory + "\\" + fileName;
                    lblScannedItem.Text = $"{processedFiles} / {totalFiles} items were scanned.";

                    Application.DoEvents();

                    lblTotalTime.Text = $"Scan was completed. Total elapsed time {stopwatch.Elapsed.TotalSeconds} seconds";
                    IsItDoneScan();
                }
                stopwatch.Stop();
            }
            else
            {
                MessageBox.Show("Please select a valid folder.", "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        //Move Process
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

        private void Form1_Load(object sender, EventArgs e)
        {
            dtDateOption.Format = DateTimePickerFormat.Custom;
            dtDateOption.CustomFormat = "dd MMMM yyyy h:mm";
            DisabledChecked();
            rdMove.Enabled = false;
            rdCopy.Enabled = false;
        }
        string selectedFileName;
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
                string selectedFolder = txtSourcePath.Text;

                if (rdCreatedDate.Checked == true)
                {
                    ScanProcess(selectedFolder, "Created");
                }
                if (rdModifiedDate.Checked == true)
                {
                    ScanProcess(selectedFolder, "Accessed");
                }
                if (rdAccessedDate.Checked == true)
                {
                    ScanProcess(selectedFolder, "Modified");
                }
            }

            #endregion

            #region MoveProcess
            if (rdMove.Checked)
            {
                if (chOverWrite.Checked)
                {
                    try
                    {
                        string sourceFolderPath = txtSourcePath.Text;
                        string destinationFolderPath = txtTargetPath.Text + "\\" + selectedFileName;
                        if (Directory.Exists(destinationFolderPath))
                            DeleteDirectory(destinationFolderPath); // overwrite process.

                        MoveDirectoryByDate(sourceFolderPath, destinationFolderPath, GetSelectedDateType());
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
                        MoveDirectoryByDate(sourceFolderPath, destinationFolderPath, GetSelectedDateType());
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

        #region Move Directory
        public void MoveDirectoryByDate(string sourceFolder, string targetDirectory, string dateType)
        {
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            string[] files = Directory.GetFiles(sourceFolder);
            //DateTime selectedDate = dtDateOption.Value;

            foreach (string file in files)
            {
                GetDateType(dateType, file);

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
                    MoveDirectoryByDate(subDirectory, targetSubDirectory, dateType);
                }
                else
                {
                    if (chEmptyFolders.Checked)
                    {
                        MoveDirectoryByDate(subDirectory, targetSubDirectory, dateType);
                    }
                    else
                        continue;
                }
            }

            Directory.Delete(sourceFolder, recursive: true);
        }

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
                Form1 form1 = new Form1();
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
