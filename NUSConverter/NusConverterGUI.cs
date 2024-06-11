using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NUSConverter
{
    public partial class NUSConverterGUI : Form
    {
        NUSContent.Meta MetaData;

        private NUSContent.Format NUSContentFormat;
        private string NUSContentPath;

        public NUSConverterGUI()
        {
            InitializeComponent();

            string warning = NUSConverterBase.Initialize();
            if (warning.Length != 0)
            {
                Log.WriteLine(warning);
                MessageBox.Show(warning, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (NUSConverterBase.CheckCommonKeyFiles())
            {
                Log.WriteLine("Wii U Common Key files: OK!");
                textBoxCommonKey.Enabled = false;
                panelCommonKey.BackgroundImage = Properties.Resources.checkmark_16;
            }
            else
            {
                Log.WriteLine("Wii U Common Key files: Not found.");
                textBoxCommonKey.Enabled = true;
                panelCommonKey.BackgroundImage = Properties.Resources.x_mark_16;
            }
        }

        private void textBoxCommonKey_TextChanged(object sender, EventArgs e)
        {
            if (NUSConverterBase.LoadKey(textBoxCommonKey.Text))
            {
                textBoxCommonKey.Text = "";
                textBoxCommonKey.Enabled = false;
                panelCommonKey.BackgroundImage = Properties.Resources.checkmark_16;
                Log.WriteLine("Valid Wii U Common Key.");
                Log.WriteLine("The key was successfully loaded!");
            }
            else
            {
                textBoxCommonKey.Enabled = true;
                panelCommonKey.BackgroundImage = Properties.Resources.x_mark_16;
                Log.WriteLine("Invalid Wii U Common Key: " + textBoxCommonKey.Text);
            }
        }
        
        private void NusConverterGUI_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void NusConverterGUI_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileList.Length == 1 && Directory.Exists(fileList[0]))
            {
                Properties.Settings.Default.InputPath = fileList[0];
                SetNUSContent(fileList[0]);
            }
        }

        private void buttonChoose_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = Properties.Settings.Default.InputPath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.InputPath = folderBrowserDialog.SelectedPath;
                SetNUSContent(folderBrowserDialog.SelectedPath);
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            buttonChoose.Enabled = false;            
            buttonConvert.Enabled = false;
            buttonConvert.Text = "Working...";
            if (NUSConverterBase.CheckCommonKeyFiles())
            {
                NUSConverterBase.CheckBatchFiles();
                try
                {
                    string output = NUSContentPath;
                    if (NUSContentFormat == NUSContent.Format.Encrypted)
                    {
                        output += " (Decrypted)";
                        Log.WriteLine("Input: \"" + NUSContentPath + "\"");
                        Log.WriteLine("Output: \"" + output + "\"");
                        Log.WriteLine("Decrypting...");
                        Directory.CreateDirectory(output);
                        NUSContent.Decrypt(NUSContentPath, output);
                        buttonConvert.Text = "Convert to decrypted format (for CEMU/Loadiine)";
                        Log.WriteLine("Decrypted!");
                        MessageBox.Show("Output: \"" + output + "\"", "Decrypted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (NUSContentFormat == NUSContent.Format.Decrypted)
                    {
                        output += " (Encrypted)";
                        Log.WriteLine("Input: \"" + NUSContentPath + "\"");
                        Log.WriteLine("Output: \"" + output + "\"");
                        Log.WriteLine("Encrypting...");
                        NUSContent.Encrypt(NUSContentPath, output);
                        buttonConvert.Text = "Convert to ecrypted format (for WUP Installer)";
                        Log.WriteLine("Encrypted!");
                        MessageBox.Show("Output: \"" + output + "\"", "Encrypted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        throw new Exception("NUS Content format was not detected.");
                }
                catch (Exception ex)
                {
                    Log.WriteLine(ex.ToString());
                    MessageBox.Show(ex.ToString(), "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetNUSContent(NUSContentPath);
                }
            }
            else
            {
                MessageBox.Show("First load the Wii U Common Key!", "Invalid Wii U Common Key!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonConvert.Text = "Convert";
            }
            buttonChoose.Enabled = true;
            buttonConvert.Enabled = true;
        }

        private void SetNUSContent(string path)
        {
            NUSContentPath = path;
            textBoxFolderName.Text = Path.GetFileName(NUSContentPath);
            Log.WriteLine("Path: \"" + NUSContentPath + "\"");

            labelFormat.Text = "NUS Content format: Indeterminate.";
            buttonConvert.Text = "Convert";
            buttonConvert.Enabled = false;

            textBoxTitleID.Text = "";
            textBoxProductCode.Text = "";
            textBoxShortName.Text = "";
            textBoxLongName.Text = "";

            pictureBoxIcon.Image = null;
            pictureBoxTv.Image = null;

            NUSContentFormat = NUSContent.GetFormat(NUSContentPath);
            if (NUSContentFormat == NUSContent.Format.Encrypted)
            {
                Log.WriteLine("Ecrypted format detected.");
                labelFormat.Text = "NUS Content format: Ecrypted (WUP Installer).";
                buttonConvert.Text = "Convert to decrypted format (for CEMU/Loadiine)";
                buttonConvert.Enabled = true;

                if (NUSConverterBase.CheckCommonKeyFiles())
                {
                    NUSConverterBase.CheckBatchFiles();
                    NUSContent.Decrypt(path, Path.Combine("meta", "meta.xml"), Path.Combine(Path.GetTempPath(), "NUSConverter", "meta", "meta.xml"));
                    NUSContent.Decrypt(path, Path.Combine("meta", "iconTex.tga"), Path.Combine(Path.GetTempPath(), "NUSConverter", "meta", "iconTex.tga"));
                    NUSContent.Decrypt(path, Path.Combine("meta", "bootTvTex.tga"), Path.Combine(Path.GetTempPath(), "NUSConverter", "meta", "bootTvTex.tga"));

                    try
                    {
                        MetaData = NUSContent.GetMeta(Path.Combine(Path.GetTempPath(), "NUSConverter"));
                    }
                    catch
                    {
                        NUSConverterBase.UpdateApps();

                        NUSContent.Decrypt(path, Path.Combine("meta", "meta.xml"), Path.Combine(Path.GetTempPath(), "NUSConverter", "meta", "meta.xml"));
                        NUSContent.Decrypt(path, Path.Combine("meta", "iconTex.tga"), Path.Combine(Path.GetTempPath(), "NUSConverter", "meta", "iconTex.tga"));
                        NUSContent.Decrypt(path, Path.Combine("meta", "bootTvTex.tga"), Path.Combine(Path.GetTempPath(), "NUSConverter", "meta", "bootTvTex.tga"));

                        MetaData = NUSContent.GetMeta(Path.Combine(Path.GetTempPath(), "NUSConverter"));
                    }

                    textBoxTitleID.Text = MetaData.TitleID;
                    textBoxProductCode.Text = MetaData.ProductCode;
                    textBoxShortName.Text = MetaData.ShortName;
                    textBoxLongName.Text = MetaData.LongName.Replace("\n", "\r\n");

                    pictureBoxIcon.Image = NUSContent.TGAToBitmap(Path.Combine(Path.GetTempPath(), "NUSConverter", "meta", "iconTex.tga"));
                    pictureBoxTv.Image = NUSContent.TGAToBitmap(Path.Combine(Path.GetTempPath(), "NUSConverter", "meta", "bootTvTex.tga"));
                }
                else
                {
                    MessageBox.Show("First load the Wii U Common Key!", "Invalid Wii U Common Key!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (NUSContentFormat == NUSContent.Format.Decrypted)
            {
                Log.WriteLine("Decrypted format detected.");
                labelFormat.Text = "NUS Content format: Decrypted (CEMU/Loadiine).";
                buttonConvert.Text = "Convert to ecrypted format (for WUP Installer)";
                buttonConvert.Enabled = true;

                MetaData = NUSContent.GetMeta(path);

                textBoxTitleID.Text = MetaData.TitleID;
                textBoxProductCode.Text = MetaData.ProductCode;
                textBoxShortName.Text = MetaData.ShortName;
                textBoxLongName.Text = MetaData.LongName.Replace("\n", "\r\n");

                pictureBoxIcon.Image = NUSContent.TGAToBitmap(Path.Combine(path, "meta", "iconTex.tga"));
                pictureBoxTv.Image = NUSContent.TGAToBitmap(Path.Combine(path, "meta", "bootTvTex.tga"));
            }
            else
            {
                Log.WriteLine("NUS Content format was not detected.");
            }
        }

        private void NUSConverterGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Directory.Exists(Path.Combine(Path.GetTempPath(), "NUSConverter")))
                Directory.Delete(Path.Combine(Path.GetTempPath(), "NUSConverter"), true);

            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBoxIcon.Image != null)
            {
                saveFileDialog.FileName = "";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxIcon.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void toolStripMenuItemTv_Click(object sender, EventArgs e)
        {
            if (pictureBoxTv.Image != null)
            {
                saveFileDialog.FileName = "";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBoxTv.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
}
