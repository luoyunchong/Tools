using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tools
{
    /// <summary>
    /// 对称加密算法
    /// </summary>
    public class DESUtil
    {
        private static string defaultIvKey = "12345678";

        #region DES 加密

        public static string Encrypt(DESInput input)
        {
            return DESUtil.Encrypt(input.SourceString, input.Key, input.Iv, input.PaddingMode, input.CipherMode);
        }
        /// <summary>
        /// 加密 密钥和向量必须为8位
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="Key">密钥</param>
        /// <param name="Iv">初始化向量</param>
        /// <returns></returns>
        public static string Encrypt(string sourceString, string Key, string Iv = "", PaddingMode padding = PaddingMode.PKCS7, CipherMode mode = CipherMode.CBC)
        {
            if (string.IsNullOrWhiteSpace(Iv))
            {
                Iv = defaultIvKey;
            }
            if (Key.Length < 8)
            {
                throw new ArgumentException("密钥必须是8位字符");
            }
            //大于8，自动截断
            Key = Key.Substring(0, 8);
            byte[] btKey = Encoding.UTF8.GetBytes(Key);
            byte[] btIv = Encoding.UTF8.GetBytes(Iv);
            using (var des = new DESCryptoServiceProvider())
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] inData = Encoding.UTF8.GetBytes(sourceString);

                    des.Mode = mode;//这里指定加密模式为CBC
                    des.Padding = padding;
                    var trans = des.CreateEncryptor(btKey, btIv);
                    using (CryptoStream cs = new CryptoStream(ms, trans, CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        #endregion

        #region DES 解密

        public static string Decrypt(DESInput input)
        {
            return DESUtil.Decrypt(input.SourceString, input.Key, input.Iv, input.PaddingMode, input.CipherMode);
        }

        /// <summary>
        /// 解密:密钥和向量必须为8位
        /// </summary>
        /// <param name="encryptedString"></param>
        /// <param name="keyString"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptedString, string Key, string Iv = "", PaddingMode padding = PaddingMode.PKCS7, CipherMode mode = CipherMode.CBC)
        {
            if (string.IsNullOrWhiteSpace(Iv))
            {
                Iv = defaultIvKey;
            }
            if (Key.Length < 8)
            {
                throw new ArgumentException("密钥必须是8位字符");
            }
            Key = Key.Substring(0, 8);
            byte[] btKey = Encoding.UTF8.GetBytes(Key);
            byte[] btIv = Encoding.UTF8.GetBytes(Iv);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Mode = mode;//这里指定加密模式为CBC
                des.Padding = padding;

                des.Key = btKey;
                des.IV = btIv;

                using (var ms = new MemoryStream())
                {
                    byte[] inData = Convert.FromBase64String(encryptedString);
                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        } 
        #endregion
    }

    public class DESInput : AESInput
    {
    }
}
