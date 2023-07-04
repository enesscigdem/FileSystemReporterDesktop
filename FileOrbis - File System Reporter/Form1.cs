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

        private void AddHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell(1, 1).Value = "File Name";
            worksheet.Cell(1, 2).Value = "Create Date";
            worksheet.Cell(1, 3).Value = "Modified Date";
            worksheet.Cell(1, 4).Value = "Access Date";
            worksheet.Cell(1, 5).Value = "File Size (bytes)";
        }

        private void AddFileData(IXLWorksheet worksheet, int row, string file, DateTime createDate, DateTime modifiedDate, DateTime accessDate, long fileSize)
        {
            worksheet.Cell(row, 1).Value = file;
            worksheet.Cell(row, 2).Value = createDate.ToString();
            worksheet.Cell(row, 3).Value = modifiedDate.ToString();
            worksheet.Cell(row, 4).Value = accessDate.ToString();
            worksheet.Cell(row, 5).Value = fileSize.ToString();
        }

        private void SaveExcel()
        {
            string selectedFolder = textBox1.Text;

            if (string.IsNullOrEmpty(selectedFolder) || !Directory.Exists(selectedFolder))
            {
                MessageBox.Show("Lütfen geçerli bir klasör seçin.", "Klasör Seçin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);
            string excelPathAfterDate = Path.Combine(Application.StartupPath, "output", "afterdate.xlsx");
            string excelPathBeforeDate = Path.Combine(Application.StartupPath, "output", "beforedate.xlsx");

            using (var workbookAfterDate = new XLWorkbook())
            using (var workbookBeforeDate = new XLWorkbook())
            {
                var worksheetAfterDate = workbookAfterDate.Worksheets.Add("Data");
                var worksheetBeforeDate = workbookBeforeDate.Worksheets.Add("Data");

                AddHeaders(worksheetAfterDate);
                AddHeaders(worksheetBeforeDate);

                int rowAfterDate = 2;
                int rowBeforeDate = 2;

                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    string fileDirectory = Path.GetDirectoryName(file);
                    DateTime createDate = File.GetCreationTime(file);
                    DateTime modifiedDate = File.GetLastWriteTime(file);
                    DateTime accessDate = File.GetLastAccessTime(file);
                    long fileSize = new FileInfo(file).Length;

                    if ((radioButton1.Checked && createDate > dateTimePicker1.Value) ||
                        (radioButton2.Checked && accessDate > dateTimePicker1.Value) ||
                        (radioButton3.Checked && modifiedDate > dateTimePicker1.Value))
                    {
                        AddFileData(worksheetAfterDate, rowAfterDate, $"{fileDirectory}\\{fileName}", createDate, modifiedDate, accessDate, fileSize);
                        rowAfterDate++;
                    }
                    else
                    {
                        AddFileData(worksheetBeforeDate, rowBeforeDate, $"{fileDirectory}\\{fileName}", createDate, modifiedDate, accessDate, fileSize);
                        rowBeforeDate++;
                    }
                }

                var rangeAfterDate = worksheetAfterDate.Range("A1:E" + (rowAfterDate - 1));
                var rangeBeforeDate = worksheetBeforeDate.Range("A1:E" + (rowBeforeDate - 1));
                rangeAfterDate.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                rangeBeforeDate.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                worksheetAfterDate.ColumnWidth = 15;
                worksheetBeforeDate.ColumnWidth = 15;

                try
                {
                    workbookAfterDate.SaveAs(excelPathAfterDate);
                    workbookBeforeDate.SaveAs(excelPathBeforeDate);
                    MessageBox.Show("Veriler Excel'e kaydedildi.", "Başarılı İşlem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Çıktı alma başarısız. Excel dosyanız açık ise kapatıp deneyiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
        public string GetListBoxItem(string fileDirectory, string fileName, DateTime createDate, DateTime modifiedDate, DateTime accessDate, long fileSize)
        {
            string listItem = $"{fileDirectory + "\\" + fileName}";
            listItem += $"\n  Create Date: {createDate}";
            listItem += $"\n  Modified Date: {modifiedDate}";
            listItem += $"\n  Access Date: {accessDate}";
            listItem += $"\n  File Size (bytes): {fileSize}";

            return listItem;
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
                    string fileName = Path.GetFileName(file);
                    string fileDirectory = Path.GetDirectoryName(file);
                    DateTime createDate = File.GetCreationTime(file);
                    DateTime modifiedDate = File.GetLastWriteTime(file);
                    DateTime accessDate = File.GetLastAccessTime(file);
                    long fileSize = new FileInfo(file).Length;
                    if ((DateType == "Created" && createDate > dateTimePicker1.Value) || (DateType == "Accessed" && accessDate > dateTimePicker1.Value) || (DateType == "Modified" && modifiedDate > dateTimePicker1.Value))
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
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            if (radioButton6.Checked)
            {
                string selectedFolder = textBox1.Text;

                if (radioButton1.Checked == true)
                {
                    ScanProcess(selectedFolder, "Created");
                }
                if (radioButton2.Checked == true)
                {
                    ScanProcess(selectedFolder, "Accessed");
                }
                if (radioButton3.Checked == true)
                {
                    ScanProcess(selectedFolder, "Modified");
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
                        DeleteDirectory(destinationFolderPath); // overwrite işlemi.
                        MoveDirectoryByDate(sourceFolderPath, destinationFolderPath, GetSelectedDateType());

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

                        Directory.CreateDirectory(destinationFolderPath);
                        MoveDirectoryByDate(sourceFolderPath, destinationFolderPath, GetSelectedDateType());

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
                        bool copyPermissions = checkBox2.Checked;
                        string sourceFolderPath = textBox1.Text;
                        string destinationFolderPath = textBox3.Text + "\\" + selectedFileName;

                        DeleteDirectory(destinationFolderPath); // overwrite işlemi .
                        CopyDirectory(sourceFolderPath, destinationFolderPath, copyPermissions);

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
                        bool copyPermissions = checkBox2.Checked;
                        string sourceFolderPath = textBox1.Text;
                        string destinationFolderPath = textBox3.Text + "\\" + selectedFileName;

                        CopyDirectory(sourceFolderPath, destinationFolderPath, copyPermissions);

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

        #region Move Directory
        public void MoveDirectoryByDate(string kaynakKlasör, string hedefKlasör, string dateType)
        {
            if (!Directory.Exists(hedefKlasör))
            {
                Directory.CreateDirectory(hedefKlasör);
            }

            string[] dosyalar = Directory.GetFiles(kaynakKlasör);
            DateTime selectedDate = dateTimePicker1.Value;

            foreach (string dosya in dosyalar)
            {
                DateTime fileDate;

                switch (dateType)
                {
                    case "Created":
                        fileDate = File.GetCreationTime(dosya);
                        break;
                    case "Modified":
                        fileDate = File.GetLastWriteTime(dosya);
                        break;
                    case "Accessed":
                        fileDate = File.GetLastAccessTime(dosya);
                        break;
                    default:
                        throw new ArgumentException("Geçersiz tarih türü.");
                }

                // Dosyanın seçilen tarihten sonraki bir tarihte mi olduğunu kontrol ediyoruz.
                if (fileDate > selectedDate)
                {
                    string dosyaAdi = Path.GetFileName(dosya);
                    string hedefDosya = Path.Combine(hedefKlasör, dosyaAdi);
                    File.Move(dosya, hedefDosya);
                }
            }

            string[] altDizinler = Directory.GetDirectories(kaynakKlasör);
            foreach (string altDizin in altDizinler)
            {
                string altDizinAdi = Path.GetFileName(altDizin);
                string[] altDizinDosyalari = Directory.GetFiles(altDizin);
                if (altDizinDosyalari.Length >= 1)
                {
                    string hedefAltDizin = Path.Combine(hedefKlasör, altDizinAdi);
                    MoveDirectoryByDate(altDizin, hedefAltDizin, dateType);
                }
                else
                {
                    if (checkBox1.Checked)
                    {
                        string hedefAltDizin = Path.Combine(hedefKlasör, altDizinAdi);
                        MoveDirectoryByDate(altDizin, hedefAltDizin, dateType);
                    }
                    else
                        continue;
                }
            }

            Directory.Delete(kaynakKlasör, recursive: true);
        }

        private string GetSelectedDateType()
        {
            if (radioButton1.Checked)
                return "Created";
            else if (radioButton2.Checked)
                return "Modified";
            else if (radioButton3.Checked)
                return "Accessed";
            else
                throw new ArgumentException("Tarih türü seçilmemiş.");
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
                    FileSecurity sourceFileSecurity = File.GetAccessControl(file);
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

        #endregion// overrite işlemi için ilk olarak siliyorum. sonrasında tekrardan taşıma işlemi gerçekleştiricem.
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
    }
}
