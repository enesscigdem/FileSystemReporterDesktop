﻿using ClosedXML.Excel;
using DocumentFormat.OpenXml.Presentation;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileOrbis___File_System_Reporter
{
    public partial class Form1 : Form
    {
        private ScanProcess scanProcess;
        private List<Thread> scanThreads;
        public Form1()
        {
            InitializeComponent();
            scanThreads = new List<Thread>();
            scanProcess = new ScanProcess(this);
        }
        public DateTime selectedDate, createDate, modifiedDate, accessDate, fileDate;
        public string fileName, fileDirectory, selectedFileName, checkedDate = "Created";
        public long fileSize;
        public List<Fileİnformation> informationList;

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


        public void UpdateLblScan(int processedFiles, int totalFiles)
        {
            if (lblScannedItem.InvokeRequired)
            {
                void action()
                {
                    UpdateLblScan(processedFiles, totalFiles);
                }
                lblScannedItem.Invoke((Action)action);
                return;
            }
            lblScannedItem.Text = $"{processedFiles} / {totalFiles} items were scanned.";
            lblScannedItem.Update();
        }
        public void UpdateLblTotalTıme(Stopwatch stopwatch)
        {
            if (lblTotalTime.InvokeRequired)
            {
                void action()
                {
                    UpdateLblTotalTıme(stopwatch);
                }
                lblTotalTime.Invoke((Action)action);
                return;
            }
            lblTotalTime.Text = $"Scan was completed. Total elapsed time: {stopwatch.Elapsed.TotalSeconds} seconds";
            lblTotalTime.Update();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            #region Scan Process 

            selectedDate = dtDateOption.Value;
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            if (rdScan.Checked)
            {
                ScanProcess scanProcess = new ScanProcess(this);// bunun invokunu ordaki fonksiyona eşitliyosun frm yi kullanmana gerek kalmıyor
                scanProcess.lblScannedMessage = new lblScannedMessage(UpdateLblScan);
                scanProcess.lblTotalTımeCallBack = new lblTotalTımeCallBack(UpdateLblTotalTıme);
                string selectedFolder = txtSourcePath.Text;
                var tempList = scanProcess.ScanOperation(selectedFolder, selectedDate, checkedDate, fileDate,Convert.ToInt32(txtThread.Text));
                if (tempList != null)
                {
                    informationList = tempList;
                    IsItDoneScan();
                }
            }
            #endregion

            #region threadlı scan işlemi demo 
            //listBox1.Items.Clear();
            //listBox2.Items.Clear();

            //if (rdScan.Checked)
            //{
            //    int threadCount;
            //    if (!int.TryParse(txtThread.Text, out threadCount) || threadCount <= 0)
            //    {
            //        MessageBox.Show("Please enter a valid thread count.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    string selectedFolder = txtSourcePath.Text;
            //    string dateType = "";
            //    if (rdCreatedDate.Checked)
            //        dateType = "Created";
            //    else if (rdModifiedDate.Checked)
            //        dateType = "Accessed";
            //    else if (rdAccessedDate.Checked)
            //        dateType = "Modified";
            //    else
            //    {
            //        MessageBox.Show("Please select a date type.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    for (int i = 0; i < threadCount; i++)
            //    {
            //        Thread thread = new Thread(() =>
            //        {
            //            ScanProcess scanProcess = new ScanProcess(this);
            //            scanProcess.ScanOperation(selectedFolder, dateType);
            //        });
            //        scanThreads.Add(thread);
            //        thread.Start();
            //    }
            //}
            #endregion

            #region MoveProcess
            if (rdMove.Checked)
            {
                MoveProcess moveProcess = new MoveProcess();
                DeleteProcess deleteProcess = new DeleteProcess();
                moveProcess.MoveOperation(checkedDate, rdMove.Checked, chOverWrite.Checked, txtSourcePath.Text, txtTargetPath.Text, selectedFileName, chEmptyFolders.Checked, fileDate, selectedDate, informationList);
            }
            #endregion

            #region Copy Process
            if (rdCopy.Checked)
            {
                CopyProcess copyProcess = new CopyProcess();
                copyProcess.CopyOperation(txtSourcePath.Text, txtTargetPath.Text, selectedFileName, chOverWrite.Checked, chNtfsPermission.Checked, rdCopy.Checked, informationList);
            }
            #endregion
        }
        private void button4_Click(object sender, EventArgs e)
        {
            #region report process 
            if (rdTxt.Checked)
            {
                TxtProcess txtProcess = new TxtProcess();
                txtProcess.SaveTxt(txtSourcePath.Text, fileDirectory, fileName, createDate, modifiedDate, accessDate, fileSize, informationList);
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
