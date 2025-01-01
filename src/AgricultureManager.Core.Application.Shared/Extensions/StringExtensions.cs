using System.Security.Cryptography;
using System.Text;

namespace AgricultureManager.Core.Application.Shared.Extensions
{
    /// <summary>
    /// Verschlüsselt und Entschlüsselt Texte
    /// AES Verschlüsselung
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Verschlüsselt einen Text
        /// </summary>
        /// <param name="plainText">Text zum verschlüsseln</param>
        /// <returns>Verschlüsselter Text</returns>
        public static string? Encrypt(this string? plainText)
        {
            if (plainText == null)
                return null;

            byte[] encrypted;

            using (Aes aes = Aes.Create())
            {
                aes.Key = DoExtendKey("AgricultureManager", 32);
                aes.IV = DoCreateBlocksize(16);

                using var memoryStream = new MemoryStream();
                using var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();
                encrypted = memoryStream.ToArray();
            }
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Entschlüsselt einen verschlüsselten Text
        /// </summary>
        /// <param name="cipherText">Verschlüsselter Text</param>
        /// <returns>Entschlüsselter Text</returns>
        public static string? Decrypt(this string? cipherText)
        {
            if (cipherText == null)
                return null;

            string? plaintext = null;

            using (Aes aes = Aes.Create())
            {
                aes.Key = DoExtendKey("AgricultureManager", 32);
                aes.IV = DoCreateBlocksize(16);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] encryptedBytes = Convert.FromBase64String(cipherText);

                using var memoryStream = new MemoryStream(encryptedBytes);
                using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using var sr = new StreamReader(cryptoStream);
                plaintext = sr.ReadToEnd();
            }
            return plaintext;
        }

        private static byte[] DoExtendKey(string key, int newKeyLength)
        {
            byte[] bKey = new byte[newKeyLength];
            byte[] tmpKey = Encoding.UTF8.GetBytes(key);
            for (int i = 0; i < key.Length; i++)
            { bKey[i] = tmpKey[i]; }
            return bKey;
        }

        private static byte[] DoCreateBlocksize(int newBlockSize)
        {
            byte[] block = new byte[newBlockSize];
            for (byte i = 0; i < newBlockSize; i++)
            { block[i] = i; }
            return block;
        }
    }
}
