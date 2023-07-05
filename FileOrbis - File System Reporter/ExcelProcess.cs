using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileOrbis___File_System_Reporter
{
    public class ExcelProcess
    {
        Form1 frm = new Form1();
        public void ExcelAddHeaders(IXLWorksheet worksheet)
        {
            worksheet.Cell(1, 1).Value = "File Name";
            worksheet.Cell(1, 2).Value = "Create Date";
            worksheet.Cell(1, 3).Value = "Modified Date";
            worksheet.Cell(1, 4).Value = "Access Date";
            worksheet.Cell(1, 5).Value = "File Size (bytes)";
        }

        public void ExcelAddFileData(IXLWorksheet worksheet, int row, string file, DateTime createDate, DateTime modifiedDate, DateTime accessDate, long fileSize)
        {
            worksheet.Cell(row, 1).Value = file;
            worksheet.Cell(row, 2).Value = createDate.ToString();
            worksheet.Cell(row, 3).Value = modifiedDate.ToString();
            worksheet.Cell(row, 4).Value = accessDate.ToString();
            worksheet.Cell(row, 5).Value = fileSize.ToString();
        }
        public void ExcelOperations(string selectedFolder, string excelfileName, string dateType)
        {
            string[] files = Directory.GetFiles(selectedFolder, "*", SearchOption.AllDirectories);
            string selectedExcelFileName = string.Format(excelfileName + "{0:dd-MM-yyyy_HH.mm.ss}.xlsx", DateTime.Now);
            string excelFilePath = Path.Combine(Path.Combine(Application.StartupPath, "output", selectedExcelFileName));

            using (var workbook = new XLWorkbook())
            {
                string worksheetName = Convert.ToString(workbook.Worksheets.Add(excelfileName));
                ExcelAddHeaders(workbook.Worksheet(worksheetName));

                int row = 2;

                foreach (string file in files)
                {
                    frm.Fileİnformations(file);
                    frm.GetDateType(dateType, file);

                    if (frm.fileDate > frm.selectedDate && excelfileName == "afterDate")
                    {
                        ExcelAddFileData(workbook.Worksheet(worksheetName), row, $"{frm.fileDirectory}\\{frm.fileName}", frm.createDate, frm.modifiedDate, frm.accessDate, frm.fileSize);
                        row++;
                    }
                    else if (frm.fileDate < frm.selectedDate && excelfileName == "beforeDate")
                    {
                        ExcelAddFileData(workbook.Worksheet(worksheetName), row, $"{frm.fileDirectory}\\{frm.fileName}", frm.createDate, frm.modifiedDate, frm.accessDate, frm.fileSize);
                        row++;
                    }
                }
                var range = workbook.Worksheet(worksheetName).Range("A1:E" + (row - 1));
                range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                workbook.Worksheet(worksheetName).ColumnWidth = 15;

                try
                {
                    workbook.SaveAs(excelFilePath);
                    MessageBox.Show("The data has been saved to Excel.", "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Output failed. If your Excel file is open, close it and try.", "İnfo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void SaveExcel(string sourcePath, string dateType)
        {
            string selectedFolder = sourcePath;

            if (string.IsNullOrEmpty(selectedFolder) || !Directory.Exists(selectedFolder))
            {
                MessageBox.Show("Please select a valid folder.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ExcelOperations(selectedFolder, "afterDate", dateType);
            ExcelOperations(selectedFolder, "beforeDate", dateType);
        }
    }
}
