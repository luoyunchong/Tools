using DSSM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;
using Tools.Api.DTO;

namespace Tools.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SM4Controller : ControllerBase
{
    private readonly ILogger<SM4Controller> _logger;

    public SM4Controller(ILogger<SM4Controller> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///加密后的密文(16进制hex): 11D9B2E155AE15A9525455BA0A7CEED0 
    ///加密后的密文(hex): 11D9B2E155AE15A9525455BA0A7CEED0 
    ///加密后的密文(base64): Edmy4VWuFalSVFW6Cnzu0A== 
    ///解密: tuserid002
    ///密钥 sW93ZE8rjDeD3!1m
    /// </summary>
    /// <param name="input">要加密的文本：tuserid002</param>
    [HttpPost("ECB_Padding")]
    public IActionResult ECB_Padding([FromBody] SM4ECBInput input)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(input.Keys);

        if (input.EnOrDecrpyt)
        {
            byte[] plaintext = Encoding.UTF8.GetBytes(input.Data);

            byte[] cipher = SM4Util.Encrypt_ECB_Padding(keyBytes, plaintext);
            _logger.LogInformation("加密后的密文(hex): {0}", Hex.ToHexString(cipher).ToUpper());
            _logger.LogInformation("加密后的密文(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
            _logger.LogInformation("加密后的密文(base64): {0}", Convert.ToBase64String(cipher));
            return Ok(new
            {
                sm4Hex = Hex.ToHexString(cipher).ToUpper(),
                sm4Base64 = Convert.ToBase64String(cipher),
                text = input.Data
            });
        }
        else
        {
            byte[] cipher = input.Type == Base64OrHexEnum.Hex ? Hex.Decode(input.Data) : Convert.FromBase64String(input.Data);
            byte[] decryptedData = SM4Util.Decrypt_ECB_Padding(keyBytes, cipher);

            _logger.LogInformation("解密: {0}", Encoding.UTF8.GetString(decryptedData));

            return Ok(new
            {
                sm4Hex = "",
                sm4Base64 = "",
                text = Encoding.UTF8.GetString(decryptedData),
            });
        }
    }

    /// <summary>
    /// 0123456789abcdeffedcba9876543210
    /// </summary>
    /// <param name="data"></param>
    [HttpGet("ECB_NoPadding")]
    public IActionResult ECB_NoPadding(string data)
    {
        byte[] plaintext = Hex.Decode(data);// 需满足 (pSourceLen % 16 == 0)
        byte[] keyBytes = Hex.Decode("0123456789abcdeffedcba9876543210");//长度： 16B

        byte[] cipher = SM4Util.Encrypt_ECB_NoPadding(keyBytes, plaintext);
        _logger.LogInformation("加密后的密文(hex): {0}", Hex.ToHexString(cipher).ToUpper());
        _logger.LogInformation("加密后的密文(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
        _logger.LogInformation("加密后的密文(base64): {0}", Convert.ToBase64String(cipher));

        byte[] decryptedData = SM4Util.Decrypt_ECB_NoPadding(keyBytes, cipher);
        _logger.LogInformation("解密: {0}", Hex.ToHexString(decryptedData));
        //如果自动会补字节数
        //output.WriteLine("解密: {0}", Encoding.UTF8.GetString(decryptedData));

        return Ok(new
        {
            data,
            hex = Hex.ToHexString(cipher).ToUpper(),
            base64 = Convert.ToBase64String(cipher),
        });
    }

    /// <summary>
    /// data:0123456789abcdeffedcba9876543210
    ///加密后的密文(hex): 89FD03EB48C8FCBCE723AD7E5E585515
    ///加密后的密文(hex): 89FD03EB48C8FCBCE723AD7E5E585515
    ///加密后的密文(base64): if0D60jI/LznI61+XlhVFQ==
    ///解密: 0123456789abcdeffedcba9876543210
    /// </summary>
    [HttpGet("SMSUtil_CBC_NoPadding")]
    public IActionResult SMSUtil_CBC_NoPadding(string data)
    {
        byte[] plaintext = Hex.Decode(data);// 需满足 (pSourceLen % 16 == 0)
        byte[] keyBytes = Hex.Decode("0123456789abcdeffedcba9876543210");//长度： 16B
        byte[] iv = Encoding.UTF8.GetBytes("!9^3mrLy8i^^eX2w");//长度： 16B
                                                               //byte[] plaintext = Encoding.UTF8.GetBytes("tuserid002");
                                                               //byte[] keyBytes = Encoding.UTF8.GetBytes("sW93ZE8rjDeD3!1m");

        // byte[] keyBytes = SM4Util.GenerateKey(16);
        //byte[] iv = SM4Util.GenerateKey(16);

        byte[] cipher = SM4Util.Encrypt_CBC_NoPadding(keyBytes, iv, plaintext);
        _logger.LogInformation("加密后的密文(hex): {0}", Hex.ToHexString(cipher).ToUpper());
        _logger.LogInformation("加密后的密文(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
        _logger.LogInformation("加密后的密文(base64): {0}", Convert.ToBase64String(cipher));

        byte[] decryptedData = SM4Util.Decrypt_CBC_NoPadding(keyBytes, iv, cipher);
        _logger.LogInformation("解密: {0}", Hex.ToHexString(decryptedData));

        return Ok(new
        {
            data,
            hex = Hex.ToHexString(cipher).ToUpper(),
            base64 = Convert.ToBase64String(cipher),
        });
    }
    /// <summary>
    ///加密后的密文(hex): 11d9b2e155ae15a9525455ba0a7ceed0 
    ///加密后的密文(base64) : Edmy4VWuFalSVFW6Cnzu0A== 
    /// </summary>
    [HttpGet("ECBTEST")]
    public IActionResult ECBTEST()
    {
        //秘钥
        SM4Crypto entity = new SM4Crypto();
        // entity.Iv = "!9^3mrLy8i^^eX2w";
        entity.Key = "sW93ZE8rjDeD3!1m";
        entity.Data = "tuserid002";//"{\"type\":\"identity\",\"identity\":\"11111\",\"timestamp\":\"2021-11-11 11:11:11\"}";
        entity.HexString = false;
        string hex = SM4Crypto.EncryptECB(entity);
        _logger.LogInformation("加密后的密文(hex): {0}", hex);
        string base64 = SM4Crypto.EncryptECBToBase64(entity);
        _logger.LogInformation("加密后的密文(base64): {0}", base64);
        //11d9b2e155ae15a9525455ba0a7ceed0
        _logger.LogInformation(base64);

        return Ok(new
        {
            data = entity.Data,
            hex,
            base64
        });
    }

}