using System;
using System.IO;
using System.Text;
using NUSConverter.Properties;

namespace NUSConverter
{
    public static class NUSConverterBase
    {
        public static string Initialize()
        {
            Log.SaveIn("NUSConverter.log");
            Log.WriteLine("NUS Converter v2.1 by phacox.cll");
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
            }

            StringBuilder sb = new StringBuilder();
            Log.WriteLine(cnuspackerPath);
            if (!File.Exists(cnuspackerPath))
            {
                sb.AppendLine("Warning! \"" + cnuspackerPath + "\" not found! NUSPacker allows you to encrypt NUS Content for WUP Installer.");
                sb.AppendLine("");
            }
            if (!File.Exists(cdecryptPath))
            {
                sb.AppendLine("Warning! \"" + cdecryptPath + "\" not found! CDecrypt allows you to decrypt NUS Content for CEMU/Loadiine.");
            }

            return sb.ToString();
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
            string unpackGetFilePath = Path.Combine(unpackPath, "getfile.bat");

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

            if (!File.Exists(unpackGetFilePath))
            {
                StreamWriter sw = File.CreateText(unpackGetFilePath);
                sw.WriteLine("@echo off");
                sw.WriteLine("cd \"" + unpackPath + "\"");
                sw.Write("CDecrypt.exe %1 %2 %3");
                sw.Close();
            }
        }

        private static bool IsValidCommonKey(string key)
        {
            byte[] array = Encoding.ASCII.GetBytes(key.ToUpper());
            uint hash = Security.ComputeCRC32(array, 0, array.Length);
            if (hash == NUSContent.CommonKeyCRC32)
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

        public static void UpdateApps()
        {
            string NUSConverterDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NUSConverter");
            string packPath = Path.Combine(NUSConverterDataPath, "pack");
            string unpackPath = Path.Combine(NUSConverterDataPath, "unpack");
            string cnuspackerPath = Path.Combine(packPath, "CNUSPacker.exe");
            string cdecryptPath = Path.Combine(unpackPath, "CDecrypt.exe");
            string libeay32Path = Path.Combine(unpackPath, "libeay32.dll");
            string packRunPath = Path.Combine(packPath, "run.bat");
            string unpackRunPath = Path.Combine(unpackPath, "run.bat");
            string unpackGetFilePath = Path.Combine(unpackPath, "getfile.bat");

            if (File.Exists(cnuspackerPath))
                File.Delete(cnuspackerPath);

            if (File.Exists(cdecryptPath))
                File.Delete(cdecryptPath);

            if (File.Exists(libeay32Path))
                File.Delete(libeay32Path);

            if (File.Exists(packRunPath))
                File.Delete(packRunPath);

            if (File.Exists(unpackRunPath))
                File.Delete(unpackRunPath);

            if (File.Exists(unpackGetFilePath))
                File.Delete(unpackGetFilePath);

            Directory.CreateDirectory(NUSConverterDataPath);
            Directory.CreateDirectory(packPath);
            Directory.CreateDirectory(unpackPath);

            FileStream fs = File.Create(cnuspackerPath);
            fs.Write(Resources.CNUSPacker, 0, Resources.CNUSPacker.Length);
            fs.Close();

            fs = File.Create(cdecryptPath);
            fs.Write(Resources.CDecrypt, 0, Resources.CDecrypt.Length);
            fs.Close();

            StreamWriter sw = File.CreateText(packRunPath);
            sw.WriteLine("@echo off");
            sw.WriteLine("cd \"" + packPath + "\"");
            sw.Write("CNUSPacker.exe -in %1 -out %2");
            sw.Close();

            sw = File.CreateText(unpackRunPath);
            sw.WriteLine("@echo off");
            sw.WriteLine("cd \"" + unpackPath + "\"");
            sw.Write("CDecrypt.exe %1 %2");
            sw.Close();

            sw = File.CreateText(unpackGetFilePath);
            sw.WriteLine("@echo off");
            sw.WriteLine("cd \"" + unpackPath + "\"");
            sw.Write("CDecrypt.exe %1 %2 %3");
            sw.Close();
        }
    }
}
