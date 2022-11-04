using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.GM;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;

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

public static class E
{
    public static bool IsNullOrWhiteSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
}
public sealed class SMCrypto
{
    /// <summary>
    /// ����SM2��Կ˽Կ
    /// </summary>
    /// <param name="publicKey">SM2��Կ 16����</param>
    /// <param name="privateKey">SM2˽Կ 16����</param>
    public static void GenerateSm2KeyHex(out string publicKey, out string privateKey)
    {
        GenerateSm2Key(out var a, out var b);
        publicKey = Hex.ToHexString(a);
        privateKey = Hex.ToHexString(b);
    }

    /// <summary>
    /// ����SM2��Կ˽Կ
    /// </summary>
    /// <param name="publicKey">SM2��Կ</param>
    /// <param name="privateKey">SM2˽Կ</param>
    public static void GenerateSm2Key(out byte[] publicKey, out byte[] privateKey)
    {
        var g = new ECKeyPairGenerator();
        g.Init(new ECKeyGenerationParameters(new ECDomainParameters(GMNamedCurves.GetByName("SM2P256V1")), new SecureRandom()));
        var k = g.GenerateKeyPair();
        publicKey = ((ECPublicKeyParameters)k.Public).Q.GetEncoded(false);
        privateKey = ((ECPrivateKeyParameters)k.Private).D.ToByteArray();
    }

    /// <summary>
    /// SM2����
    /// </summary>
    /// <param name="sourceData">����Դ 16�����ַ���</param>
    /// <param name="publicKey">��Կ 16�����ַ���</param>
    /// <returns></returns>
    public static byte[] Sm2Encrypt(string sourceData, string publicKey)
    {
        return Sm2Encrypt(Decode(sourceData), Decode(publicKey));
    }

    /// <summary>
    /// SM2����
    /// </summary>
    /// <param name="sourceData">����Դ</param>
    /// <param name="publicKey">��Կ</param>
    /// <returns></returns>
    public static byte[] Sm2Encrypt(byte[] sourceData, byte[] publicKey)
    {
        var x9ec = GMNamedCurves.GetByName("SM2P256V1");
        ICipherParameters publicKeyParameters = new ECPublicKeyParameters(x9ec.Curve.DecodePoint(publicKey), new ECDomainParameters(x9ec));

        var sm2 = new SM2Engine(new SM3Digest());
        sm2.Init(true, new ParametersWithRandom(publicKeyParameters));
        byte[] res = sm2.ProcessBlock(sourceData, 0, sourceData.Length);
        return res;
    }

    /// <summary>
    /// SM2����
    /// </summary>
    /// <param name="sourceData">����Դ 16�����ַ���</param>
    /// <param name="privkey">˽Կ 16�����ַ���</param>
    /// <returns></returns>
    public static byte[] Sm2Decrypt(string sourceData, string privkey)
    {
        return Sm2Decrypt(Decode(sourceData), Decode(privkey));
    }

    /// <summary>
    /// SM2����
    /// </summary>
    /// <param name="sourceData"></param>
    /// <param name="privateKey"></param>
    /// <returns></returns>
    public static byte[] Sm2Decrypt(byte[] sourceData, byte[] privateKey)
    {
        var privateKeyParameters = new ECPrivateKeyParameters(new BigInteger(1, privateKey), new ECDomainParameters(GMNamedCurves.GetByName("SM2P256V1")));

        var sm2 = new SM2Engine(new SM3Digest());
        sm2.Init(false, privateKeyParameters);
        byte[] res = sm2.ProcessBlock(sourceData, 0, sourceData.Length);
        return res;
    }

    /// <summary>
    /// ��ǩ�㷨 ��׼C1C2C3ģʽ
    /// </summary>
    /// <param name="sourceData">Դ����</param>
    /// <param name="privateKey">˽Կ</param>
    /// <param name="userId">�û���ʶ</param>
    /// <returns></returns>
    public static byte[] Sign(byte[] sourceData, byte[] privateKey, byte[] userId = null)
    {
        var privateKeyParameters = new ECPrivateKeyParameters(new BigInteger(1, privateKey), new ECDomainParameters(GMNamedCurves.GetByName("SM2P256V1")));
        var sm2 = new SM2Signer(new SM3Digest());
        ICipherParameters cp;
        if (userId != null) cp = new ParametersWithID(new ParametersWithRandom(privateKeyParameters), userId);
        else cp = new ParametersWithRandom(privateKeyParameters);
        sm2.Init(true, cp);
        sm2.BlockUpdate(sourceData, 0, sourceData.Length);
        return sm2.GenerateSignature();
    }

    /// <summary>
    /// ��ǩ�㷨 ��׼C1C2C3ģʽ
    /// </summary>
    /// <param name="sourceData">Դ����</param>
    /// <param name="publicKey">��Կ</param>
    /// <param name="signData">��ǩ����</param>
    /// <param name="userId">�û���ʶ</param>
    /// <returns></returns>
    public static bool VerifySign(byte[] sourceData, byte[] publicKey, byte[] signData, byte[]? userId = null)
    {
        var x9ec = GMNamedCurves.GetByName("SM2P256V1");
        ICipherParameters publicKeyParameters = new ECPublicKeyParameters(x9ec.Curve.DecodePoint(publicKey), new ECDomainParameters(x9ec));
        var sm2 = new SM2Signer(new SM3Digest());
        ICipherParameters cp;
        if (userId != null) cp = new ParametersWithID(publicKeyParameters, userId);
        else cp = publicKeyParameters;
        sm2.Init(false, cp);
        sm2.BlockUpdate(sourceData, 0, sourceData.Length);
        return sm2.VerifySignature(signData);
    }

    /// <summary>
    /// �������SM4��Կ 16λ
    /// </summary>
    /// <returns></returns>
    public static string GenerateSm4Key()
    {
        string key = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 16);
        return key;
    }

    /// <summary>
    /// SM4���� ����SM4/ECB/PKCS5Padding
    /// </summary>
    /// <param name="plaintext">�ַ�������</param>
    /// <param name="sm4Key">sm4��Կ</param>
    /// <returns></returns>
    public static string Sm4Encrypt(string plaintext, string sm4Key)
    {
        byte[] sourceData = Encoding.UTF8.GetBytes(plaintext);
        byte[] keyBytes = Encoding.UTF8.GetBytes(sm4Key);
        return Sm4Encrypt(sourceData, keyBytes);
    }

    /// <summary>
    /// SM4���� ����SM4/ECB/PKCS5Padding
    /// </summary>
    /// <param name="sourceData">Դ����</param>
    /// <param name="keyBytes">sm4��Կ</param>
    /// <returns>Base64�ַ���</returns>
    public static string Sm4Encrypt(byte[] sourceData, byte[] keyBytes)
    {
        KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
        IBufferedCipher inCipher = CipherUtilities.GetCipher("SM4/ECB/PKCS5Padding");
        inCipher.Init(true, key);
        byte[] cipher = inCipher.DoFinal(sourceData);
        string str = Convert.ToBase64String(cipher);
        return str;
    }

    /// <summary>
    /// SM4���� ����SM4/ECB/PKCS5Padding
    /// </summary>
    /// <param name="base64Str">Base64�ַ���</param>
    /// <param name="sm4Key">sm4��Կ</param>
    /// <returns></returns>
    public static string Sm4Decrypt(string base64Str, string sm4Key)
    {
        byte[] sourceData = Convert.FromBase64String(base64Str);
        byte[] keyBytes = Encoding.UTF8.GetBytes(sm4Key);
        return Sm4Decrypt(sourceData, keyBytes);
    }

    /// <summary>
    /// SM4���� ����SM4/ECB/PKCS5Padding
    /// </summary>
    /// <param name="sourceData">Դ����</param>
    /// <param name="keyBytes">sm4��Կ</param>
    /// <returns></returns>
    public static string Sm4Decrypt(byte[] sourceData, byte[] keyBytes)
    {
        KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
        IBufferedCipher outCipher = CipherUtilities.GetCipher("SM4/ECB/PKCS5Padding");
        outCipher.Init(false, key);
        byte[] cipher = outCipher.DoFinal(sourceData);
        string ans = Encoding.UTF8.GetString(cipher);
        return ans;
    }

    /// <summary>
    /// 16����ת�ֽ�����
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static byte[] Decode(string key)
    {
        return Regex.IsMatch(key, "^[0-9a-f]+$", RegexOptions.IgnoreCase) ? Hex.Decode(key) : Convert.FromBase64String(key);
    }

}