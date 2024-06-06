using System;
using System.IO;
using System.Text;

namespace NUSConverter
{
    class NUSConverterCMD
    {
        public NUSConverterCMD()
        {
            string warning = NUSConverterBase.Initialize();

            if (warning.Length != 0)
                Log.WriteLine(warning);

            if (NUSConverterBase.CheckCommonKeyFiles())
                Log.WriteLine("Wii U Common Key files: OK!");
            else
                Log.WriteLine("Wii U Common Key files: Not found.");
        }

        public void Run(string[] args)
        {
            if (args.Length == 1)
            {
                Log.WriteLine("Path: \"" + args[0] + "\"");
                if (NUSConverterBase.CheckCommonKeyFiles())
                {
                    NUSConverterBase.CheckBatchFiles();
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
                if (NUSConverterBase.LoadKey(args[1]))
                {
                    Log.WriteLine("Valid Wii U Common Key.");
                    Log.WriteLine("The key was successfully loaded!");
                }
                else
                    Log.WriteLine("Invalid Wii U Common Key!");
            }
            else
            {
                if (!NUSConverterBase.CheckCommonKeyFiles())
                    Log.WriteLine("To load the Wii U Common Key use: key XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
                Log.WriteLine("");
                Log.WriteLine("Usage: <input path>");
            }
        }
    }
}