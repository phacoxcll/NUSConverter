using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Drawing;

namespace NUSConverter
{
    public static class NUSContent
    {
        public const uint CommonKeyCRC32 = 0xB92B703D;

        public enum Format
        {
            Encrypted,
            Decrypted,
            Indeterminate
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

        public static void Encrypt(string inputPath, string outputPath)
        {
            string packPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NUSConverter"), "pack");

            Check(inputPath);
            if (!File.Exists(Path.Combine(packPath, "CNUSPacker.exe")))
                throw new Exception("The \"" + Path.Combine(packPath, "CNUSPacker.exe") + "\" file not exist.");

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
            if (!File.Exists(Path.Combine(unpackPath, "CDecrypt.exe")))
                throw new Exception("The \"" + Path.Combine(unpackPath, "CDecrypt.exe") + "\" file not exist.");

            Process decrypt = Process.Start(Path.Combine(unpackPath, "run.bat"), "\"" + inputPath + "\" \"" + outputPath + "\"");
            decrypt.WaitForExit();

            if (decrypt.ExitCode == 0)
            {
                decrypt.Dispose();
            }
            else
            {
                decrypt.Dispose();
                throw new Exception("Decrypt fail.");
            }
        }

        public static void Decrypt(string inputPath, string filename, string outputFilename)
        {
            string unpackPath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NUSConverter"), "unpack");

            CheckEncrypted(inputPath);
            if (!File.Exists(Path.Combine(unpackPath, "CDecrypt.exe")))
                throw new Exception("The \"" + Path.Combine(unpackPath, "CDecrypt.exe") + "\" file not exist.");

            Process decrypt = Process.Start(Path.Combine(unpackPath, "getfile.bat"), "\"" + inputPath + "\" \"" + filename + "\" \"" + outputFilename + "\"");
            decrypt.WaitForExit();

            if (decrypt.ExitCode == 0)
            {
                decrypt.Dispose();
            }
            else
            {
                decrypt.Dispose();
                throw new Exception("Decrypt file fail.");
            }
        }

        public struct Meta
        {
            public string ShortName;
            public string LongName;
            public string TitleID;
            public string ProductCode;
        }

        public static Meta GetMeta(string path)
        {
            if (File.Exists(Path.Combine(path, "meta", "meta.xml")))
            {
                Meta m = new Meta();
                XmlDocument xmlMeta = new XmlDocument();
                xmlMeta.Load(Path.Combine(path, "meta", "meta.xml"));
                XmlNode shortname_en = xmlMeta.SelectSingleNode("menu/shortname_en");
                XmlNode longname_en = xmlMeta.SelectSingleNode("menu/longname_en");
                XmlNode title_id = xmlMeta.SelectSingleNode("menu/title_id");
                XmlNode product_code = xmlMeta.SelectSingleNode("menu/product_code");
                m.ShortName = shortname_en.InnerText;
                m.LongName = longname_en.InnerText;
                m.TitleID = title_id.InnerText;
                m.ProductCode = product_code.InnerText;

                return m;
            }
            else
                throw new Exception("The \"" + Path.Combine(path, "meta", "meta.xml") + "\" file not exist.");
        }

        public static Bitmap TGAToBitmap(string filename)
        {
            FileStream fs = File.Open(filename, FileMode.Open);
            byte[] header = new byte[0x12];
            fs.Read(header, 0, 0x12);
            fs.Close();

            if (header[0x00] != 0 ||
                header[0x01] != 0 ||
                header[0x02] != 0x02 ||
                header[0x03] != 0 ||
                header[0x04] != 0 ||
                header[0x05] != 0 ||
                header[0x06] != 0 ||
                header[0x07] != 0 ||
                header[0x08] != 0 ||
                header[0x09] != 0 ||
                header[0x0A] != 0 ||
                header[0x0B] != 0)
                throw new FormatException("Unsupported TGA type.");

            int bytesPerPixel;
            System.Drawing.Imaging.PixelFormat pixelFormat;
            if (header[0x10] == 0x20 && header[0x11] == 0x08)
            {
                bytesPerPixel = 4;
                pixelFormat = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
            }
            else if (header[0x10] == 0x18 && header[0x11] == 0)
            {
                bytesPerPixel = 3;
                pixelFormat = System.Drawing.Imaging.PixelFormat.Format24bppRgb;
            }
            else
                throw new FormatException("Unsupported TGA format.");

            int width = (header[0x0D] << 8) + header[0x0C];
            int height = (header[0x0F] << 8) + header[0x0E];

            fs = File.Open(filename, FileMode.Open);
            byte[] tgaBytes = new byte[width * height * bytesPerPixel];
            fs.Position = 0x12;
            fs.Read(tgaBytes, 0, tgaBytes.Length);
            fs.Close();

            Bitmap bmp = new Bitmap(width, height, pixelFormat);

            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData data =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly, bmp.PixelFormat);

            for (int y = 0; y < data.Height; y++)
            {
                IntPtr ptr = (IntPtr)((long)data.Scan0 + y * data.Stride);
                System.Runtime.InteropServices.Marshal.Copy(
                     tgaBytes, (data.Height - 1 - y) * data.Width * bytesPerPixel, ptr, data.Width * bytesPerPixel);
            }
            bmp.UnlockBits(data);

            return bmp;
        }
    }
}
