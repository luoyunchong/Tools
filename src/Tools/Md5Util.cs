using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace Tools
{
    public class MD5Util
    {

        /// <summary>
        /// 结果和FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5")的实现相同
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Md5(string s)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                string result = BitConverter.ToString(md5.ComputeHash(bytes));
                return result.Replace("-", "");
            }
        }

        public static string Md5Hash(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();//实例化一个md5对像
                                   // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X2");
            }
            return pwd;
        }

        public static string Md5ToBase64(string s)
        {
            using (MD5 md = new MD5CryptoServiceProvider())
            {
                byte[] ss = md.ComputeHash(Encoding.UTF8.GetBytes(s));
                return Convert.ToBase64String(ss.ToArray()).ToUpper();
            }
        }
    }
}
