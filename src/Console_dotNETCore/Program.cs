using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Tools;

namespace Console_dotNETCore
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = "12345678";
            string sourceString = "{'idcard':'330781198509077211','date':'2021-11-11 19:04'}";
            Console.WriteLine("源字符串：" + sourceString);
            var c = DESUtil.Encrypt(sourceString, password);
            //加密：3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==
            Console.WriteLine("加密：" + c);

            c = HttpUtility.UrlEncode(c);
            Console.WriteLine("url编码：" + c);

            Console.WriteLine("================");

            c = HttpUtility.UrlDecode(c);
            Console.WriteLine("url解码：" + c);

            try
            {
                c = DESUtil.Decrypt(c, password);
                Console.WriteLine("url解密：" + c);

                var user = JsonConvert.DeserializeObject<User>(c);
                Console.WriteLine("反序列化\n日期：" + user.date);
                Console.WriteLine("身份证号：" + user.idcard);

            }
            catch (Exception e)
            {
                Console.WriteLine("无法解密：" + e.StackTrace + e.Message);
            }

            Console.WriteLine("------回车继续md5--------");
            Console.ReadLine();


            /*
源字符串：{'idcard':'330781198509077211','date':'2021-11-11 19:04'}
加密：3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==
url编码：3tL0BBKZyUpIfO%2bXJKL1VoeQhEWc0enGG8R%2f%2fRPBJQCiykspXEBmvabp8yrWTBv%2bQUL62K7dUL%2bvbpYV%2fPwZvw%3d%3d
================
url解码：3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==
url解密：{'idcard':'330781198509077211','date':'2021-11-11 19:04'}
反序列化
日期：2021-11-11 19:04
身份证号：330781198509077211
------over--------
             */


            string md5 = MD5Util.Md5(sourceString);
            Console.WriteLine("       md5:" + md5);

            md5 = MD5Util.Md5Hash(sourceString);
            Console.WriteLine("       md5:" + md5);

            md5 = MD5Util.Md5ToBase64(sourceString);
            Console.WriteLine("base64-md5:" + md5);
            /*
               md5:066ACD44ECCB4D35667367390E920AD2
               md5:066ACD44ECCB4D35667367390E920AD2
        base64-md5:BMRNROZLTTVMC2C5DPIK0G==
             */
            Console.ReadLine();
        }


    }

    public class User
    {
        public string idcard { get; set; }
        public string date { get; set; }
    }
}
