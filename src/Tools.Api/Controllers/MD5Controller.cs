using Microsoft.AspNetCore.Mvc;
using Tools.Api.DTO;

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
    ///  根据源字符串，生成所有MD5摘要算法的结果，包括大小写，32/16位、Base64的结果
    /// </summary>
    /// <param name="sourceString"></param>
    /// <returns></returns>
    [HttpGet("all")]
    public MD5Output GetAllMd5(string sourceString)
    {
        string md5Digit32 = MD5Util.Md5(sourceString, MD5Digit.Digit32);
        string md5Digit16 = MD5Util.Md5(sourceString, MD5Digit.Digit16);
        string base64Md5Digit32 = MD5Util.Md5ToBase64(sourceString, MD5Digit.Digit32);
        string base64Md5Digit16 = MD5Util.Md5ToBase64(sourceString, MD5Digit.Digit16);

        return new MD5Output(md5Digit32, md5Digit16, md5Digit32.ToLower(), md5Digit16.ToLower(), base64Md5Digit32, base64Md5Digit16);
    }

}
