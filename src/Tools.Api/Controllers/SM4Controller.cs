using System.Text;
using DSSM;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities.Encoders;

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
    ///���ܺ������(hex): 11d9b2e155ae15a9525455ba0a7ceed0 
    ///���ܺ������(base64) : Edmy4VWuFalSVFW6Cnzu0A== 
    /// </summary>
    [HttpGet("ECB")]
    public IActionResult ECB()
    {
        //��Կ
        SM4Crypto entity = new SM4Crypto();
        // entity.Iv = "!9^3mrLy8i^^eX2w";
        entity.Key = "sW93ZE8rjDeD3!1m";
        entity.Data = "tuserid002";//"{\"type\":\"identity\",\"identity\":\"11111\",\"timestamp\":\"2021-11-11 11:11:11\"}";
        entity.HexString = false;
        string hex = SM4Crypto.EncryptECB(entity);
        _logger.LogInformation("���ܺ������(hex): {0}", hex);
        string base64 = SM4Crypto.EncryptECBToBase64(entity);
        _logger.LogInformation("���ܺ������(base64): {0}", base64);
        //11d9b2e155ae15a9525455ba0a7ceed0
        _logger.LogInformation(base64);

        return Ok(new
        {
            data = entity.Data,
            hex,
            base64
        });
    }


    /// <summary>
    ///���ܺ������(hex): 11D9B2E155AE15A9525455BA0A7CEED0 
    ///���ܺ������(hex): 11D9B2E155AE15A9525455BA0A7CEED0 
    ///���ܺ������(base64): Edmy4VWuFalSVFW6Cnzu0A== 
    ///����: tuserid002
    /// </summary>
    /// <param name="data">Ҫ���ܵ��ı���tuserid002</param>
    [HttpGet("ECB_Padding")]
    public IActionResult ECB_Padding(string data)
    {
        byte[] plaintext = Encoding.UTF8.GetBytes(data);
        byte[] keyBytes = Encoding.UTF8.GetBytes("sW93ZE8rjDeD3!1m");

        byte[] cipher = SM4Util.Encrypt_ECB_Padding(keyBytes, plaintext);
        _logger.LogInformation("���ܺ������(hex): {0}", Hex.ToHexString(cipher).ToUpper());
        _logger.LogInformation("���ܺ������(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
        _logger.LogInformation("���ܺ������(base64): {0}", Convert.ToBase64String(cipher));

        byte[] decryptedData = SM4Util.Decrypt_ECB_Padding(keyBytes, cipher);
        _logger.LogInformation("����: {0}", Encoding.UTF8.GetString(decryptedData));

        return Ok(new
        {
            data,
            hex = Hex.ToHexString(cipher).ToUpper(),
            base64 = Convert.ToBase64String(cipher),
        });
    }

    /// <summary>
    /// 0123456789abcdeffedcba9876543210
    /// </summary>
    /// <param name="data"></param>
    [HttpGet("ECB_NoPadding")]
    public IActionResult ECB_NoPadding(string data)
    {
        byte[] plaintext = Hex.Decode(data);// ������ (pSourceLen % 16 == 0)
        byte[] keyBytes = Hex.Decode("0123456789abcdeffedcba9876543210");//���ȣ� 16B

        byte[] cipher = SM4Util.Encrypt_ECB_NoPadding(keyBytes, plaintext);
        _logger.LogInformation("���ܺ������(hex): {0}", Hex.ToHexString(cipher).ToUpper());
        _logger.LogInformation("���ܺ������(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
        _logger.LogInformation("���ܺ������(base64): {0}", Convert.ToBase64String(cipher));

        byte[] decryptedData = SM4Util.Decrypt_ECB_NoPadding(keyBytes, cipher);
        _logger.LogInformation("����: {0}", Hex.ToHexString(decryptedData));
        //����Զ��Ჹ�ֽ���
        //output.WriteLine("����: {0}", Encoding.UTF8.GetString(decryptedData));

        return Ok(new
        {
            data,
            hex = Hex.ToHexString(cipher).ToUpper(),
            base64 = Convert.ToBase64String(cipher),
        });
    }

    /// <summary>
    /// data:0123456789abcdeffedcba9876543210
    ///���ܺ������(hex): 89FD03EB48C8FCBCE723AD7E5E585515
    ///���ܺ������(hex): 89FD03EB48C8FCBCE723AD7E5E585515
    ///���ܺ������(base64): if0D60jI/LznI61+XlhVFQ==
    ///����: 0123456789abcdeffedcba9876543210
    /// </summary>
    [HttpGet("SMSUtil_CBC_NoPadding")]
    public IActionResult SMSUtil_CBC_NoPadding(string data)
    {
        byte[] plaintext = Hex.Decode(data);// ������ (pSourceLen % 16 == 0)
        byte[] keyBytes = Hex.Decode("0123456789abcdeffedcba9876543210");//���ȣ� 16B
        byte[] iv = Encoding.UTF8.GetBytes("!9^3mrLy8i^^eX2w");//���ȣ� 16B
                                                               //byte[] plaintext = Encoding.UTF8.GetBytes("tuserid002");
                                                               //byte[] keyBytes = Encoding.UTF8.GetBytes("sW93ZE8rjDeD3!1m");

        // byte[] keyBytes = SM4Util.GenerateKey(16);
        //byte[] iv = SM4Util.GenerateKey(16);

        byte[] cipher = SM4Util.Encrypt_CBC_NoPadding(keyBytes, iv, plaintext);
        _logger.LogInformation("���ܺ������(hex): {0}", Hex.ToHexString(cipher).ToUpper());
        _logger.LogInformation("���ܺ������(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
        _logger.LogInformation("���ܺ������(base64): {0}", Convert.ToBase64String(cipher));

        byte[] decryptedData = SM4Util.Decrypt_CBC_NoPadding(keyBytes, iv, cipher);
        _logger.LogInformation("����: {0}", Hex.ToHexString(decryptedData));

        return Ok(new
        {
            data,
            hex = Hex.ToHexString(cipher).ToUpper(),
            base64 = Convert.ToBase64String(cipher),
        });
    }

}