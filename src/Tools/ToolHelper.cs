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
            byte[] bytes = HexStrTobyte(hash);
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
            byte[] bytes = HexStrTobyte(hash);
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
        public byte[] HexStrTobyte(string hexString)
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
        public string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in data)
            {
                sb.Append(((int)b).ToString("X2"));
            }
            return sb.ToString().ToUpper();
        }
    }
}
