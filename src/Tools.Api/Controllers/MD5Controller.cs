using Microsoft.AspNetCore.Mvc;

namespace Tools.Controllers
{
    /// <summary>
    /// MD5
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MD5Controller : ControllerBase
    {
        /// <summary>
        /// MD5消息摘要
        /// </summary>
        /// <param name="sourceString">消息串{'idcard':'330781198509077211','date':'2021-11-11 19:04'}</param>
        /// <param name="isbase64">是否base64编码</param>
        /// <returns></returns>
        [HttpGet(Name = "Get")]
        public string Get(string sourceString,bool isbase64=false)
        {
             //sourceString = "{'idcard':'330781198509077211','date':'2021-11-11 19:04'}";
            string md5 = MD5Util.Md5(sourceString);
            Console.WriteLine("       md5:" + md5);

            md5 = MD5Util.Md5Hash(sourceString);
            Console.WriteLine("       md5:" + md5);

            if (isbase64 == true)
            {
                md5 = MD5Util.Md5ToBase64(sourceString);
                Console.WriteLine("base64-md5:" + md5);
            }
            /*
               md5:066ACD44ECCB4D35667367390E920AD2
               md5:066ACD44ECCB4D35667367390E920AD2
        base64-md5:BMRNROZLTTVMC2C5DPIK0G==
             */
            Console.ReadLine();

            return md5;
        }

    }
}
