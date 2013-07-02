namespace Camurphy.UpdateService
{
	partial class DownloadDialog
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
			this.lblFileCaption = new System.Windows.Forms.Label();
			this.lblUri = new System.Windows.Forms.Label();
			this.pgbProgress = new System.Windows.Forms.ProgressBar();
			this.lblProgressKB = new System.Windows.Forms.Label();
			this.lblProgressPercent = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblFileCaption
			// 
			this.lblFileCaption.AutoSize = true;
			this.lblFileCaption.Location = new System.Drawing.Point(12, 9);
			this.lblFileCaption.Name = "lblFileCaption";
			this.lblFileCaption.Size = new System.Drawing.Size(26, 13);
			this.lblFileCaption.TabIndex = 0;
			this.lblFileCaption.Text = "File:";
			// 
			// lblUri
			// 
			this.lblUri.AutoSize = true;
			this.lblUri.Location = new System.Drawing.Point(44, 9);
			this.lblUri.Name = "lblUri";
			this.lblUri.Size = new System.Drawing.Size(0, 13);
			this.lblUri.TabIndex = 1;
			// 
			// pgbProgress
			// 
			this.pgbProgress.Location = new System.Drawing.Point(12, 32);
			this.pgbProgress.Name = "pgbProgress";
			this.pgbProgress.Size = new System.Drawing.Size(464, 23);
			this.pgbProgress.TabIndex = 2;
			// 
			// lblProgressKB
			// 
			this.lblProgressKB.AutoSize = true;
			this.lblProgressKB.Location = new System.Drawing.Point(9, 67);
			this.lblProgressKB.Name = "lblProgressKB";
			this.lblProgressKB.Size = new System.Drawing.Size(61, 13);
			this.lblProgressKB.TabIndex = 3;
			this.lblProgressKB.Text = "Connecting";
			// 
			// lblProgressPercent
			// 
			this.lblProgressPercent.Location = new System.Drawing.Point(443, 67);
			this.lblProgressPercent.Name = "lblProgressPercent";
			this.lblProgressPercent.Size = new System.Drawing.Size(33, 13);
			this.lblProgressPercent.TabIndex = 4;
			this.lblProgressPercent.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(401, 91);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// DownloadDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(488, 126);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblProgressPercent);
			this.Controls.Add(this.lblProgressKB);
			this.Controls.Add(this.pgbProgress);
			this.Controls.Add(this.lblUri);
			this.Controls.Add(this.lblFileCaption);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "DownloadDialog";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Downloading";
			this.Load += new System.EventHandler(this.DownloadDialog_Load);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadDialog_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblFileCaption;
		private System.Windows.Forms.Label lblUri;
		private System.Windows.Forms.ProgressBar pgbProgress;
		private System.Windows.Forms.Label lblProgressKB;
		private System.Windows.Forms.Label lblProgressPercent;
		private System.Windows.Forms.Button btnCancel;
	}
}