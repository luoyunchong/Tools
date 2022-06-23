using Org.BouncyCastle.Utilities;
using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace Tools
{
    public enum MD5Digit
    {
        /// <summary>
        /// 16位MD5
        /// </summary>
        Digit16 = 16,
        /// <summary>
        /// 32位MD5
        /// </summary>
        Digit32 = 32
    }
    public class MD5Util
    {

        /// <summary>
        /// 结果和FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5")的实现相同
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Md5(string soureString, MD5Digit mD5Digit = MD5Digit.Digit32)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(soureString);
                string result = "";
                //16位MD5：取9到25位 
                if (mD5Digit == MD5Digit.Digit32)
                {
                    result = BitConverter.ToString(md5.ComputeHash(bytes));
                }
                else
                {
                    result = BitConverter.ToString(md5.ComputeHash(bytes), 4, 8);
                }
                return result.Replace("-", "");
            }
        }

        public static string Md5Hash(string soureString, MD5Digit mD5Digit = MD5Digit.Digit32)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
                                   // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(soureString));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X2");
            }
            if (mD5Digit == MD5Digit.Digit16)
            {
                pwd = pwd.Substring(8, 16);
            }
            return pwd;
        }
       
        public static string Md5ToBase64(string s, MD5Digit mD5Digit = MD5Digit.Digit32)
        {
            using (MD5 md = new MD5CryptoServiceProvider())
            {
                byte[] ss = md.ComputeHash(Encoding.UTF8.GetBytes(s));
                if (mD5Digit == MD5Digit.Digit32)
                {
                    return Convert.ToBase64String(ss.ToArray()).ToUpper();
                }
                else
                {
                    return Convert.ToBase64String(ss.ToArray(), 4, 8, Base64FormattingOptions.None).ToUpper();
                }
            }
        }
    }
}
