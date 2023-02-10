using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;

namespace Tools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SM3Controller : ControllerBase
{
    private readonly ILogger<SM3Controller> _logger;

    public SM3Controller(ILogger<SM3Controller> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// ¹úÃÜSM3
    /// </summary>
    /// <returns></returns>
    [HttpGet("SM3Digest")]
    public string SM3()
    {
        string key_str = "wohaicai";
        byte[] key_tmp = Encoding.Default.GetBytes(key_str);
        byte[] digest;
        SM3Digest md = new SM3Digest();
        md.BlockUpdate(key_tmp, 0, key_tmp.Length);
        digest = new byte[md.GetDigestSize()];
        md.DoFinal(digest, 0);
        string passwdDigest = new UTF8Encoding().GetString(Hex.Encode(digest));
        return passwdDigest;
    }

}