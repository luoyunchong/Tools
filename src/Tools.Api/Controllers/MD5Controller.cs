using Microsoft.AspNetCore.Mvc;

namespace Tools.Api.Controllers;

/// <summary>
/// MD5 摘要算法
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
    [HttpGet]
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
    public Md5Output GetAllMd5(string sourceString)
    {
        string md5Digit32 = MD5Util.Md5(sourceString, MD5Digit.Digit32);
        string md5Digit16 = MD5Util.Md5(sourceString, MD5Digit.Digit16);
        string base64Md5Digit32 = MD5Util.Md5ToBase64(sourceString, MD5Digit.Digit32);
        string base64Md5Digit16 = MD5Util.Md5ToBase64(sourceString, MD5Digit.Digit16);

        return new Md5Output(md5Digit32, md5Digit16, md5Digit32.ToLower(), md5Digit16.ToLower(), base64Md5Digit32, base64Md5Digit16);
    }

}


/// <summary>
/// 得到MD5的多种结构
/// </summary>
/// <param name="Md5Digit32">32位加密</param>
/// <param name="Md5Digit16">16位加密 </param>
/// <param name="Md5Digit32Lower">32位小写</param>
/// <param name="Md5Digit16Lower">16位小写</param>
/// <param name="Base64Md5Digit32">base64编码32位加密</param>
/// <param name="Base64Md5Digit16">base64编码16位加密</param>
public record Md5Output(string Md5Digit32, string Md5Digit16, string Md5Digit32Lower, string Md5Digit16Lower, string Base64Md5Digit32, string Base64Md5Digit16);

/// <summary>
/// 输入：{'idcard':'330781198509077211','date':'2021-11-11 19:04'} <br></br>
/// 32位：066ACD44ECCB4D35667367390E920AD2<br></br>
/// 16位：ECCB4D3566736739<br></br>
/// </summary>
/// <param name="SourceString">消息串</param>
/// <param name="Base64">是否base64编码</param>
/// <param name="Digit">位数32/16</param>
/// <param name="Capital">是否大写</param>
public record MD5Input(string SourceString, bool Base64 = true, MD5Digit Digit = MD5Digit.Digit32, bool Capital = true);
