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

        // Scan Process
        #region Scan Process Save Txt and Excel
        private void SaveTxt()
        {
            string selectedFolder = textBox1.Text;

            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);

                string textFilePath = Path.Combine(Application.StartupPath, "output", "output.txt");

                using (StreamWriter sw = new StreamWriter(textFilePath))
                {
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        string fileDirectory = Path.GetDirectoryName(file);

                        DateTime createDate = File.GetCreationTime(file);
                        DateTime modifiedDate = File.GetLastWriteTime(file);
                        DateTime accessDate = File.GetLastAccessTime(file);
                        long fileSize = new FileInfo(file).Length;
                        string item = $"{fileDirectory + "\\" + fileName}";
                        item += $"  Create Date: {createDate}";
                        item += $"  Modified Date: {modifiedDate}";
                        item += $"  Access Date: {accessDate}";
                        item += $"\n  File Size (bytes): {fileSize}";

                        sw.WriteLine(item);
                    }
                }

                MessageBox.Show("Veriler metin dosyasına kaydedildi.", "Başarılı İşlem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir klasör seçin.", "Klasör Seçin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SaveExcel()
        {
            string selectedFolder = textBox1.Text;

            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);

                string excelPathAfterDate = Path.Combine(Application.StartupPath, "output", "afterdate.xlsx");
                string excelPathBeforeDate = Path.Combine(Application.StartupPath, "output", "beforedate.xlsx");

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Data");

                    worksheet.Cell(1, 1).Value = "File Name";
                    worksheet.Cell(1, 2).Value = "Create Date";
                    worksheet.Cell(1, 3).Value = "Modified Date";
                    worksheet.Cell(1, 4).Value = "Access Date";
                    worksheet.Cell(1, 5).Value = "File Size (bytes)";

                    int row = 2;
                    foreach (string file in files)
                    {
                        string fileName = Path.GetFileName(file);
                        string fileDirectory = Path.GetDirectoryName(file);

                        DateTime createDate = File.GetCreationTime(file);
                        DateTime modifiedDate = File.GetLastWriteTime(file);
                        DateTime accessDate = File.GetLastAccessTime(file);
                        long fileSize = new FileInfo(file).Length;

                        worksheet.Cell(row, 1).Value = $"{fileDirectory + "\\" + fileName}";
                        worksheet.Cell(row, 2).Value = createDate.ToString();
                        worksheet.Cell(row, 3).Value = modifiedDate.ToString();
                        worksheet.Cell(row, 4).Value = accessDate.ToString();
                        worksheet.Cell(row, 5).Value = fileSize.ToString();

                        row++;
                    }
                    var range = worksheet.Range("A1:D" + (row - 1));
                    range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    worksheet.ColumnWidth = 15;
                    workbook.SaveAs(excelPathAfterDate);
                    workbook.SaveAs(excelPathBeforeDate);
                }

                MessageBox.Show("Veriler Excel'e kaydedildi.", "Başarılı İşlem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir klasör seçin.", "Klasör Seçin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region Disabled Checked Radio Buttons,CheckBoxs
        public void DisabledChecked()
        {
            textBox3.Enabled = false;
            button2.Enabled = false;
            radioButton1.Checked = true;
            radioButton6.Checked = true;
            radioButton9.Checked = true;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
        }
        #endregion
        #region Scan Process and view to listbox
        private void ScanProcess(string selectedFolder)
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
                    string fileName = Path.GetFileName(file);
                    string fileDirectory = Path.GetDirectoryName(file);

                    DateTime createDate = File.GetCreationTime(file);
                    DateTime modifiedDate = File.GetLastWriteTime(file);
                    DateTime accessDate = File.GetLastAccessTime(file);
                    long fileSize = new FileInfo(file).Length;

                    string listItem = $"{fileDirectory + "\\" + fileName}";
                    listItem += $"\n  Create Date: {createDate}";
                    listItem += $"\n  Modified Date: {modifiedDate}";
                    listItem += $"\n  Access Date: {accessDate}";
                    listItem += $"\n  File Size (bytes): {fileSize}";

                    listBox1.Items.Add(listItem);

                    processedFiles++;
                    progressBar1.Value = processedFiles;

                    label9.Text = "PATH : " + fileDirectory + "\\" + fileName;
                    label8.Text = $"{processedFiles} / {totalFiles} items were scanned.";

                    Application.DoEvents();

                    label10.Text = $"Scan was completed. Total elapsed time {stopwatch.Elapsed.TotalSeconds} seconds";
                }

                stopwatch.Stop();
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir klasör seçin.", "Klasör Seçin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        //Move Process
        #region Enabled Checked Radio Buttons,CheckBoxs
        public void EnabledChecked()
        {
            textBox3.Enabled = true;
            button2.Enabled = true;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
        }
        #endregion
        #region Move Process 
        private void MoveProcess()
        {
            string path = textBox1.Text;
            string pathToMove = textBox2.Text;
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd MMMM yyyy h:mm";
            DisabledChecked();
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
                textBox1.Text = selectedFolder;
            }
            #endregion
        }

        private void button3_Click(object sender, EventArgs e)
        {
            #region Scan Process 
            if (radioButton6.Checked)
            {

                string selectedFolder = textBox1.Text;

                if (radioButton1.Checked == true)
                {
                    if (File.GetCreationTime(selectedFolder) > dateTimePicker1.Value)
                        ScanProcess(selectedFolder);
                    else
                    {

                        DialogResult d1 = MessageBox.Show("Bu tarihten sonra bir kayıt bulunamadı! Yine de tarama işlemi yapılsın mı?", "Kayıt Bulunamadı.", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (d1 == DialogResult.Yes)
                        {
                            ScanProcess(selectedFolder);
                        }
                    }
                }
                if (radioButton2.Checked == true)
                {
                    if (File.GetLastWriteTime(selectedFolder) > dateTimePicker1.Value)
                        ScanProcess(selectedFolder);
                    else
                        MessageBox.Show("Bu tarihten sonra bir kayıt bulunamadı!", "Kayıt Bulunamadı.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (radioButton3.Checked == true)
                {
                    if (File.GetLastAccessTime(selectedFolder) > dateTimePicker1.Value)
                        ScanProcess(selectedFolder);
                    else
                        MessageBox.Show("Bu tarihten sonra bir kayıt bulunamadı!", "Kayıt Bulunamadı.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            #endregion

            #region MoveProcess
            if (radioButton5.Checked)
            {
                if (checkBox3.Checked)
                {
                    try
                    {
                        string sourceFolderPath = textBox1.Text;
                        string destinationFolderPath = textBox3.Text + "\\" + selectedFileName;

                        DeleteDirectory(destinationFolderPath); // overwrite işlemi .
                        Directory.Move(sourceFolderPath, destinationFolderPath);

                        MessageBox.Show("Klasör '" + sourceFolderPath + "' konumundan '" + destinationFolderPath + "' konumuna taşınmıştır.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Klasör taşıma işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        string sourceFolderPath = textBox1.Text;
                        string destinationFolderPath = textBox3.Text + "\\" + selectedFileName;

                        DeleteDirectory(destinationFolderPath); // overwrite işlemi .
                        Directory.Move(sourceFolderPath, destinationFolderPath);

                        MessageBox.Show("Klasör '" + sourceFolderPath + "' konumundan '" + destinationFolderPath + "' konumuna taşınmıştır.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Klasör taşıma işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            #endregion

            #region Copy Process
            if (radioButton4.Checked)
            {
                if (checkBox3.Checked)
                {
                    try
                    {
                        string sourceFolderPath = textBox1.Text;
                        string destinationFolderPath = textBox3.Text + "\\" + selectedFileName;

                        DeleteDirectory(destinationFolderPath); // overwrite işlemi .
                        CopyDirectory(sourceFolderPath, destinationFolderPath);

                        MessageBox.Show("Klasör '" + sourceFolderPath + "' konumu '" + destinationFolderPath + "' konumuna kopyalanmıştır ve " +
                            "üzerine yazılmıştır.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Klasör kopyalama işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        string sourceFolderPath = textBox1.Text;
                        string destinationFolderPath = textBox3.Text + "\\" + selectedFileName;

                        CopyDirectory(sourceFolderPath, destinationFolderPath);

                        MessageBox.Show("Klasör '" + sourceFolderPath + "' konumu '" + destinationFolderPath + "' konumuna kopyalanmıştır.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Klasör kopyalama işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            #endregion
        }

        #region Copy Directory 
        private void CopyDirectory(string sourceDir, string destinationDir)
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
            }

            string[] subDirectories = Directory.GetDirectories(sourceDir);
            foreach (string subDir in subDirectories)
            {
                string dirName = Path.GetFileName(subDir);
                string destSubDir = Path.Combine(destinationDir, dirName);
                CopyDirectory(subDir, destSubDir);
            }
        }

        #endregion
        private void button4_Click(object sender, EventArgs e)
        {
            #region report process 
            if (radioButton9.Checked)
            {
                SaveTxt();
            }
            else if (radioButton8.Checked)
            {
                SaveExcel();
            }
            else
            {
                MessageBox.Show("Lütfen bir seçenek seçin.", "Seçenek Seçin", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                textBox3.Text = selectedFolderMove;
            }
            #endregion


        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
                EnabledChecked();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
                DisabledChecked();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                EnabledChecked();
        }

        // overrite işlemi için ilk olarak siliyorum. sonrasında tekrardan taşıma işlemi gerçekleştiricem.
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
    }
}
