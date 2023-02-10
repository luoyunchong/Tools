using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;
using Tools.Api.Utils;

namespace Tools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SM2Controller : ControllerBase
{
    private readonly ILogger<SM2Controller> _logger;

    public SM2Controller(ILogger<SM2Controller> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 签名，国密SM2
    /// </summary>
    /// <param name="body">参数内容</param>
    /// <param name="privateKey">私钥</param>
    /// <param name="sign">签名值</param>
    /// <param name="timestamp">时间戳</param>
    private string Sign(string body, string privateKey, out string timestamp)
    {
        if (body.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(body));
        if (privateKey.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(privateKey));

        timestamp = "";

        // 加密算法采用SM2加密算法
        string sign = Hex.ToHexString(SMCrypto.Sign(Encoding.UTF8.GetBytes(body), SMCrypto.Decode(privateKey), Encoding.UTF8.GetBytes(timestamp)));
        return sign;
    }
    /// <summary>
    /// 验签，国密SM2
    /// </summary>
    /// <param name="body">参数内容</param>
    /// <param name="publicKey">公约</param>
    /// <param name="sign">签名值</param>
    /// <param name="timestamp">时间戳</param>
    /// <returns>成功与否</returns>
    private bool VerifySign(string body, string publicKey, string sign, string timestamp)
    {
        if (body.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(body));
        if (publicKey.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(publicKey));
        if (sign.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(sign));
        if (timestamp.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(timestamp));

        return SMCrypto.VerifySign(Encoding.UTF8.GetBytes(body), SMCrypto.Decode(publicKey), SMCrypto.Decode(sign),
            Encoding.UTF8.GetBytes(timestamp));
    }
    /// <summary>
    /// 国密SM2
    /// </summary>
    /// <returns></returns>
    [HttpGet("SM2Digest")]
    public string SM2()
    {
        var t = "";
        return Sign("12345", "", out t);
    }

}
