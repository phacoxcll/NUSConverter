using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NUSConverter.Properties;

namespace NUSConverter
{
    public partial class NUSConverterGUI : Form
    {
        private NUSContent.Format NUSContentFormat;
        private string NUSContentPath;

        public NUSConverterGUI()
        {
            Log.SaveIn("NUSConverter.log");
            Log.WriteLine("NUS Converter v1.0 by phacox.cll");
            Log.WriteLine(DateTime.Now.ToString());

            InitializeComponent();

            string NUSConverterDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NUSConverter");
            string packPath = Path.Combine(NUSConverterDataPath, "pack");
            string unpackPath = Path.Combine(NUSConverterDataPath, "unpack");
            string cnuspackerPath = Path.Combine(packPath, "CNUSPacker.exe");
            string cdecryptPath = Path.Combine(unpackPath, "CDecrypt.exe");

            if (!Directory.Exists(NUSConverterDataPath))
            {
                Directory.CreateDirectory(NUSConverterDataPath);
                Directory.CreateDirectory(packPath);
                Directory.CreateDirectory(unpackPath);
                FileStream fs = File.Create(cnuspackerPath);
                fs.Write(Resources.CNUSPacker, 0, Resources.CNUSPacker.Length);
                fs.Close();
                fs = File.Create(cdecryptPath);
                fs.Write(Resources.CDecrypt, 0, Resources.CDecrypt.Length);
                fs.Close();
                fs = File.Create(Path.Combine(unpackPath, "libeay32.dll"));
                fs.Write(Resources.libeay32, 0, Resources.libeay32.Length);
                fs.Close();
            }

            StringBuilder sb = new StringBuilder();
            bool warning = false;
            if (!File.Exists(cnuspackerPath))
            {
                sb.AppendLine("Warning! \"" + cnuspackerPath + "\" not found! NUSPacker allows you to encrypt NUS Content for WUP Installer.");
                sb.AppendLine("");
                warning = true;
            }
            if (!File.Exists(cdecryptPath))
            {
                sb.AppendLine("Warning! \"" + cdecryptPath + "\" not found! CDecrypt allows you to decrypt NUS Content for Loadiine.");
                warning = true;
            }
            if (warning)
            {
                Log.WriteLine(sb.ToString());
                MessageBox.Show(sb.ToString(), "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (NUSContent.CheckCommonKeyFiles())
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
            if (NUSContent.LoadKey(textBoxCommonKey.Text))
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
                    buttonConvert.Text = "Convert to decrypted format (for Loadiine)";
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
            buttonChoose.Enabled = true;
            buttonConvert.Enabled = true;
        }

        private void SetNUSContent(string path)
        {
            NUSContentPath = path;
            textBoxFolderName.Text = Path.GetFileName(NUSContentPath);
            Log.WriteLine("Path: \"" + NUSContentPath + "\"");
            NUSContentFormat = NUSContent.GetFormat(NUSContentPath);
            if (NUSContentFormat == NUSContent.Format.Encrypted)
            {
                Log.WriteLine("Ecrypted format detected.");
                labelFormat.Text = "NUS Content format: Ecrypted (WUP Installer).";
                buttonConvert.Text = "Convert to decrypted format (for Loadiine)";
                buttonConvert.Enabled = true;
            }
            else if (NUSContentFormat == NUSContent.Format.Decrypted)
            {
                Log.WriteLine("Decrypted format detected.");
                labelFormat.Text = "NUS Content format: Decrypted (Loadiine).";
                buttonConvert.Text = "Convert to ecrypted format (for WUP Installer)";
                buttonConvert.Enabled = true;
            }
            else
            {
                Log.WriteLine("NUS Content format was not detected.");
                labelFormat.Text = "NUS Content format: Indeterminate.";
                buttonConvert.Text = "Convert";
                buttonConvert.Enabled = false;
            }
        }

        private void NUSConverterGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
