using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NUSConverter.Properties;

namespace NUSConverter
{
    class NUSConverterCMD
    {
        public NUSConverterCMD()
        {
            Log.SaveIn("NUSConverter.log");
            Log.WriteLine("NUS Converter v1.0 by phacox.cll");
            Log.WriteLine(DateTime.Now.ToString());

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
            Log.WriteLine(cnuspackerPath);
            bool warning = false;
            if (!File.Exists(cnuspackerPath))
            {
                sb.AppendLine("Warning! \"" + cnuspackerPath + "\" not found! CNUSPacker allows you to encrypt NUS Content for WUP Installer.");
                sb.AppendLine("");
                warning = true;
            }
            if (!File.Exists(cdecryptPath))
            {
                sb.AppendLine("Warning! \"" + cdecryptPath + "\" not found! CDecrypt allows you to decrypt NUS Content for Loadiine.");
                warning = true;
            }
            if (warning)            
                Log.WriteLine(sb.ToString());            

            if (NUSContent.CheckCommonKeyFiles())
                Log.WriteLine("Wii U Common Key files: OK!");
            else
                Log.WriteLine("Wii U Common Key files: Not found.");
        }

        public void Run(string[] args)
        {
            if (args.Length == 1)
            {
                Log.WriteLine("Path: \"" + args[0] + "\"");
                if (NUSContent.CheckCommonKeyFiles())
                {
                    try
                    {                        
                        NUSContent.Format format = NUSContent.GetFormat(args[0]);
                        string output = args[0];
                        if (format == NUSContent.Format.Encrypted)
                        {
                            Log.WriteLine("Ecrypted format detected.");
                            output += " (Decrypted)";
                            Log.WriteLine("Input: \"" + args[0] + "\"");
                            Log.WriteLine("Output: \"" + output + "\"");
                            Log.WriteLine("Decrypting...");
                            Directory.CreateDirectory(output);
                            NUSContent.Decrypt(args[0], output);
                            Log.WriteLine("Decrypted!");
                            MessageBox.Show("Output: \"" + output + "\"", "Decrypted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (format == NUSContent.Format.Decrypted)
                        {
                            Log.WriteLine("Decrypted format detected.");
                            output += " (Encrypted)";
                            Log.WriteLine("Input: \"" + args[0] + "\"");
                            Log.WriteLine("Output: \"" + output + "\"");
                            Log.WriteLine("Encrypting...");
                            NUSContent.Encrypt(args[0], output);
                            Log.WriteLine("Encrypted!");
                            MessageBox.Show("Output: \"" + output + "\"", "Encrypted!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            Log.WriteLine("NUS Content format was not detected.");
                    }
                    catch (Exception e)
                    {
                        Log.WriteLine(e.ToString());
                    }
                }
                else
                {
                    Log.WriteLine("First load the Wii U Common Key!");
                    Log.WriteLine("Use: key XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                }
            }
            else if (args.Length == 2 && args[0] == "key")
            {
                if (NUSContent.LoadKey(args[1]))
                {
                    Log.WriteLine("Valid Wii U Common Key.");
                    Log.WriteLine("The key was successfully loaded!");
                }
                else
                    Log.WriteLine("Invalid Wii U Common Key!");
            }
            else
            {
                if (!NUSContent.CheckCommonKeyFiles())
                    Log.WriteLine("To load the Wii U Common Key use: key XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                Log.WriteLine("");
                Log.WriteLine("Usage: <input path>");
            }
        }
    }
}