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
            this.SuspendLayout();
            // 
            // panelCommonKey
            // 
            this.panelCommonKey.BackgroundImage = global::NUSConverter.Properties.Resources.x_mark_16;
            this.panelCommonKey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelCommonKey.Location = new System.Drawing.Point(341, 12);
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
            this.textBoxCommonKey.Location = new System.Drawing.Point(119, 12);
            this.textBoxCommonKey.Name = "textBoxCommonKey";
            this.textBoxCommonKey.Size = new System.Drawing.Size(216, 20);
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
            this.buttonConvert.Enabled = false;
            this.buttonConvert.Location = new System.Drawing.Point(12, 90);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(349, 46);
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
            this.textBoxFolderName.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxFolderName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFolderName.Location = new System.Drawing.Point(154, 62);
            this.textBoxFolderName.Name = "textBoxFolderName";
            this.textBoxFolderName.Size = new System.Drawing.Size(206, 13);
            this.textBoxFolderName.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(360, 135);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(12, 12);
            this.panel1.TabIndex = 8;
            this.toolTip.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // NUSConverterGUI
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 148);
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
            this.MaximumSize = new System.Drawing.Size(389, 187);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(389, 187);
            this.Name = "NUSConverterGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NUS Converter v1.1";
            this.toolTip.SetToolTip(this, "Drag and drop a folder with NUS Content.");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NUSConverterGUI_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.NusConverterGUI_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.NusConverterGUI_DragEnter);
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
        private System.Windows.Forms.Panel panel1;
    }
}

