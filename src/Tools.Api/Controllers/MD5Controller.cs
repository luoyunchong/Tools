using Microsoft.AspNetCore.Mvc;

namespace Tools.Controllers
{
    /// <summary>
    /// MD5
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MD5Controller : ControllerBase
    {
        private readonly ILogger<MD5Controller> _logger;
        /// <summary>
        /// MD5 摘要
        /// </summary>
        /// <param name="logger"></param>
        public MD5Controller(ILogger<MD5Controller> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// MD5消息摘要
        /// </summary>
        /// <param name="input">md5请求内容</param>
        /// <returns></returns>
        [HttpGet(Name = "Get")]
        public string Get([FromQuery] MD5Input input)
        {
            string md5 = "";

            if (input.Base64 == true)
            {
                md5 = MD5Util.Md5ToBase64(input.SourceString, input.Digit);
                _logger.LogInformation("base64-md5:" + md5);
            }
            else
            {
                MD5Util.Md5(input.SourceString, input.Digit);
                _logger.LogInformation("       md5:" + md5);
            }

            //不是大写、是小 写
            if (input.Capital == false)
            {
                md5 = md5.ToLower();
            }
            return md5;
        }

        /// <summary>
        ///  根据源字符串，生成所有MD5摘要算法的结果，包括大小写，32/16位、Base64的结果
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        [HttpGet("all")]
        public MD5Output GetAllMd5(string sourceString)
        {
            string md5Digit32 = MD5Util.Md5(sourceString, MD5Digit.Digit32);
            string md5Digit16 = MD5Util.Md5(sourceString, MD5Digit.Digit16);
            string base64md5Digit32 = MD5Util.Md5ToBase64(sourceString, MD5Digit.Digit32);
            string base64md5Digit16 = MD5Util.Md5ToBase64(sourceString, MD5Digit.Digit16);

            return new MD5Output(md5Digit32, md5Digit16, md5Digit32.ToLower(), md5Digit16.ToLower(), base64md5Digit32, base64md5Digit16);
        }

    }


    public record MD5Output(string md5Digit32, string md5Digit16, string md5Digit32lower, string md5Digit16lower, string base64md5Digit32, string base64md5Digit16);
    /// <summary>
    /// 输入：{'idcard':'330781198509077211','date':'2021-11-11 19:04'}
    /// 32位：066ACD44ECCB4D35667367390E920AD2
    /// 16位：ECCB4D3566736739
    /// </summary>
    /// <param name="SourceString">消息串</param>
    /// <param name="Base64">是否base64编码</param>
    /// <param name="Digit">位数32/16</param>
    /// <param name="Capital">是否大写</param>
    public record MD5Input(string SourceString, bool Base64 = true, MD5Digit Digit = MD5Digit.Digit32, bool Capital = true);

}
