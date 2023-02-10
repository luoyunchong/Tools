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
using System.Text;
using System.Text.RegularExpressions;

namespace Tools.Api.Utils;

public sealed class SMCrypto
{
    /// <summary>
    /// 生成SM2公钥私钥
    /// </summary>
    /// <param name="publicKey">SM2公钥 16进制</param>
    /// <param name="privateKey">SM2私钥 16进制</param>
    public static void GenerateSm2KeyHex(out string publicKey, out string privateKey)
    {
        GenerateSm2Key(out var a, out var b);
        publicKey = Hex.ToHexString(a);
        privateKey = Hex.ToHexString(b);
    }

    /// <summary>
    /// 生成SM2公钥私钥
    /// </summary>
    /// <param name="publicKey">SM2公钥</param>
    /// <param name="privateKey">SM2私钥</param>
    public static void GenerateSm2Key(out byte[] publicKey, out byte[] privateKey)
    {
        var g = new ECKeyPairGenerator();
        g.Init(new ECKeyGenerationParameters(new ECDomainParameters(GMNamedCurves.GetByName("SM2P256V1")), new SecureRandom()));
        var k = g.GenerateKeyPair();
        publicKey = ((ECPublicKeyParameters)k.Public).Q.GetEncoded(false);
        privateKey = ((ECPrivateKeyParameters)k.Private).D.ToByteArray();
    }

    /// <summary>
    /// SM2加密
    /// </summary>
    /// <param name="sourceData">数据源 16进制字符串</param>
    /// <param name="publicKey">公钥 16进制字符串</param>
    /// <returns></returns>
    public static byte[] Sm2Encrypt(string sourceData, string publicKey)
    {
        return Sm2Encrypt(Decode(sourceData), Decode(publicKey));
    }

    /// <summary>
    /// SM2加密
    /// </summary>
    /// <param name="sourceData">数据源</param>
    /// <param name="publicKey">公钥</param>
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
    /// SM2解密
    /// </summary>
    /// <param name="sourceData">数据源 16进制字符串</param>
    /// <param name="privkey">私钥 16进制字符串</param>
    /// <returns></returns>
    public static byte[] Sm2Decrypt(string sourceData, string privkey)
    {
        return Sm2Decrypt(Decode(sourceData), Decode(privkey));
    }

    /// <summary>
    /// SM2解密
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
    /// 加签算法 标准C1C2C3模式
    /// </summary>
    /// <param name="sourceData">源数据</param>
    /// <param name="privateKey">私钥</param>
    /// <param name="userId">用户标识</param>
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
    /// 验签算法 标准C1C2C3模式
    /// </summary>
    /// <param name="sourceData">源数据</param>
    /// <param name="publicKey">公钥</param>
    /// <param name="signData">验签数据</param>
    /// <param name="userId">用户标识</param>
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
    /// 随机生成SM4秘钥 16位
    /// </summary>
    /// <returns></returns>
    public static string GenerateSm4Key()
    {
        string key = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 16);
        return key;
    }

    /// <summary>
    /// SM4加密 采用SM4/ECB/PKCS5Padding
    /// </summary>
    /// <param name="plaintext">字符串内容</param>
    /// <param name="sm4Key">sm4秘钥</param>
    /// <returns></returns>
    public static string Sm4Encrypt(string plaintext, string sm4Key)
    {
        byte[] sourceData = Encoding.UTF8.GetBytes(plaintext);
        byte[] keyBytes = Encoding.UTF8.GetBytes(sm4Key);
        return Sm4Encrypt(sourceData, keyBytes);
    }

    /// <summary>
    /// SM4加密 采用SM4/ECB/PKCS5Padding
    /// </summary>
    /// <param name="sourceData">源数据</param>
    /// <param name="keyBytes">sm4秘钥</param>
    /// <returns>Base64字符串</returns>
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
    /// SM4解密 采用SM4/ECB/PKCS5Padding
    /// </summary>
    /// <param name="base64Str">Base64字符串</param>
    /// <param name="sm4Key">sm4秘钥</param>
    /// <returns></returns>
    public static string Sm4Decrypt(string base64Str, string sm4Key)
    {
        byte[] sourceData = Convert.FromBase64String(base64Str);
        byte[] keyBytes = Encoding.UTF8.GetBytes(sm4Key);
        return Sm4Decrypt(sourceData, keyBytes);
    }

    /// <summary>
    /// SM4解密 采用SM4/ECB/PKCS5Padding
    /// </summary>
    /// <param name="sourceData">源数据</param>
    /// <param name="keyBytes">sm4秘钥</param>
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
    /// 16进制转字节数组
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static byte[] Decode(string key)
    {
        return Regex.IsMatch(key, "^[0-9a-f]+$", RegexOptions.IgnoreCase) ? Hex.Decode(key) : Convert.FromBase64String(key);
    }

}