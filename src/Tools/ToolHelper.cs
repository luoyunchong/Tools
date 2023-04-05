using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Tools
{
    public enum DIGEST_TYPE
    {
        MD5, SHA1, SHA256
    }

    public class ToolHelper
    {
        /// <summary>
        /// 自行选择算法
        /// </summary>
        /// <param name="path"></param>
        /// <param name="hashAlgorithm"></param>
        /// <returns></returns>
        public string FileToHashBase64String(string path, HashAlgorithm hashAlgorithm)
        {
            string hash = GetFileHash(path, hashAlgorithm);
            byte[] bytes = HexStrToByte(hash);
            return Convert.ToBase64String(bytes);
        }
        public string FileToHashBase64String(string path, DIGEST_TYPE DIGEST_TYPE)
        {
            string hash;
            switch (DIGEST_TYPE)
            {
                case DIGEST_TYPE.MD5:
                    hash = GetFileMD5Hash(path);
                    break;
                case DIGEST_TYPE.SHA1:
                    hash = GetFileSAH1Hash(path);
                    break;
                case DIGEST_TYPE.SHA256:
                    hash = GetFileSHA256Hash(path);
                    break;
                default:
                    throw new Exception("NOT SUPPORT");
            }
            byte[] bytes = HexStrToByte(hash);
            return Convert.ToBase64String(bytes);
        }
        //byte[] data;
        //Encoding.ASCII.GetString(buffer,0,count);
        //Encoding.GetEncoding("GB2312").GetString(data);
        //Encoding.UTF8.GetString(data);

        /// <summary>
        /// 获取文件的SHA1 的Hash值
        /// </summary>
        /// <param name="path">文件的全路径</param>
        /// <returns></returns>
        public string GetFileSAH1Hash(string path)
        {
            HashAlgorithm hash = SHA1.Create();
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] hashByte = hash.ComputeHash(stream);
            stream.Close();
            return BitConverter.ToString(hashByte).Replace("-", "");
        }
        public string GetFileMD5Hash(string path)
        {
            HashAlgorithm hash = MD5.Create();
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] hashByte = hash.ComputeHash(stream);
            stream.Close();
            return BitConverter.ToString(hashByte).Replace("-", "");
        }
        public string GetFileSHA256Hash(string path)
        {
            HashAlgorithm hash = SHA256.Create();
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] hashByte = hash.ComputeHash(stream);
            stream.Close();
            return BitConverter.ToString(hashByte).Replace("-", "");
        }

        public string GetFileHash(string path, DIGEST_TYPE DIGEST_TYPE)
        {
            string hash;
            switch (DIGEST_TYPE)
            {
                case DIGEST_TYPE.MD5:
                    hash = GetFileMD5Hash(path);
                    break;
                case DIGEST_TYPE.SHA1:
                    hash = GetFileSAH1Hash(path);
                    break;
                case DIGEST_TYPE.SHA256:
                    hash = GetFileSHA256Hash(path);
                    break;
                default:
                    throw new Exception("NOT SUPPORT");
            }
            return hash;
        }
        public string GetFileHash(string path, HashAlgorithm hashAlgorithm)
        {
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] hashByte = hashAlgorithm.ComputeHash(stream);
            stream.Close();
            return BitConverter.ToString(hashByte).Replace("-", "");
        }
        /// <summary>
        /// 16进制 转byte[]
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public byte[] HexStrToByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }

        /// <summary>
        /// byte[] 转 16进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string ByteToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.Append(((int)b).ToString("X2"));
            }
            return sb.ToString().ToUpper();
        }


        #region Base64位加密解密
        /// <summary>
        /// 将字符串转换成base64格式,使用UTF8字符集
        /// </summary>
        /// <param name="content">加密内容</param>
        /// <returns></returns>
        public static string Base64Encode(string content)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            return Convert.ToBase64String(bytes);
        }
        /// <summary>
        /// 将base64格式，转换utf8
        /// </summary>
        /// <param name="content">解密内容</param>
        /// <returns></returns>
        public static string Base64Decode(string content)
        {
            byte[] bytes = Convert.FromBase64String(content);
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion
    }
}
