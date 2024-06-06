using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NUSConverter
{
    public static class NUSContent
    {
        public const uint CommonKeyHashCRC32 = 0xB92B703D;

        public enum Format
        {
            Encrypted,
            Decrypted,
            Indeterminate
        }

        public static void Check(string path)
        {
            if (!Directory.Exists(Path.Combine(path, "code")))
                throw new Exception("The \"" + Path.Combine(path, "code") + "\" folder not exist.");
            if (!Directory.Exists(Path.Combine(path, "content")))
                throw new Exception("The \"" + Path.Combine(path, "content") + "\" folder not exist.");
            if (!Directory.Exists(Path.Combine(path, "meta")))
                throw new Exception("The \"" + Path.Combine(path, "meta") + "\" folder not exist.");
        }

        public static void CheckEncrypted(string path)
        {
            if (!File.Exists(Path.Combine(path, "title.tmd")))
                throw new Exception("The \"" + Path.Combine(path, "title.tmd") + "\" file not exist.");
            if (!File.Exists(Path.Combine(path, "title.tik")))
                throw new Exception("The \"" + Path.Combine(path, "title.tik") + "\" file not exist.");
            /*if (!File.Exists(Path.Combine(path, "title.cert")))
                throw new Exception("The \"" + Path.Combine(path, "title.cert") + "\" file not exist.");*/
        }

        public static Format GetFormat(string path)
        {
            Format format = Format.Indeterminate;
            try
            {
                CheckEncrypted(path);
                format = Format.Encrypted;
            }
            catch
            {
                try
                {
                    Check(path);
                    format = Format.Decrypted;
                }
                catch
                {
                }
            }

            return format;
        }

        public static void CheckBatchFiles()
        {
            string NUSConverterDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NUSConverter");
            string packPath = Path.Combine(NUSConverterDataPath, "pack");
            string unpackPath = Path.Combine(NUSConverterDataPath, "unpack");

            if (!Directory.Exists(NUSConverterDataPath))
                throw new Exception("The \"" + NUSConverterDataPath + "\" folder not exist.");
            if (!Directory.Exists(packPath))
                throw new Exception("The \"" + packPath + "\" folder not exist.");
            if (!Directory.Exists(unpackPath))
                throw new Exception("The \"" + unpackPath + "\" folder not exist.");

            string packRunPath = Path.Combine(packPath, "run.bat");
            string unpackRunPath = Path.Combine(unpackPath, "run.bat");

            if (!File.Exists(packRunPath))
            {
                StreamWriter sw = File.CreateText(packRunPath);
                sw.WriteLine("@echo off");
                sw.WriteLine("cd \"" + packPath + "\"");
                sw.Write("CNUSPacker.exe -in %1 -out %2");
                sw.Close();
            }

            if (!File.Exists(unpackRunPath))
            {
                StreamWriter sw = File.CreateText(unpackRunPath);
                sw.WriteLine("@echo off");
                sw.WriteLine("cd \"" + unpackPath + "\"");
                sw.Write("CDecrypt.exe %1 %2");
                sw.Close();
            }
        }

        private static bool IsValidCommonKey(string key)
        {
            byte[] array = Encoding.ASCII.GetBytes(key.ToUpper());
            uint hash = Security.ComputeCRC32(array, 0, array.Length);
            if (hash == CommonKeyHashCRC32)
                return true;
            return false;
        }

        public static bool LoadKey(string key)
        {
            if (IsValidCommonKey(key))
            {
                try
                {
                    string NUSConverterDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NUSConverter");
                    string packPath = Path.Combine(NUSConverterDataPath, "pack");
                    string unpackPath = Path.Combine(NUSConverterDataPath, "unpack");

                    if (!Directory.Exists(NUSConverterDataPath))
                        throw new Exception("The \"" + NUSConverterDataPath + "\" folder not exist.");
                    if (!Directory.Exists(packPath))
                        throw new Exception("The \"" + packPath + "\" folder not exist.");
                    if (!Directory.Exists(unpackPath))
                        throw new Exception("The \"" + unpackPath + "\" folder not exist.");

                    StreamWriter sw = File.CreateText(Path.Combine(packPath, "encryptKeyWith"));
                    sw.WriteLine(key.ToUpper());
                    sw.Close();
                    sw = File.CreateText(Path.Combine(unpackPath, "keys.txt"));
                    sw.WriteLine(key.ToUpper());
                    sw.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public static bool CheckCommonKeyFiles()
        {
            string NUSConverterDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NUSConverter");
            string packPath = Path.Combine(NUSConverterDataPath, "pack");
            string unpackPath = Path.Combine(NUSConverterDataPath, "unpack");

            StreamReader sr;
            StreamWriter sw;
            string cdecryptKey = "";
            string nuspackerKey = "";
            bool cdecryptKeyValid = false;
            bool nuspackerKeyValid = false;

            if (File.Exists(Path.Combine(unpackPath, "keys.txt")))
            {
                sr = File.OpenText(Path.Combine(unpackPath, "keys.txt"));
                cdecryptKey = sr.ReadLine();
                sr.Close();
                cdecryptKeyValid = IsValidCommonKey(cdecryptKey);
            }

            if (File.Exists(Path.Combine(packPath, "encryptKeyWith")))
            {
                sr = File.OpenText(Path.Combine(packPath, "encryptKeyWith"));
                nuspackerKey = sr.ReadLine();
                sr.Close();
                nuspackerKeyValid = IsValidCommonKey(nuspackerKey);
            }

            if (cdecryptKeyValid && nuspackerKeyValid)
                return true;
            else if (cdecryptKeyValid && !nuspackerKeyValid)
            {
                try
                {
                    sw = File.CreateText(Path.Combine(packPath, "encryptKeyWith"));
                    sw.WriteLine(cdecryptKey);
                    sw.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else if (!cdecryptKeyValid && nuspackerKeyValid)
            {
                try
                {
                    sw = File.CreateText(Path.Combine(unpackPath, "keys.txt"));
                    sw.WriteLine(nuspackerKey);
                    sw.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }

        public static void Encrypt(string inputPath, string outputPath)
        {
            string packPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NUSConverter"), "pack");

            Check(inputPath);
            if (!CheckCommonKeyFiles())
                throw new Exception("Common Key Files error.");
            if (!File.Exists(Path.Combine(packPath, "CNUSPacker.exe")))
                throw new Exception("The \"" + Path.Combine(packPath, "CNUSPacker.exe") + "\" file not exist.");

            CheckBatchFiles();
            Process encrypt = Process.Start(Path.Combine(packPath, "run.bat"), "\"" + inputPath + "\" \"" + outputPath + "\"");
            encrypt.WaitForExit();

            if (encrypt.ExitCode == 0)
                encrypt.Dispose();
            else
            {
                encrypt.Dispose();
                throw new Exception("Encrypt fail.");
            }
        }

        public static void Decrypt(string inputPath, string outputPath)
        {
            string unpackPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NUSConverter"), "unpack");

            CheckEncrypted(inputPath);
            if (!CheckCommonKeyFiles())
                throw new Exception("Common Key Files error.");
            /*if (!File.Exists(Path.Combine(unpackPath, "libeay32.dll")))
                throw new Exception("The \"" + Path.Combine(unpackPath, "libeay32.dll") + "\" file not exist.");*/
            if (!File.Exists(Path.Combine(unpackPath, "CDecrypt.exe")))
                throw new Exception("The \"" + Path.Combine(unpackPath, "CDecrypt.exe") + "\" file not exist.");

            CheckBatchFiles();
            Process decrypt = Process.Start(Path.Combine(unpackPath, "run.bat"), "\"" + inputPath + "\" \"" + outputPath + "\"");
            decrypt.WaitForExit();

            if (decrypt.ExitCode == 0)
                decrypt.Dispose();
            else
            {
                decrypt.Dispose();
                throw new Exception("Decrypt fail.");
            }
        }
    }
}
