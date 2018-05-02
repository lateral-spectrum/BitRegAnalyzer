using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BitRegAnalyzer
{
    public static class ByteStringConverter
    {
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static byte[] StringToByteArray(String hex)
        {            
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static byte[] ConvertFromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        public static string HexStringFromByteArr(byte[] bytes)
        {
            string hex_string = BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
            return hex_string;
        }

        private static string HashFile(byte[] md5)
        {
            MD5 a = MD5.Create();
            byte[] hash = a.ComputeHash(File.OpenRead(@"C:\Users\TTDDWW\Desktop\putty.exe"));
            string hexString = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();            
            return hexString;            
        }
    }
}
