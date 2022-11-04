using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web;

namespace Tools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        _logger.LogInformation("源字符串：" + sourceString);

        var c = DESUtil.Encrypt(sourceString, password);
        //加密：3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==
        _logger.LogInformation("加密：" + c);

        c = HttpUtility.UrlEncode(c);
        _logger.LogInformation("url编码：" + c);

        _logger.LogInformation("================");

        c = HttpUtility.UrlDecode(c);
        _logger.LogInformation("url解码：" + c);
        try
        {
            c = DESUtil.Decrypt(c, password);
            _logger.LogInformation("url解密：" + c);

            var user = JsonConvert.DeserializeObject<User>(c);
            _logger.LogInformation("反序列化\n日期：" + user.date);
            _logger.LogInformation("身份证号：" + user.idcard);
            return user;
        }
        catch (Exception e)
        {
            _logger.LogInformation("无法解密：" + e.StackTrace + e.Message);
        }

        _logger.LogInformation("------over--------");

        return null;
    }
}

public record User(string idcard, DateTime date);