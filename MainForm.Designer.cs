namespace Camurphy.CameraDownload
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.gbxNamingOptions = new System.Windows.Forms.GroupBox();
            this.nudFileDigits = new System.Windows.Forms.NumericUpDown();
            this.lblDigits = new System.Windows.Forms.Label();
            this.tbxFilePrefix = new System.Windows.Forms.TextBox();
            this.lblPrefix = new System.Windows.Forms.Label();
            this.cbxRestartNumbering = new System.Windows.Forms.CheckBox();
            this.cbxResumeNumbering = new System.Windows.Forms.CheckBox();
            this.cbxRenameFiles = new System.Windows.Forms.CheckBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblDestination = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.fbdBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.lblDeviceType = new System.Windows.Forms.Label();
            this.rbnVideo = new System.Windows.Forms.RadioButton();
            this.rbnStill = new System.Windows.Forms.RadioButton();
            this.gbxImageOptions = new System.Windows.Forms.GroupBox();
            this.ddlImageSize = new System.Windows.Forms.ComboBox();
            this.lblSize = new System.Windows.Forms.Label();
            this.cbxResizeImages = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbxDestination = new System.Windows.Forms.TextBox();
            this.ddlSource = new CameraDownload.CustomComboBox();
            this.gbxNamingOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFileDigits)).BeginInit();
            this.gbxImageOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxNamingOptions
            // 
            this.gbxNamingOptions.Controls.Add(this.nudFileDigits);
            this.gbxNamingOptions.Controls.Add(this.lblDigits);
            this.gbxNamingOptions.Controls.Add(this.tbxFilePrefix);
            this.gbxNamingOptions.Controls.Add(this.lblPrefix);
            this.gbxNamingOptions.Controls.Add(this.cbxRestartNumbering);
            this.gbxNamingOptions.Controls.Add(this.cbxResumeNumbering);
            this.gbxNamingOptions.Controls.Add(this.cbxRenameFiles);
            this.gbxNamingOptions.Location = new System.Drawing.Point(11, 143);
            this.gbxNamingOptions.Name = "gbxNamingOptions";
            this.gbxNamingOptions.Size = new System.Drawing.Size(429, 76);
            this.gbxNamingOptions.TabIndex = 15;
            this.gbxNamingOptions.TabStop = false;
            this.gbxNamingOptions.Text = "Naming Options";
            // 
            // nudFileDigits
            // 
            this.nudFileDigits.Location = new System.Drawing.Point(365, 45);
            this.nudFileDigits.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFileDigits.Name = "nudFileDigits";
            this.nudFileDigits.Size = new System.Drawing.Size(49, 20);
            this.nudFileDigits.TabIndex = 5;
            this.nudFileDigits.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblDigits
            // 
            this.lblDigits.AutoSize = true;
            this.lblDigits.Location = new System.Drawing.Point(326, 47);
            this.lblDigits.Name = "lblDigits";
            this.lblDigits.Size = new System.Drawing.Size(33, 13);
            this.lblDigits.TabIndex = 4;
            this.lblDigits.Text = "Digits";
            // 
            // tbxFilePrefix
            // 
            this.tbxFilePrefix.Location = new System.Drawing.Point(48, 44);
            this.tbxFilePrefix.MaxLength = 150;
            this.tbxFilePrefix.Name = "tbxFilePrefix";
            this.tbxFilePrefix.Size = new System.Drawing.Size(272, 20);
            this.tbxFilePrefix.TabIndex = 3;
            // 
            // lblPrefix
            // 
            this.lblPrefix.AutoSize = true;
            this.lblPrefix.Location = new System.Drawing.Point(9, 47);
            this.lblPrefix.Name = "lblPrefix";
            this.lblPrefix.Size = new System.Drawing.Size(33, 13);
            this.lblPrefix.TabIndex = 2;
            this.lblPrefix.Text = "Prefix";
            // 
            // cbxRestartNumbering
            // 
            this.cbxRestartNumbering.AutoSize = true;
            this.cbxRestartNumbering.Location = new System.Drawing.Point(231, 19);
            this.cbxRestartNumbering.Name = "cbxRestartNumbering";
            this.cbxRestartNumbering.Size = new System.Drawing.Size(183, 17);
            this.cbxRestartNumbering.TabIndex = 1;
            this.cbxRestartNumbering.Text = "Restart numbering for each folder";
            this.cbxRestartNumbering.UseVisualStyleBackColor = true;
            // 
            // cbxResumeNumbering
            // 
            this.cbxResumeNumbering.AutoSize = true;
            this.cbxResumeNumbering.Location = new System.Drawing.Point(108, 19);
            this.cbxResumeNumbering.Name = "cbxResumeNumbering";
            this.cbxResumeNumbering.Size = new System.Drawing.Size(117, 17);
            this.cbxResumeNumbering.TabIndex = 0;
            this.cbxResumeNumbering.Text = "Resume numbering";
            this.cbxResumeNumbering.UseVisualStyleBackColor = true;
            this.cbxResumeNumbering.CheckedChanged += new System.EventHandler(this.cbxResumeNumbering_CheckedChanged);
            // 
            // cbxRenameFiles
            // 
            this.cbxRenameFiles.AutoSize = true;
            this.cbxRenameFiles.Location = new System.Drawing.Point(11, 19);
            this.cbxRenameFiles.Name = "cbxRenameFiles";
            this.cbxRenameFiles.Size = new System.Drawing.Size(87, 17);
            this.cbxRenameFiles.TabIndex = 14;
            this.cbxRenameFiles.Text = "Rename files";
            this.cbxRenameFiles.UseVisualStyleBackColor = true;
            this.cbxRenameFiles.CheckedChanged += new System.EventHandler(this.cbxRename_CheckedChanged);
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(12, 13);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(41, 13);
            this.lblSource.TabIndex = 16;
            this.lblSource.Text = "Source";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(365, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 18;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(12, 65);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(60, 13);
            this.lblDestination.TabIndex = 19;
            this.lblDestination.Text = "Destination";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(365, 60);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 21;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblDeviceType
            // 
            this.lblDeviceType.AutoSize = true;
            this.lblDeviceType.Location = new System.Drawing.Point(12, 41);
            this.lblDeviceType.Name = "lblDeviceType";
            this.lblDeviceType.Size = new System.Drawing.Size(68, 13);
            this.lblDeviceType.TabIndex = 22;
            this.lblDeviceType.Text = "Device Type";
            // 
            // rbnVideo
            // 
            this.rbnVideo.AutoSize = true;
            this.rbnVideo.Checked = true;
            this.rbnVideo.Location = new System.Drawing.Point(86, 39);
            this.rbnVideo.Name = "rbnVideo";
            this.rbnVideo.Size = new System.Drawing.Size(132, 17);
            this.rbnVideo.TabIndex = 23;
            this.rbnVideo.TabStop = true;
            this.rbnVideo.Text = "Video Camera (*.MOD)";
            this.rbnVideo.UseVisualStyleBackColor = true;
            // 
            // rbnStill
            // 
            this.rbnStill.AutoSize = true;
            this.rbnStill.Location = new System.Drawing.Point(224, 39);
            this.rbnStill.Name = "rbnStill";
            this.rbnStill.Size = new System.Drawing.Size(116, 17);
            this.rbnStill.TabIndex = 24;
            this.rbnStill.Text = "Still Camera (*.JPG)";
            this.rbnStill.UseVisualStyleBackColor = true;
            // 
            // gbxImageOptions
            // 
            this.gbxImageOptions.Controls.Add(this.ddlImageSize);
            this.gbxImageOptions.Controls.Add(this.lblSize);
            this.gbxImageOptions.Controls.Add(this.cbxResizeImages);
            this.gbxImageOptions.Location = new System.Drawing.Point(11, 88);
            this.gbxImageOptions.Name = "gbxImageOptions";
            this.gbxImageOptions.Size = new System.Drawing.Size(429, 49);
            this.gbxImageOptions.TabIndex = 25;
            this.gbxImageOptions.TabStop = false;
            this.gbxImageOptions.Text = "Image Options";
            // 
            // ddlImageSize
            // 
            this.ddlImageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlImageSize.FormattingEnabled = true;
            this.ddlImageSize.Items.AddRange(new object[] {
            "640x480",
            "800x600",
            "1024x768"});
            this.ddlImageSize.Location = new System.Drawing.Point(161, 17);
            this.ddlImageSize.Name = "ddlImageSize";
            this.ddlImageSize.Size = new System.Drawing.Size(84, 21);
            this.ddlImageSize.TabIndex = 2;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(128, 21);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(27, 13);
            this.lblSize.TabIndex = 1;
            this.lblSize.Text = "Size";
            // 
            // cbxResizeImages
            // 
            this.cbxResizeImages.AutoSize = true;
            this.cbxResizeImages.Location = new System.Drawing.Point(11, 20);
            this.cbxResizeImages.Name = "cbxResizeImages";
            this.cbxResizeImages.Size = new System.Drawing.Size(94, 17);
            this.cbxResizeImages.TabIndex = 0;
            this.cbxResizeImages.Text = "Resize images";
            this.cbxResizeImages.UseVisualStyleBackColor = true;
            this.cbxResizeImages.CheckedChanged += new System.EventHandler(this.cbxResize_CheckedChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(191, 225);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 26;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbxDestination
            // 
            this.tbxDestination.Location = new System.Drawing.Point(86, 62);
            this.tbxDestination.Name = "tbxDestination";
            this.tbxDestination.Size = new System.Drawing.Size(273, 20);
            this.tbxDestination.TabIndex = 20;
            // 
            // ddlSource
            // 
            this.ddlSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSource.FormattingEnabled = true;
            this.ddlSource.Location = new System.Drawing.Point(86, 10);
            this.ddlSource.Name = "ddlSource";
            this.ddlSource.Size = new System.Drawing.Size(273, 21);
            this.ddlSource.TabIndex = 17;
            this.ddlSource.SelectedIndexChanged += new System.EventHandler(this.ddlSource_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 262);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.gbxImageOptions);
            this.Controls.Add(this.rbnStill);
            this.Controls.Add(this.rbnVideo);
            this.Controls.Add(this.lblDeviceType);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbxDestination);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.ddlSource);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this.gbxNamingOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Camera Download";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbxNamingOptions.ResumeLayout(false);
            this.gbxNamingOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFileDigits)).EndInit();
            this.gbxImageOptions.ResumeLayout(false);
            this.gbxImageOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxNamingOptions;
        private System.Windows.Forms.NumericUpDown nudFileDigits;
        private System.Windows.Forms.Label lblDigits;
        private System.Windows.Forms.TextBox tbxFilePrefix;
        private System.Windows.Forms.Label lblPrefix;
        private System.Windows.Forms.CheckBox cbxRestartNumbering;
        private System.Windows.Forms.CheckBox cbxResumeNumbering;
        private System.Windows.Forms.CheckBox cbxRenameFiles;
        private System.Windows.Forms.Label lblSource;
        private CustomComboBox ddlSource;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.TextBox tbxDestination;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog fbdBrowser;
        private System.Windows.Forms.Label lblDeviceType;
        private System.Windows.Forms.RadioButton rbnVideo;
        private System.Windows.Forms.RadioButton rbnStill;
        private System.Windows.Forms.GroupBox gbxImageOptions;
        private System.Windows.Forms.CheckBox cbxResizeImages;
        private System.Windows.Forms.ComboBox ddlImageSize;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Button btnStart;

    }
}