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
    /// ǩ��������SM2
    /// </summary>
    /// <param name="body">��������</param>
    /// <param name="privateKey">˽Կ</param>
    /// <param name="sign">ǩ��ֵ</param>
    /// <param name="timestamp">ʱ���</param>
    private string Sign(string body, string privateKey, out string timestamp)
    {
        if (body.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(body));
        if (privateKey.IsNullOrWhiteSpace()) throw new ArgumentNullException(nameof(privateKey));

        timestamp = "";

        // �����㷨����SM2�����㷨
        string sign = Hex.ToHexString(SMCrypto.Sign(Encoding.UTF8.GetBytes(body), SMCrypto.Decode(privateKey), Encoding.UTF8.GetBytes(timestamp)));
        return sign;
    }
    /// <summary>
    /// ��ǩ������SM2
    /// </summary>
    /// <param name="body">��������</param>
    /// <param name="publicKey">��Լ</param>
    /// <param name="sign">ǩ��ֵ</param>
    /// <param name="timestamp">ʱ���</param>
    /// <returns>�ɹ����</returns>
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
    /// ����SM2
    /// </summary>
    /// <returns></returns>
    [HttpGet("SM2Digest")]
    public string SM2()
    {
        var t = "";
        return Sign("12345", "", out t);
    }

}
