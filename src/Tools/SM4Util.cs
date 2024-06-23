using System.Runtime.CompilerServices;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Utilities.Encoders;
using System;

namespace Tools
{
    public enum Base64OrHexEnum
    {
        Base64,
        Hex
    }

    public class SM4Util
    {
        public static string ALGORITHM_NAME = "SM4";
        public static string ALGORITHM_NAME_ECB_PADDING = "SM4/ECB/PKCS7Padding";
        public static string ALGORITHM_NAME_ECB_NOPADDING = "SM4/ECB/NoPadding";
        public static string ALGORITHM_NAME_CBC_PADDING = "SM4/CBC/PKCS7Padding";
        public static string ALGORITHM_NAME_CBC_NOPADDING = "SM4/CBC/NoPadding";

        public static int DEFAULT_KEY_SIZE = 16;


        public static byte[] GenerateKey()
        {
            return GenerateKey(DEFAULT_KEY_SIZE);
        }
        public static byte[] GenerateKey(int keySize)
        {
            var rand = RandomNumberGenerator.Create();
            byte[] bytes = new byte[keySize];
            rand.GetBytes(bytes);
            return bytes;
        }

        #region CBC
        public static byte[] Encrypt_CBC_Padding(byte[] key, byte[] iv, byte[] data)
        {
            IBufferedCipher cipher = GenerateCBCCipher(ALGORITHM_NAME_CBC_PADDING, true, key, iv);
            return cipher.DoFinal(data);
        }

        public static byte[] Decrypt_CBC_Padding(byte[] key, byte[] iv, byte[] cipherText)
        {
            IBufferedCipher cipher = GenerateCBCCipher(ALGORITHM_NAME_CBC_PADDING, false, key, iv);
            return cipher.DoFinal(cipherText);
        }

        public static byte[] Encrypt_CBC_NoPadding(byte[] key, byte[] iv, byte[] data)
        {
            IBufferedCipher cipher = GenerateCBCCipher(ALGORITHM_NAME_CBC_NOPADDING, true, key, iv);
            return cipher.DoFinal(data);
        }

        public static byte[] Decrypt_CBC_NoPadding(byte[] key, byte[] iv, byte[] cipherText)
        {
            IBufferedCipher cipher = GenerateCBCCipher(ALGORITHM_NAME_CBC_NOPADDING, false, key, iv);
            return cipher.DoFinal(cipherText);
        }

        private static IBufferedCipher GenerateCBCCipher(string algorithmName, bool forEncryption, byte[] keyBytes, byte[] iv)
        {
            IBufferedCipher inCipher = CipherUtilities.GetCipher(algorithmName);
            KeyParameter key = ParameterUtilities.CreateKeyParameter(ALGORITHM_NAME, keyBytes);
            ParametersWithIV keyParamWithIv = new ParametersWithIV(key, iv);
            inCipher.Init(forEncryption, keyParamWithIv);
            return inCipher;
        }
        #endregion

        #region ECB
        public static byte[] Encrypt_ECB_Padding(string key, string data)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] plaintext = Encoding.UTF8.GetBytes(data);
            return Encrypt_ECB_Padding(keyBytes, plaintext);
        }
        public static byte[] Encrypt_ECB_Padding(byte[] key, byte[] data)
        {
            IBufferedCipher cipher = GenerateECBCipher(ALGORITHM_NAME_ECB_PADDING, true, key);
            return cipher.DoFinal(data);
        }

        public static byte[] Decrypt_ECB_Padding(string key, string cipherText, Base64OrHexEnum type = Base64OrHexEnum.Hex)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] plaintext = type == Base64OrHexEnum.Hex ? Hex.Decode(cipherText) : Convert.FromBase64String(cipherText);
            return Decrypt_ECB_Padding(keyBytes, plaintext);
        }

        public static byte[] Decrypt_ECB_Padding(byte[] key, byte[] cipherText)
        {
            IBufferedCipher cipher = GenerateECBCipher(ALGORITHM_NAME_ECB_PADDING, false, key);
            return cipher.DoFinal(cipherText);
        }

        public static byte[] Encrypt_ECB_NoPadding(byte[] key, byte[] data)
        {
            IBufferedCipher cipher = GenerateECBCipher(ALGORITHM_NAME_ECB_NOPADDING, true, key);
            return cipher.DoFinal(data);
        }

        public static byte[] Decrypt_ECB_NoPadding(byte[] key, byte[] cipherText)
        {
            IBufferedCipher cipher = GenerateECBCipher(ALGORITHM_NAME_ECB_NOPADDING, false, key);
            return cipher.DoFinal(cipherText);
        }

        private static IBufferedCipher GenerateECBCipher(string algorithmName, bool forEncryption, byte[] keyBytes)
        {
            IBufferedCipher inCipher = CipherUtilities.GetCipher(algorithmName);
            KeyParameter key = ParameterUtilities.CreateKeyParameter(ALGORITHM_NAME, keyBytes);
            inCipher.Init(forEncryption, key);
            return inCipher;
        }
        #endregion

    }
}
