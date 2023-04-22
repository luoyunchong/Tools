using Microsoft.AspNetCore.Mvc;

namespace Tools.Api.Controllers;


/// <summary>
/// AES加解密
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AESController : ControllerBase
{
    private readonly ILogger<AESController> _logger;

    public AESController(ILogger<AESController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// AES 加密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("encrypt")]
    public string Encrypt([FromBody] AESInput input)
    {
        var res = AESUtil.Encrypt(input);
        return res;
    }

    /// <summary>
    /// AES 解密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("decrypt")]
    public string Decrypt([FromBody] AESInput input)
    {
        var res = AESUtil.Decrypt(input);
        return res;
    }
}

