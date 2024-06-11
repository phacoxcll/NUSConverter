namespace NUSConverter
{
    partial class NUSConverterGUI
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NUSConverterGUI));
            this.panelCommonKey = new System.Windows.Forms.Panel();
            this.labelCommonKey = new System.Windows.Forms.Label();
            this.textBoxCommonKey = new System.Windows.Forms.TextBox();
            this.buttonChoose = new System.Windows.Forms.Button();
            this.labelFolderName = new System.Windows.Forms.Label();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.labelFormat = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.textBoxFolderName = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTitleID = new System.Windows.Forms.Label();
            this.textBoxTitleID = new System.Windows.Forms.TextBox();
            this.textBoxProductCode = new System.Windows.Forms.TextBox();
            this.labelProductCode = new System.Windows.Forms.Label();
            this.textBoxShortName = new System.Windows.Forms.TextBox();
            this.labelShortName = new System.Windows.Forms.Label();
            this.textBoxLongName = new System.Windows.Forms.TextBox();
            this.labelLongName = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.contextMenuStripIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxTv = new System.Windows.Forms.PictureBox();
            this.contextMenuStripTv = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemTv = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.contextMenuStripIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTv)).BeginInit();
            this.contextMenuStripTv.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCommonKey
            // 
            this.panelCommonKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCommonKey.BackgroundImage = global::NUSConverter.Properties.Resources.x_mark_16;
            this.panelCommonKey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelCommonKey.Location = new System.Drawing.Point(354, 12);
            this.panelCommonKey.Name = "panelCommonKey";
            this.panelCommonKey.Size = new System.Drawing.Size(20, 20);
            this.panelCommonKey.TabIndex = 0;
            this.toolTip.SetToolTip(this.panelCommonKey, "Googling \"wii u common key\". Starts with D7B0...");
            // 
            // labelCommonKey
            // 
            this.labelCommonKey.AutoSize = true;
            this.labelCommonKey.Location = new System.Drawing.Point(12, 15);
            this.labelCommonKey.Name = "labelCommonKey";
            this.labelCommonKey.Size = new System.Drawing.Size(101, 13);
            this.labelCommonKey.TabIndex = 1;
            this.labelCommonKey.Text = "Wii U Common Key:";
            this.toolTip.SetToolTip(this.labelCommonKey, "Googling \"wii u common key\". Starts with D7B0...");
            // 
            // textBoxCommonKey
            // 
            this.textBoxCommonKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCommonKey.Location = new System.Drawing.Point(119, 12);
            this.textBoxCommonKey.Name = "textBoxCommonKey";
            this.textBoxCommonKey.Size = new System.Drawing.Size(229, 20);
            this.textBoxCommonKey.TabIndex = 2;
            this.toolTip.SetToolTip(this.textBoxCommonKey, "Googling \"wii u common key\". Starts with D7B0...");
            this.textBoxCommonKey.TextChanged += new System.EventHandler(this.textBoxCommonKey_TextChanged);
            // 
            // buttonChoose
            // 
            this.buttonChoose.Location = new System.Drawing.Point(12, 38);
            this.buttonChoose.Name = "buttonChoose";
            this.buttonChoose.Size = new System.Drawing.Size(98, 46);
            this.buttonChoose.TabIndex = 3;
            this.buttonChoose.Text = "Choose";
            this.toolTip.SetToolTip(this.buttonChoose, "Choose a folder with NUS Content.\r\nDecrypted format (files, title.cert, title.tik" +
        ", title.tmd, \"*.app\" and \"*.h3\"). \r\nEcrypted format (folders code, content and m" +
        "eta).\r\n");
            this.buttonChoose.UseVisualStyleBackColor = true;
            this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
            // 
            // labelFolderName
            // 
            this.labelFolderName.AutoSize = true;
            this.labelFolderName.Location = new System.Drawing.Point(116, 62);
            this.labelFolderName.Name = "labelFolderName";
            this.labelFolderName.Size = new System.Drawing.Size(39, 13);
            this.labelFolderName.TabIndex = 4;
            this.labelFolderName.Text = "Folder:";
            // 
            // buttonConvert
            // 
            this.buttonConvert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConvert.Enabled = false;
            this.buttonConvert.Location = new System.Drawing.Point(12, 315);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(362, 46);
            this.buttonConvert.TabIndex = 5;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // labelFormat
            // 
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new System.Drawing.Point(116, 43);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new System.Drawing.Size(175, 13);
            this.labelFormat.TabIndex = 6;
            this.labelFormat.Text = "NUS Content format: Indeterminate.";
            // 
            // textBoxFolderName
            // 
            this.textBoxFolderName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolderName.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxFolderName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFolderName.Location = new System.Drawing.Point(154, 62);
            this.textBoxFolderName.Name = "textBoxFolderName";
            this.textBoxFolderName.Size = new System.Drawing.Size(219, 13);
            this.textBoxFolderName.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(374, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(12, 12);
            this.panel1.TabIndex = 8;
            this.toolTip.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // labelTitleID
            // 
            this.labelTitleID.AutoSize = true;
            this.labelTitleID.Location = new System.Drawing.Point(12, 91);
            this.labelTitleID.Name = "labelTitleID";
            this.labelTitleID.Size = new System.Drawing.Size(44, 13);
            this.labelTitleID.TabIndex = 9;
            this.labelTitleID.Text = "Title ID:";
            // 
            // textBoxTitleID
            // 
            this.textBoxTitleID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTitleID.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxTitleID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTitleID.Location = new System.Drawing.Point(92, 91);
            this.textBoxTitleID.Name = "textBoxTitleID";
            this.textBoxTitleID.Size = new System.Drawing.Size(282, 13);
            this.textBoxTitleID.TabIndex = 10;
            // 
            // textBoxProductCode
            // 
            this.textBoxProductCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProductCode.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxProductCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxProductCode.Location = new System.Drawing.Point(92, 110);
            this.textBoxProductCode.Name = "textBoxProductCode";
            this.textBoxProductCode.Size = new System.Drawing.Size(282, 13);
            this.textBoxProductCode.TabIndex = 12;
            // 
            // labelProductCode
            // 
            this.labelProductCode.AutoSize = true;
            this.labelProductCode.Location = new System.Drawing.Point(12, 110);
            this.labelProductCode.Name = "labelProductCode";
            this.labelProductCode.Size = new System.Drawing.Size(74, 13);
            this.labelProductCode.TabIndex = 11;
            this.labelProductCode.Text = "Product code:";
            // 
            // textBoxShortName
            // 
            this.textBoxShortName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxShortName.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxShortName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxShortName.Location = new System.Drawing.Point(92, 129);
            this.textBoxShortName.Name = "textBoxShortName";
            this.textBoxShortName.Size = new System.Drawing.Size(282, 13);
            this.textBoxShortName.TabIndex = 14;
            // 
            // labelShortName
            // 
            this.labelShortName.AutoSize = true;
            this.labelShortName.Location = new System.Drawing.Point(12, 129);
            this.labelShortName.Name = "labelShortName";
            this.labelShortName.Size = new System.Drawing.Size(64, 13);
            this.labelShortName.TabIndex = 13;
            this.labelShortName.Text = "Short name:";
            // 
            // textBoxLongName
            // 
            this.textBoxLongName.AcceptsReturn = true;
            this.textBoxLongName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLongName.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLongName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxLongName.Location = new System.Drawing.Point(92, 148);
            this.textBoxLongName.Multiline = true;
            this.textBoxLongName.Name = "textBoxLongName";
            this.textBoxLongName.Size = new System.Drawing.Size(282, 26);
            this.textBoxLongName.TabIndex = 16;
            // 
            // labelLongName
            // 
            this.labelLongName.AutoSize = true;
            this.labelLongName.Location = new System.Drawing.Point(12, 148);
            this.labelLongName.Name = "labelLongName";
            this.labelLongName.Size = new System.Drawing.Size(63, 13);
            this.labelLongName.TabIndex = 15;
            this.labelLongName.Text = "Long name:";
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.ContextMenuStrip = this.contextMenuStripIcon;
            this.pictureBoxIcon.Location = new System.Drawing.Point(12, 180);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(128, 128);
            this.pictureBoxIcon.TabIndex = 17;
            this.pictureBoxIcon.TabStop = false;
            // 
            // contextMenuStripIcon
            // 
            this.contextMenuStripIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem});
            this.contextMenuStripIcon.Name = "contextMenuStripIcon";
            this.contextMenuStripIcon.Size = new System.Drawing.Size(144, 26);
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItem.Text = "Save image...";
            this.toolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // pictureBoxTv
            // 
            this.pictureBoxTv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxTv.ContextMenuStrip = this.contextMenuStripTv;
            this.pictureBoxTv.Location = new System.Drawing.Point(146, 180);
            this.pictureBoxTv.Name = "pictureBoxTv";
            this.pictureBoxTv.Size = new System.Drawing.Size(228, 128);
            this.pictureBoxTv.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxTv.TabIndex = 18;
            this.pictureBoxTv.TabStop = false;
            // 
            // contextMenuStripTv
            // 
            this.contextMenuStripTv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTv});
            this.contextMenuStripTv.Name = "contextMenuStripTv";
            this.contextMenuStripTv.Size = new System.Drawing.Size(144, 26);
            // 
            // toolStripMenuItemTv
            // 
            this.toolStripMenuItemTv.Name = "toolStripMenuItemTv";
            this.toolStripMenuItemTv.Size = new System.Drawing.Size(143, 22);
            this.toolStripMenuItemTv.Text = "Save image...";
            this.toolStripMenuItemTv.Click += new System.EventHandler(this.toolStripMenuItemTv_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "PNG file (*.png)|*.png|All files (*.*)|*.*";
            // 
            // NUSConverterGUI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 373);
            this.Controls.Add(this.pictureBoxTv);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.textBoxLongName);
            this.Controls.Add(this.labelLongName);
            this.Controls.Add(this.textBoxShortName);
            this.Controls.Add(this.labelShortName);
            this.Controls.Add(this.textBoxProductCode);
            this.Controls.Add(this.labelProductCode);
            this.Controls.Add(this.textBoxTitleID);
            this.Controls.Add(this.labelTitleID);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxFolderName);
            this.Controls.Add(this.labelFormat);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.labelFolderName);
            this.Controls.Add(this.buttonChoose);
            this.Controls.Add(this.textBoxCommonKey);
            this.Controls.Add(this.labelCommonKey);
            this.Controls.Add(this.panelCommonKey);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(402, 412);
            this.Name = "NUSConverterGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NUS Converter v2.1";
            this.toolTip.SetToolTip(this, "Drag and drop a folder with NUS Content.");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NUSConverterGUI_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.NusConverterGUI_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.NusConverterGUI_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.contextMenuStripIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTv)).EndInit();
            this.contextMenuStripTv.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelCommonKey;
        private System.Windows.Forms.Label labelCommonKey;
        private System.Windows.Forms.TextBox textBoxCommonKey;
        private System.Windows.Forms.Button buttonChoose;
        private System.Windows.Forms.Label labelFolderName;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.Label labelFormat;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox textBoxFolderName;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelTitleID;
        private System.Windows.Forms.TextBox textBoxTitleID;
        private System.Windows.Forms.TextBox textBoxProductCode;
        private System.Windows.Forms.Label labelProductCode;
        private System.Windows.Forms.TextBox textBoxShortName;
        private System.Windows.Forms.Label labelShortName;
        private System.Windows.Forms.TextBox textBoxLongName;
        private System.Windows.Forms.Label labelLongName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.PictureBox pictureBoxTv;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTv;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTv;
    }
}

