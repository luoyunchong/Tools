# Tools

目标：实现在线预览，加密、编码、转换工具，功能如下：

- MD5 信息-摘要算法
- SM4/SM3/SM2国密
- DES
- BASE64
- URI 编码
- JSON格式化
- 图片验证码(跨平台)
- 时间戳

## 预览地址

- [https://igeekfan.cn/tools/#/](https://igeekfan.cn/tools/#/)

## 示例 DES

- JAVA+C# 相同的DES 加密/解密

### C# 调用

```csharp
    string password = "12345678";
    string sourceString = "{'idcard':'330781198509077211','date':'2021-11-11 19:04'}";
    Console.WriteLine("源字符串：" + sourceString);
    var c = DESUtil.Encrypt(sourceString, password);
    //加密：3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==
    Console.WriteLine("加密：" + c);

    c = HttpUtility.UrlEncode(c);
    Console.WriteLine("url编码：" + c);

    Console.WriteLine("================");

    c = HttpUtility.UrlDecode(c);
    Console.WriteLine("url解码：" + c);

    try
    {
        c = DESUtil.Decrypt(c, password);
        Console.WriteLine("url解密：" + c);

        var user = JsonConvert.DeserializeObject<User>(c);
        Console.WriteLine("反序列化\n日期：" + user.date);
        Console.WriteLine("身份证号：" + user.idcard);

    }
    catch (Exception e)
    {
        Console.WriteLine("无法解密：" + e.StackTrace + e.Message);
    }

    Console.WriteLine("------over--------");
    Console.ReadLine();
```

### JAVA调用

```java
    String password="12345678";

    String den = DESUtil.encrypt("{'idcard':'330781198509077211','date':'2021-11-11 19:04'}",password);
    System.out.println("加密:"+den);

    String CHARSET="utf-8";
    String encodeStr= URLEncoder.encode(den,CHARSET);
    System.out.println("url编码:"+den);

    String decodeUrl=URLDecoder.decode(encodeStr,CHARSET);
    System.out.println("url解码："+decodeUrl);

    String ert = DESUtil.decrypt(decodeUrl,password);
    System.out.println("url解密："+ert);
```
