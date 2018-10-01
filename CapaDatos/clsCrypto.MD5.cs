using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CapaDatos.clsCrypto
{
    public class MD5
	{
		public static string MD5HashFile(string FilePath)
		{
			// Abre el Archivo (ReadOnly)
			using (FileStream reader = new FileStream(FilePath, FileMode.Open, FileAccess.Read)) {
                string Result = MD5HashFile(reader);
				reader.Close();

                return Result;
			}
		}

		public static string MD5HashFile(Stream FileStream)
		{
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider()) {
				// Aqui se Guarda el Hach del Archivo
				byte[] aHash = md5.ComputeHash(FileStream);

				//FileStream.Close()
				// Se Convierte en HEX de 2 Digitos
				return ByteArrayToString(aHash);
			}
		}

		public static bool MD5ValidFile(string FilePath, string Hash)
		{
			return (string.Compare(Hash, MD5HashFile(FilePath), true) == 0);
		}

		public static bool MD5ValidFile(Stream FileStream, string Hash)
		{
			return (string.Compare(Hash, MD5HashFile(FileStream), true) == 0);
		}

		public static string MD5Hash(string strText)
		{
			using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider()) {
				byte[] aHash = md5.ComputeHash(Encoding.Default.GetBytes(strText));

				// Se Convierte en HEX de 2 Digitos
				return ByteArrayToString(aHash);
			}
		}

		public static bool MD5ValidHash(string strText, string Hash)
		{
			return (string.Compare(Hash, MD5Hash(strText), true) == 0);
		}

		// Convierte un Arregla Byte en un String de HEX a 2 Digitos
		private static string ByteArrayToString(byte[] arrInput)
		{
            //Encoding.UTF8.GetString(arrInput, 0, arrInput.Length);

            StringBuilder sb = new System.Text.StringBuilder(arrInput.Length * 2);

			for (int i = 0; i <= arrInput.Length - 1; i++) {
				sb.Append(arrInput[i].ToString("X2"));
			}

			return sb.ToString().ToUpper();
		}
	}
}

