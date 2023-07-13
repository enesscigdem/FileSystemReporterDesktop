﻿using ClosedXML.Excel;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using FileOrbis___File_System_Reporter.Date_Process;
using FileOrbis___File_System_Reporter.File_İnformation;
using FileOrbis___File_System_Reporter.FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
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
        public string fileName, fileDirectory, selectedFileName, checkedDate = "Created";
        public long fileSize;
        public List<Fileİnformation> informationList;
        public List<Folderİnformation> folderList;
        ScanProcess scanProcess = new ScanProcess();
        DateType dt = new DateType();
        IDateOptions dateOptions;
        Validation validator = new Validation();
        int totalFiles;
        FormValidate model = new FormValidate();

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
        private void Form1_Load(object sender, EventArgs e)
        {
            dtDateOption.Format = DateTimePickerFormat.Custom;
            dtDateOption.CustomFormat = "dd MMMM yyyy h:mm";
            dtDateOption.Value = DateTime.Now.AddDays(-7);
            DisabledChecked();
            rdMove.Enabled = false;
            rdCopy.Enabled = false;
            scanProcess.lblScannedMessage = new lblScannedMessage(UpdateLblScan);
            scanProcess.lblTotalTımeCallBack = new lblTotalTımeCallBack(UpdateLblTotalTıme);
            scanProcess.lblPathMessage = new lblPathMessage(UpdateLblPath);
            scanProcess.ProgressBarCallBack = UpdateProgressBar;
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
                rdMove.Enabled = false;
                rdCopy.Enabled = false;
                rdScan.Checked = true;
            }
            #endregion
        }

        #region Get Selected Data Type
        private void rdCreatedDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCreatedDate.Checked)
                checkedDate = "Created";
        }

        private void rdModifiedDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdModifiedDate.Checked)
                checkedDate = "Modified";
        }

        private void rdAccessedDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAccessedDate.Checked)
                checkedDate = "Accessed";
        }
        #endregion
        public void UpdateProgressBar(int processedFiles, int totalFiles)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<int, int>(UpdateProgressBar), processedFiles, totalFiles);
                return;
            }
            progressBar1.Maximum = totalFiles;
            progressBar1.Value = processedFiles;
            progressBar1.Update();
        }

        public void UpdateLblPath(string fileInfo)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(UpdateLblPath), fileInfo);
                return;
            }
            lblPath.Text = fileInfo;
            lblPath.Update();
        }
        public void UpdateLblScan(int processedFiles, int totalFiles)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<int, int>(UpdateLblScan), processedFiles, totalFiles);
                return;
            }
            lblScannedItem.Text = $"{processedFiles} / {totalFiles} items were scanned.";
            lblScannedItem.Update();
        }

        public void UpdateLblTotalTıme(Stopwatch stopwatch)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<Stopwatch>(UpdateLblTotalTıme), stopwatch);
                return;
            }
            lblTotalTime.Text = $"Scan was completed. Total elapsed time: {stopwatch.Elapsed.TotalSeconds} seconds";
            lblTotalTime.Update();
        }
       
        private async void txtSourcePath_TextChanged(object sender, EventArgs e)
        {
            FluentValidation();
            if (validationResult.IsValid)
            {
                btnRun.Enabled = false;
                await Task.Run(() =>
                {
                    totalFiles = Directory.GetFiles(txtSourcePath.Text, "*", SearchOption.AllDirectories).Length;
                });
                btnRun.Enabled = true;
            }
            else
            {
                string errorMessage = string.Join(Environment.NewLine, validationResult.Errors);
                MessageBox.Show(errorMessage, "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        ValidationResult validationResult;
        private void FluentValidation()
        {
            model.Thread = txtThread.Text;
            model.Path = txtSourcePath.Text;
            validationResult = validator.Validate(model);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            FluentValidation();
            if (validationResult.IsValid)
            {
                #region Scan Process 
                dateOptions = dt.GetDateType(checkedDate);
                selectedDate = dtDateOption.Value;
                if (rdScan.Checked)
                {
                    progressBar1.Value = 0;
                    string selectedFolder = txtSourcePath.Text;

                    var result = scanProcess.ScanOperation(selectedFolder, Convert.ToInt32(txtThread.Text), totalFiles);

                    if (result.files != null && result.folders != null)
                    {
                        informationList = result.files;
                        folderList = result.folders;
                        IsItDoneScan();
                    }

                }
                #endregion

                #region MoveProcess
                if (rdMove.Checked)
                {
                    MoveProcess moveProcess = new MoveProcess();
                    moveProcess.MoveOperation(checkedDate, rdMove.Checked, chOverWrite.Checked, txtSourcePath.Text, txtTargetPath.Text, selectedFileName, chEmptyFolders.Checked, fileDate, selectedDate, informationList, folderList, dateOptions);
                }
                #endregion

                #region Copy Process
                if (rdCopy.Checked)
                {
                    if (Directory.Exists(txtTargetPath.Text))
                    {
                        CopyProcess copyProcess = new CopyProcess();
                        copyProcess.CopyOperation(txtSourcePath.Text, txtTargetPath.Text, selectedFileName, chOverWrite.Checked, chNtfsPermission.Checked, informationList, folderList, fileDate, selectedDate, chEmptyFolders.Checked, dateOptions);

                    }
                    else
                        MessageBox.Show("No such path was found.");
                }
                #endregion
            }
            else
            {
                string errorMessage = string.Join(Environment.NewLine, validationResult.Errors);
                MessageBox.Show(errorMessage, "Doğrulama Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void button4_Click(object sender, EventArgs e)
        {
            #region report process 
            if (rdTxt.Checked)
            {
                TxtProcess txtProcess = new TxtProcess();
                txtProcess.SaveTxt(txtSourcePath.Text, informationList);
            }
            else if (rdExcel.Checked)
            {
                ExcelProcess excelProcess = new ExcelProcess();

                excelProcess.SaveExcel(txtSourcePath.Text, checkedDate, selectedDate, fileDate, informationList);
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
