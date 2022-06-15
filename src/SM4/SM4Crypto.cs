using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace DSSM
{
    // <summary>
    /// Sm4算法  
    /// 对标国际DES算法
    /// </summary>
    public class SM4Crypto
    {
        public SM4Crypto()
        {
            Key = "98145489617106616498";
            Iv = "0000000000000000";
            HexString = false;
            CryptoMode = SM4CryptoEnum.ECB;
        }
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 向量
        /// </summary>
        public string Iv { get; set; }
        /// <summary>
        /// 明文是否是十六进制
        /// </summary>
        public bool HexString { get; set; }
        /// <summary>
        /// 加密模式(默认ECB)
        /// </summary>
        public SM4CryptoEnum CryptoMode { get; set; }

        #region 加密
        public string Encrypt(SM4Crypto entity)
        {
            return entity.CryptoMode == SM4CryptoEnum.CBC ? EncryptCBC(entity) : EncryptECB(entity);
        }
 

        /// <summary>
        /// ECB加密
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string EncryptECB(SM4Crypto entity)
        {
            SM4Context ctx = new SM4Context
            {
                IsPadding = true
            };
            byte[] keyBytes = entity.HexString ? Hex.Decode(entity.Key) : Encoding.Default.GetBytes(entity.Key);

            SM4 sm4 = new SM4();
            sm4.SetKeyEnc(ctx, keyBytes);
            byte[] encrypted = sm4.Sm4CryptEcb(ctx, Encoding.Default.GetBytes(entity.Data));
           
            return Encoding.Default.GetString(Hex.Encode(encrypted));
        }

        public static string EncryptECBToBase64(SM4Crypto entity)
        {
            SM4Context ctx = new SM4Context
            {
                IsPadding = true
            };

            byte[] keyBytes = entity.HexString ? Hex.Decode(entity.Key) : Encoding.Default.GetBytes(entity.Key);

            SM4 sm4 = new SM4();
            sm4.SetKeyEnc(ctx, keyBytes);
            byte[] encrypted = sm4.Sm4CryptEcb(ctx, Encoding.Default.GetBytes(entity.Data));

            return Convert.ToBase64String(encrypted); ;
        }
        /// <summary>
        /// CBC加密
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string EncryptCBC(SM4Crypto entity)
        {
            SM4Context ctx = new SM4Context
            {
                IsPadding = true
            };

            byte[] keyBytes = entity.HexString ? Hex.Decode(entity.Key) : Encoding.Default.GetBytes(entity.Key);
            byte[] ivBytes = entity.HexString ? Hex.Decode(entity.Iv) : Encoding.Default.GetBytes(entity.Iv);

            SM4 sm4 = new SM4();
            sm4.SetKeyEnc(ctx, keyBytes);
            byte[] encrypted = sm4.Sm4CryptCbc(ctx, ivBytes, Encoding.Default.GetBytes(entity.Data));

            return Convert.ToBase64String(encrypted);
        }
        #endregion


        #region 解密
        public object Decrypt(SM4Crypto entity)
        {
            return entity.CryptoMode == SM4CryptoEnum.CBC ? DecryptCBC(entity) : DecryptECB(entity);
        }
        /// <summary>
        ///  ECB解密
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string DecryptECB(SM4Crypto entity)
        {
            SM4Context ctx = new SM4Context
            {
                IsPadding = true,
                Mode = 0
            };

            byte[] keyBytes = entity.HexString ? Hex.Decode(entity.Key) : Encoding.Default.GetBytes(entity.Key);

            SM4 sm4 = new SM4();
            sm4.Sm4SetKeyDec(ctx, keyBytes);
            byte[] decrypted = sm4.Sm4CryptEcb(ctx, Hex.Decode(entity.Data));
            return Encoding.Default.GetString(decrypted);
        }

        public static string DecryptECBFromBase64(SM4Crypto entity)
        {
            SM4Context ctx = new SM4Context
            {
                IsPadding = true,
                Mode = 0
            };

            byte[] keyBytes = entity.HexString ? Hex.Decode(entity.Key) : Encoding.Default.GetBytes(entity.Key);

            SM4 sm4 = new SM4();
            sm4.Sm4SetKeyDec(ctx, keyBytes);
            byte[] decrypted = sm4.Sm4CryptEcb(ctx, Convert.FromBase64String(entity.Data));
            return Encoding.Default.GetString(decrypted);
        }
        /// <summary>
        /// CBC解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string DecryptCBC(SM4Crypto entity)
        {
            SM4Context ctx = new SM4Context
            {
                IsPadding = true,
                Mode = 0
            };

            byte[] keyBytes = entity.HexString ? Hex.Decode(entity.Key) : Encoding.Default.GetBytes(entity.Key);
            byte[] ivBytes = entity.HexString ? Hex.Decode(entity.Iv) : Encoding.Default.GetBytes(entity.Iv);

            SM4 sm4 = new SM4();
            sm4.Sm4SetKeyDec(ctx, keyBytes);
            byte[] decrypted = sm4.Sm4CryptCbc(ctx, ivBytes, Convert.FromBase64String(entity.Data));
            return Encoding.Default.GetString(decrypted);
        }
        #endregion

        /// <summary>
        /// 加密类型
        /// </summary>
        public enum SM4CryptoEnum
        {
            /// <summary>
            /// ECB(电码本模式)
            /// </summary>
            [Description("ECB模式")]
            ECB = 0,
            /// <summary>
            /// CBC(密码分组链接模式)
            /// </summary>
            [Description("CBC模式")]
            CBC = 1
        }
    }
}
