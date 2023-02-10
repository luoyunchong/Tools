namespace Tools.Api.DTO;

/// <summary>
/// 得到MD5的多种结构
/// </summary>
/// <param name="Md5Digit32">32位加密</param>
/// <param name="Md5Digit16">16位加密 </param>
/// <param name="Md5Digit32Lower">32位小写</param>
/// <param name="Md5Digit16Lower">16位小写</param>
/// <param name="Base64Md5Digit32">base64编码32位加密</param>
/// <param name="Base64Md5Digit16">base64编码16位加密</param>
public record MD5Output(string Md5Digit32, string Md5Digit16, string Md5Digit32Lower, string Md5Digit16Lower, string Base64Md5Digit32, string Base64Md5Digit16);
