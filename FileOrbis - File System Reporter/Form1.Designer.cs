
namespace FileOrbis___File_System_Reporter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.btnSourcePath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtDateOption = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtThread = new System.Windows.Forms.TextBox();
            this.rdCopy = new System.Windows.Forms.RadioButton();
            this.rdMove = new System.Windows.Forms.RadioButton();
            this.rdScan = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.chEmptyFolders = new System.Windows.Forms.CheckBox();
            this.chNtfsPermission = new System.Windows.Forms.CheckBox();
            this.chOverWrite = new System.Windows.Forms.CheckBox();
            this.btnTargetPath = new System.Windows.Forms.Button();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.lblScannedItem = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdAccessedDate = new System.Windows.Forms.RadioButton();
            this.rdModifiedDate = new System.Windows.Forms.RadioButton();
            this.rdCreatedDate = new System.Windows.Forms.RadioButton();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rdExcel = new System.Windows.Forms.RadioButton();
            this.rdTxt = new System.Windows.Forms.RadioButton();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path";
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSourcePath.Location = new System.Drawing.Point(12, 43);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.Size = new System.Drawing.Size(751, 28);
            this.txtSourcePath.TabIndex = 1;
            // 
            // btnSourcePath
            // 
            this.btnSourcePath.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSourcePath.Location = new System.Drawing.Point(782, 43);
            this.btnSourcePath.Name = "btnSourcePath";
            this.btnSourcePath.Size = new System.Drawing.Size(96, 27);
            this.btnSourcePath.TabIndex = 2;
            this.btnSourcePath.Text = "Browse";
            this.btnSourcePath.UseVisualStyleBackColor = true;
            this.btnSourcePath.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(12, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Date Option";
            // 
            // dtDateOption
            // 
            this.dtDateOption.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dtDateOption.Location = new System.Drawing.Point(424, 130);
            this.dtDateOption.Name = "dtDateOption";
            this.dtDateOption.Size = new System.Drawing.Size(266, 28);
            this.dtDateOption.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(420, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Date Option";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(710, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "Thread Count";
            // 
            // txtThread
            // 
            this.txtThread.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtThread.Location = new System.Drawing.Point(714, 130);
            this.txtThread.Name = "txtThread";
            this.txtThread.Size = new System.Drawing.Size(164, 28);
            this.txtThread.TabIndex = 11;
            // 
            // rdCopy
            // 
            this.rdCopy.AutoSize = true;
            this.rdCopy.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rdCopy.Location = new System.Drawing.Point(275, 223);
            this.rdCopy.Name = "rdCopy";
            this.rdCopy.Size = new System.Drawing.Size(63, 25);
            this.rdCopy.TabIndex = 15;
            this.rdCopy.TabStop = true;
            this.rdCopy.Text = "Copy";
            this.rdCopy.UseVisualStyleBackColor = true;
            this.rdCopy.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // rdMove
            // 
            this.rdMove.AutoSize = true;
            this.rdMove.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rdMove.Location = new System.Drawing.Point(142, 223);
            this.rdMove.Name = "rdMove";
            this.rdMove.Size = new System.Drawing.Size(68, 25);
            this.rdMove.TabIndex = 14;
            this.rdMove.TabStop = true;
            this.rdMove.Text = "Move";
            this.rdMove.UseVisualStyleBackColor = true;
            this.rdMove.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // rdScan
            // 
            this.rdScan.AutoSize = true;
            this.rdScan.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rdScan.Location = new System.Drawing.Point(16, 223);
            this.rdScan.Name = "rdScan";
            this.rdScan.Size = new System.Drawing.Size(60, 25);
            this.rdScan.TabIndex = 13;
            this.rdScan.TabStop = true;
            this.rdScan.Text = "Scan";
            this.rdScan.UseVisualStyleBackColor = true;
            this.rdScan.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(12, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 21);
            this.label5.TabIndex = 12;
            this.label5.Text = "Options";
            // 
            // chEmptyFolders
            // 
            this.chEmptyFolders.AutoSize = true;
            this.chEmptyFolders.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chEmptyFolders.Location = new System.Drawing.Point(424, 223);
            this.chEmptyFolders.Name = "chEmptyFolders";
            this.chEmptyFolders.Size = new System.Drawing.Size(129, 25);
            this.chEmptyFolders.TabIndex = 16;
            this.chEmptyFolders.Text = "Empty Folders";
            this.chEmptyFolders.UseVisualStyleBackColor = true;
            // 
            // chNtfsPermission
            // 
            this.chNtfsPermission.AutoSize = true;
            this.chNtfsPermission.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chNtfsPermission.Location = new System.Drawing.Point(557, 223);
            this.chNtfsPermission.Name = "chNtfsPermission";
            this.chNtfsPermission.Size = new System.Drawing.Size(152, 25);
            this.chNtfsPermission.TabIndex = 17;
            this.chNtfsPermission.Text = "NTFS Permissions";
            this.chNtfsPermission.UseVisualStyleBackColor = true;
            // 
            // chOverWrite
            // 
            this.chOverWrite.AutoSize = true;
            this.chOverWrite.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chOverWrite.Location = new System.Drawing.Point(713, 223);
            this.chOverWrite.Name = "chOverWrite";
            this.chOverWrite.Size = new System.Drawing.Size(98, 25);
            this.chOverWrite.TabIndex = 18;
            this.chOverWrite.Text = "Overwrite";
            this.chOverWrite.UseVisualStyleBackColor = true;
            // 
            // btnTargetPath
            // 
            this.btnTargetPath.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTargetPath.Location = new System.Drawing.Point(782, 308);
            this.btnTargetPath.Name = "btnTargetPath";
            this.btnTargetPath.Size = new System.Drawing.Size(96, 27);
            this.btnTargetPath.TabIndex = 21;
            this.btnTargetPath.Text = "Browse";
            this.btnTargetPath.UseVisualStyleBackColor = true;
            this.btnTargetPath.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtTargetPath
            // 
            this.txtTargetPath.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTargetPath.Location = new System.Drawing.Point(12, 308);
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.Size = new System.Drawing.Size(751, 28);
            this.txtTargetPath.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(12, 286);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 21);
            this.label6.TabIndex = 19;
            this.label6.Text = "Target Path";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(12, 363);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 21);
            this.label7.TabIndex = 22;
            this.label7.Text = "Folder Exceptions";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Calibri", 12F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Custom Regex"});
            this.comboBox1.Location = new System.Drawing.Point(12, 400);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(197, 27);
            this.comboBox1.TabIndex = 23;
            this.comboBox1.Text = "Custom Regex";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox4.Location = new System.Drawing.Point(215, 400);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(663, 28);
            this.textBox4.TabIndex = 24;
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRun.Location = new System.Drawing.Point(12, 482);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(430, 33);
            this.btnRun.TabIndex = 25;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnReport
            // 
            this.btnReport.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnReport.Location = new System.Drawing.Point(448, 482);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(430, 33);
            this.btnReport.TabIndex = 26;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.button4_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 528);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(866, 62);
            this.progressBar1.TabIndex = 27;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.lblTotalTime);
            this.panel1.Controls.Add(this.lblPath);
            this.panel1.Controls.Add(this.lblScannedItem);
            this.panel1.Location = new System.Drawing.Point(12, 596);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 140);
            this.panel1.TabIndex = 28;
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTotalTime.Location = new System.Drawing.Point(13, 98);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(317, 21);
            this.lblTotalTime.TabIndex = 31;
            this.lblTotalTime.Text = "Scan was completed. Total elapsed time : .....";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblPath.Location = new System.Drawing.Point(13, 51);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(70, 21);
            this.lblPath.TabIndex = 30;
            this.lblPath.Text = "PATH  ... ";
            // 
            // lblScannedItem
            // 
            this.lblScannedItem.AutoSize = true;
            this.lblScannedItem.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblScannedItem.Location = new System.Drawing.Point(13, 12);
            this.lblScannedItem.Name = "lblScannedItem";
            this.lblScannedItem.Size = new System.Drawing.Size(168, 21);
            this.lblScannedItem.TabIndex = 29;
            this.lblScannedItem.Text = "... items were scanned.";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdAccessedDate);
            this.panel2.Controls.Add(this.rdModifiedDate);
            this.panel2.Controls.Add(this.rdCreatedDate);
            this.panel2.Location = new System.Drawing.Point(12, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(397, 42);
            this.panel2.TabIndex = 29;
            // 
            // rdAccessedDate
            // 
            this.rdAccessedDate.AutoSize = true;
            this.rdAccessedDate.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rdAccessedDate.Location = new System.Drawing.Point(263, 13);
            this.rdAccessedDate.Name = "rdAccessedDate";
            this.rdAccessedDate.Size = new System.Drawing.Size(127, 25);
            this.rdAccessedDate.TabIndex = 32;
            this.rdAccessedDate.TabStop = true;
            this.rdAccessedDate.Text = "Accessed Date";
            this.rdAccessedDate.UseVisualStyleBackColor = true;
            this.rdAccessedDate.CheckedChanged += new System.EventHandler(this.rdAccessedDate_CheckedChanged);
            // 
            // rdModifiedDate
            // 
            this.rdModifiedDate.AutoSize = true;
            this.rdModifiedDate.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rdModifiedDate.Location = new System.Drawing.Point(130, 13);
            this.rdModifiedDate.Name = "rdModifiedDate";
            this.rdModifiedDate.Size = new System.Drawing.Size(127, 25);
            this.rdModifiedDate.TabIndex = 31;
            this.rdModifiedDate.TabStop = true;
            this.rdModifiedDate.Text = "Modified Date";
            this.rdModifiedDate.UseVisualStyleBackColor = true;
            this.rdModifiedDate.CheckedChanged += new System.EventHandler(this.rdModifiedDate_CheckedChanged);
            // 
            // rdCreatedDate
            // 
            this.rdCreatedDate.AutoSize = true;
            this.rdCreatedDate.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rdCreatedDate.Location = new System.Drawing.Point(4, 13);
            this.rdCreatedDate.Name = "rdCreatedDate";
            this.rdCreatedDate.Size = new System.Drawing.Size(118, 25);
            this.rdCreatedDate.TabIndex = 30;
            this.rdCreatedDate.TabStop = true;
            this.rdCreatedDate.Text = "Created Date";
            this.rdCreatedDate.UseVisualStyleBackColor = true;
            this.rdCreatedDate.CheckedChanged += new System.EventHandler(this.rdCreatedDate_CheckedChanged);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(12, 771);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(430, 130);
            this.listBox1.TabIndex = 31;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.Location = new System.Drawing.Point(528, 448);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 21);
            this.label11.TabIndex = 35;
            this.label11.Text = "Options :";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rdExcel);
            this.panel3.Controls.Add(this.rdTxt);
            this.panel3.Location = new System.Drawing.Point(616, 436);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(134, 42);
            this.panel3.TabIndex = 34;
            // 
            // rdExcel
            // 
            this.rdExcel.AutoSize = true;
            this.rdExcel.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rdExcel.Location = new System.Drawing.Point(64, 10);
            this.rdExcel.Name = "rdExcel";
            this.rdExcel.Size = new System.Drawing.Size(62, 25);
            this.rdExcel.TabIndex = 31;
            this.rdExcel.TabStop = true;
            this.rdExcel.Text = "Excel";
            this.rdExcel.UseVisualStyleBackColor = true;
            // 
            // rdTxt
            // 
            this.rdTxt.AutoSize = true;
            this.rdTxt.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rdTxt.Location = new System.Drawing.Point(6, 10);
            this.rdTxt.Name = "rdTxt";
            this.rdTxt.Size = new System.Drawing.Size(52, 25);
            this.rdTxt.TabIndex = 30;
            this.rdTxt.TabStop = true;
            this.rdTxt.Text = "Txt ";
            this.rdTxt.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 18;
            this.listBox2.Location = new System.Drawing.Point(448, 771);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(430, 130);
            this.listBox2.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label12.Location = new System.Drawing.Point(183, 747);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 21);
            this.label12.TabIndex = 37;
            this.label12.Text = "After";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label13.Location = new System.Drawing.Point(612, 747);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(55, 21);
            this.label13.TabIndex = 38;
            this.label13.Text = "Before";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 917);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnTargetPath);
            this.Controls.Add(this.txtTargetPath);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.chOverWrite);
            this.Controls.Add(this.chNtfsPermission);
            this.Controls.Add(this.chEmptyFolders);
            this.Controls.Add(this.rdCopy);
            this.Controls.Add(this.rdMove);
            this.Controls.Add(this.rdScan);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtThread);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtDateOption);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSourcePath);
            this.Controls.Add(this.txtSourcePath);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FileOrbis - File System Reporter - Version: 1.0.6";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSourcePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtThread;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTargetPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdAccessedDate;
        private System.Windows.Forms.RadioButton rdModifiedDate;
        private System.Windows.Forms.RadioButton rdCreatedDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rdExcel;
        private System.Windows.Forms.RadioButton rdTxt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Label lblTotalTime;
        public System.Windows.Forms.Label lblPath;
        public System.Windows.Forms.Label lblScannedItem;
        public System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.ListBox listBox2;
        public System.Windows.Forms.CheckBox chEmptyFolders;
        public System.Windows.Forms.CheckBox chNtfsPermission;
        public System.Windows.Forms.CheckBox chOverWrite;
        public System.Windows.Forms.RadioButton rdCopy;
        public System.Windows.Forms.RadioButton rdMove;
        public System.Windows.Forms.RadioButton rdScan;
        public System.Windows.Forms.TextBox txtSourcePath;
        public System.Windows.Forms.TextBox txtTargetPath;
        public System.Windows.Forms.DateTimePicker dtDateOption;
    }
}

