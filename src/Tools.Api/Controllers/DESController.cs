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
            Console.WriteLine("Դ�ַ�����" + sourceString);

            var c = DESUtil.Encrypt(sourceString, password);
            //���ܣ�3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==
            Console.WriteLine("���ܣ�" + c);

            c = HttpUtility.UrlEncode(c);
            Console.WriteLine("url���룺" + c);

            Console.WriteLine("================");

            c = HttpUtility.UrlDecode(c);
            Console.WriteLine("url���룺" + c);
            try
            {
                c = DESUtil.Decrypt(c, password);
                Console.WriteLine("url���ܣ�" + c);

                var user = JsonConvert.DeserializeObject<User>(c);
                Console.WriteLine("�����л�\n���ڣ�" + user.date);
                Console.WriteLine("���֤�ţ�" + user.idcard);
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine("�޷����ܣ�" + e.StackTrace + e.Message);
            }

            Console.WriteLine("------over--------");
            Console.ReadLine();

            return null;
        }
    }

    public record User(string idcard, DateTime date);
}