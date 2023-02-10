using Microsoft.AspNetCore.Mvc;
using System.Text;
using Tools.Api.Utils;

namespace Tools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CaptchaController : ControllerBase
{
    private static readonly string RandomString = "23456789abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWSYZ";
    /// <summary>
    /// 验证码
    /// </summary>
    /// <returns></returns>
    [HttpGet("img")]
    public IActionResult CaptchaImg()
    {
        string captcha = GetRandomString(4);
        var bytes = ImgHelper.GetVerifyCode(captcha);
        return File(bytes, "image/png");
    }

    private string GetRandomString(int num)
    {
        Random random = new();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < num; i++)
        {
            int number = random.Next(RandomString.Length);
            sb.Append(RandomString[number]);
        }
        return sb.ToString();
    }
}
