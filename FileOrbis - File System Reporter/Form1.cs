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
                DeleteProcess deleteProcess = new DeleteProcess(this);
                if (chOverWrite.Checked)
                {
                    try
                    {
                        string sourceFolderPath = txtSourcePath.Text;
                        string destinationFolderPath = txtTargetPath.Text + "\\" + selectedFileName;
                        if (Directory.Exists(destinationFolderPath))
                            deleteProcess.DeleteDirectory(destinationFolderPath);

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
                MoveProcess moveProcess = new MoveProcess(this);
                moveProcess.MoveOperation();
            }
            #endregion
        }
        private void button4_Click(object sender, EventArgs e)
        {
            #region report process 
            if (rdTxt.Checked)
            {
                TxtProcess txtProcess = new TxtProcess();
                txtProcess.SaveTxt(txtSourcePath.Text, fileDirectory, fileName, createDate, modifiedDate, accessDate, fileSize);
            }
            else if (rdExcel.Checked)
            {
                ExcelProcess excelProcess = new ExcelProcess();
                excelProcess.SaveExcel(txtSourcePath.Text, GetSelectedDateType());
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
