using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Camurphy.CameraDownload
{
    public enum MediaType { Video = 0, Image = 1 }
    public enum ImageSize { Small = 0, Medium = 1, Large = 2 }

    public partial class MainForm : Form
    {
        const int CD_IMAGE_OPTIONS_OFFSET = 55;

        private RadioButtonGroup _mediaType;
        private bool _videoCameraEnabled = true, _imageOptionsVisible = true, _imageOptionsEnabled = true,
            _namingOptionsEnabled = true;

        public MainForm()
        {
            InitializeComponent();

            _mediaType = new RadioButtonGroup();
            _mediaType.Add(rbnVideo);
            _mediaType.Add(rbnStill);
            _mediaType.CheckedChanged += new EventHandler(DeviceTypeChangedHandler);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            UpdateService.Worker.Instance.CheckForUpdates();
            ReloadSourceList();
            LoadSettings();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Source = ddlSource.Items[ddlSource.SelectedIndex].ToString();
            Properties.Settings.Default.MediaType = _mediaType.SelectedIndex;
            Properties.Settings.Default.ResizeImages = cbxResizeImages.Checked;
            Properties.Settings.Default.ResizedImageSize = ddlImageSize.SelectedIndex;

            if (_mediaType.SelectedIndex == (int)MediaType.Video)
            {
                Properties.Settings.Default.Video_SettingsSaved = true;
                Properties.Settings.Default.Video_Destination = tbxDestination.Text;
                Properties.Settings.Default.Video_RenameFiles = cbxRenameFiles.Checked;
                Properties.Settings.Default.Video_ResumeNumbering = cbxResumeNumbering.Checked;
                Properties.Settings.Default.Video_RestartNumbering = cbxRestartNumbering.Checked;
                Properties.Settings.Default.Video_FilePrefix = tbxFilePrefix.Text;
                Properties.Settings.Default.Video_FileDigits = (int)nudFileDigits.Value;
            }
            else
            {
                Properties.Settings.Default.Still_SettingsSaved = true;
                Properties.Settings.Default.Still_Destination = tbxDestination.Text;
                Properties.Settings.Default.Still_RenameFiles = cbxRenameFiles.Checked;
                Properties.Settings.Default.Still_ResumeNumbering = cbxResumeNumbering.Checked;
                Properties.Settings.Default.Still_RestartNumbering = cbxRestartNumbering.Checked;
                Properties.Settings.Default.Still_FilePrefix = tbxFilePrefix.Text;
                Properties.Settings.Default.Still_FileDigits = (int)nudFileDigits.Value;
            }               

            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            if (ddlSource.SelectedIndex == -1)
                ddlSource.SelectedIndex = ddlSource.IndexOf(Properties.Settings.Default.Source);

            ddlSource_SelectedIndexChanged(null, null);

            // Only set the media type if Video Camera (default) is selected. Avoids
            // setting device type to Video Camera when ReloadSouceList has already set
            // it to Still Camera. This can occur when only a still camera was detected
            if (_mediaType.SelectedIndex == 0)
            {
                _mediaType.SelectedIndex = Properties.Settings.Default.MediaType;
                DeviceTypeChangedHandler(null, null);
            }

            cbxResizeImages.Checked = Properties.Settings.Default.ResizeImages;
            ddlImageSize.SelectedIndex = Properties.Settings.Default.ResizedImageSize;
        }

        private void ReloadSourceList()
        {
            ddlSource.Items.Clear();

            List<MediaDevice> devices = DeviceFactory.GetAllDevices();
            foreach (MediaDevice device in devices)
                ddlSource.Items.Add(device);

            if (devices.Count == 1)
            {
                ddlSource.SelectedIndex = 0;
                ddlSource_SelectedIndexChanged(this, null);
            }
        }

        private bool ValidateForm()
        {
            if (ddlSource.SelectedIndex == -1)
                MessageBox.Show(this, Properties.Resources.SourceNullError);
            else if (tbxDestination.Text.Length == 0)
                MessageBox.Show(this, Properties.Resources.DestinationNullError);
            else if (!Program.IsValidPath(tbxDestination.Text))
                MessageBox.Show(this, Properties.Resources.InvalidDestinationError);
            else if (!Program.IsValidFilename(tbxFilePrefix.Text))
                MessageBox.Show(this, Properties.Resources.InvalidPrefixError);
            else
                return true;

            return false;
        }

        private void HideImageOptions()
        {
            if (_imageOptionsVisible)
            {
                gbxImageOptions.Visible = false;
                _imageOptionsVisible = false;
                gbxNamingOptions.Top -= CD_IMAGE_OPTIONS_OFFSET;
                btnStart.Top -= CD_IMAGE_OPTIONS_OFFSET;
                this.Height -= CD_IMAGE_OPTIONS_OFFSET;
            }
        }

        private void ShowImageOptions()
        {
            if (!_imageOptionsVisible)
            {
                gbxImageOptions.Visible = true;
                _imageOptionsVisible = true;
                gbxNamingOptions.Top += CD_IMAGE_OPTIONS_OFFSET;
                btnStart.Top += CD_IMAGE_OPTIONS_OFFSET;
                this.Height += CD_IMAGE_OPTIONS_OFFSET;
            }
        }

        private void UpdateImageOptionsState()
        {
            if (_imageOptionsEnabled != cbxResizeImages.Checked)
            {
                if (cbxResizeImages.Checked)
                {
                    ddlImageSize.Enabled = true;
                    _imageOptionsEnabled = true;
                }
                else
                {
                    ddlImageSize.Enabled = false;
                    _imageOptionsEnabled = false;
                }
            }
        }

        private void UpdateNamingOptionsState()
        {
            if (_namingOptionsEnabled != cbxRenameFiles.Checked)
            {
                if (cbxRenameFiles.Checked)
                {
                    cbxResumeNumbering.Enabled = true;
                    if (!cbxResumeNumbering.Checked)
                        cbxRestartNumbering.Enabled = true;
                    tbxFilePrefix.Enabled = true;
                    nudFileDigits.Enabled = true;
                    _namingOptionsEnabled = true;
                }
                else
                {
                    cbxResumeNumbering.Enabled = false;
                    cbxRestartNumbering.Enabled = false;
                    tbxFilePrefix.Enabled = false;
                    nudFileDigits.Enabled = false;
                    _namingOptionsEnabled = false;
                }
            }
        }

        private void UpdateImageOptionsVisibility()
        {
            if (_mediaType.SelectedIndex == (int)MediaType.Video)
                HideImageOptions();
            else
                ShowImageOptions();
        }

        private void DeviceTypeChangedHandler(object sender, EventArgs e)
        {
            UpdateImageOptionsVisibility();

            if (_mediaType.SelectedIndex == (int)MediaType.Video)
            {
                if (Properties.Settings.Default.Video_SettingsSaved)
                {
                    tbxDestination.Text = Properties.Settings.Default.Video_Destination;
                    cbxRenameFiles.Checked = Properties.Settings.Default.Video_RenameFiles;
                    cbxResumeNumbering.Checked = Properties.Settings.Default.Video_ResumeNumbering;
                    cbxRestartNumbering.Checked = Properties.Settings.Default.Video_RestartNumbering;
                    tbxFilePrefix.Text = Properties.Settings.Default.Video_FilePrefix;
                    nudFileDigits.Value = (decimal)Properties.Settings.Default.Video_FileDigits;
                }
            }
            else
            {
                if (Properties.Settings.Default.Still_SettingsSaved)
                {
                    tbxDestination.Text = Properties.Settings.Default.Still_Destination;
                    cbxRenameFiles.Checked = Properties.Settings.Default.Still_RenameFiles;
                    cbxResumeNumbering.Checked = Properties.Settings.Default.Still_ResumeNumbering;
                    cbxRestartNumbering.Checked = Properties.Settings.Default.Still_RestartNumbering;
                    tbxFilePrefix.Text = Properties.Settings.Default.Still_FilePrefix;
                    nudFileDigits.Value = (decimal)Properties.Settings.Default.Still_FileDigits;                    
                }
            }

            UpdateImageOptionsState();
            UpdateNamingOptionsState();
            cbxResumeNumbering_CheckedChanged(null, null);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (Program.IsValidPath(tbxDestination.Text))
                fbdBrowser.SelectedPath = tbxDestination.Text;

            if (fbdBrowser.ShowDialog() == DialogResult.OK)
                tbxDestination.Text = fbdBrowser.SelectedPath;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadSourceList();
        }

        private void cbxResize_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImageOptionsState();
        }

        private void cbxRename_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNamingOptionsState();
        }

        private void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSource.SelectedIndex >= 0)
            {
                if (ddlSource.Items[ddlSource.SelectedIndex] is StillCamera)
                {
                    if (_mediaType.SelectedIndex != (int)MediaType.Image)
                        _mediaType.SelectedIndex = (int)MediaType.Image;

                    if (_videoCameraEnabled)
                    {
                        rbnVideo.Enabled = false;
                        _videoCameraEnabled = false;
                    }
                }
                else if (!_videoCameraEnabled)
                {
                    rbnVideo.Enabled = true;
                    _videoCameraEnabled = true;
                }

                UpdateImageOptionsVisibility();
            }
        }

        private void cbxResumeNumbering_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxResumeNumbering.Checked)
            {
                cbxRestartNumbering.Enabled = false;
            }
            else if (_namingOptionsEnabled)
            {
                cbxRestartNumbering.Enabled = true;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SaveSettings();

                Worker.Instance.Settings = new WorkerSettings
                {
                    Device = (MediaDevice)ddlSource.Items[ddlSource.SelectedIndex],
                    MediaType = (MediaType)_mediaType.SelectedIndex,
                    DestinationDirectory = tbxDestination.Text,
                    ResizeImages = cbxResizeImages.Checked,
                    ImageSize = (ImageSize)ddlImageSize.SelectedIndex,
                    RenameFiles = cbxRenameFiles.Checked,
                    ResumeNumbering = cbxResumeNumbering.Checked,
                    RestartNumbering = cbxRestartNumbering.Checked,
                    FilePrefix = tbxFilePrefix.Text,
                    FileDigits = (int)nudFileDigits.Value
                };                    
                    
                Worker.Instance.Begin();

                this.Hide();
            }
        }
    }
}