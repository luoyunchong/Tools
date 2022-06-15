using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web;

namespace Tools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DESController : ControllerBase
    {
        private readonly ILogger<DESController> _logger;

        public DESController(ILogger<DESController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// "{'idcard':'330781198509077211','date':'2021-11-11 19:04'}";
        /// </summary>
        /// <param name="inputUser"></param>
        /// <returns></returns>
        [HttpPost(Name = "Encrypt")]
        public User? Encrypt([FromBody] User inputUser)
        {
            string password = "12345678";
            //string sourceString = "{'idcard':'330781198509077211','date':'2021-11-11 19:04'}";
            string sourceString = JsonConvert.SerializeObject(inputUser);
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
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine("无法解密：" + e.StackTrace + e.Message);
            }

            Console.WriteLine("------over--------");
            Console.ReadLine();

            return null;
        }
    }

    public record User(string idcard, DateTime date);
}