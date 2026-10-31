**English** | [简体中文](README.md)

# Tools preview package

The goal is to provide online previews and tools for encryption, encoding, conversion, and related tasks:

- MD5 message-digest algorithm
- SM4/SM3/SM2 cryptographic algorithms
- DES
- AES
- BASE64
- URI encoding
- JSON formatting
- Image CAPTCHA (cross-platform)
- Timestamps
- Online M3U8 file preview [http://101.34.7.82/tools/#/m3u8](http://101.34.7.82/tools/#/m3u8)

## Live previews

- UI [https://igeekfan.cn/tools/#/](https://igeekfan.cn/tools/#/)
- API [https://igeekfan.cn/tools_api/swagger/index.html](https://igeekfan.cn/tools_api/swagger/index.html)

## DES example

- Equivalent DES encryption and decryption in Java and C#

### C# example

```csharp
    string password = "12345678";
    string sourceString = "{'idcard':'000000000000000000','date':'2021-11-11 19:04'}";
    Console.WriteLine("Source string: " + sourceString);
    var c = DESUtil.Encrypt(sourceString, password);
    // The encrypted value is printed at runtime.
    Console.WriteLine("Encrypted: " + c);

    c = HttpUtility.UrlEncode(c);
    Console.WriteLine("URL encoding: " + c);

    Console.WriteLine("================");

    c = HttpUtility.UrlDecode(c);
    Console.WriteLine("URL decoding: " + c);

    try
    {
        c = DESUtil.Decrypt(c, password);
        Console.WriteLine("URL decryption: " + c);

        var user = JsonConvert.DeserializeObject<User>(c);
        Console.WriteLine("Deserialized date: " + user.date);
        Console.WriteLine("ID number: " + user.idcard);

    }
    catch (Exception e)
    {
        Console.WriteLine("Unable to decrypt: " + e.StackTrace + e.Message);
    }

    Console.WriteLine("------over--------");
    Console.ReadLine();
```

### Java example

```java
    String password="12345678";

    String den = DESUtil.encrypt("{'idcard':'000000000000000000','date':'2021-11-11 19:04'}",password);
    System.out.println("Encrypted: "+den);

    String CHARSET="utf-8";
    String encodeStr= URLEncoder.encode(den,CHARSET);
    System.out.println("URL encoding: "+den);

    String decodeUrl=URLDecoder.decode(encodeStr,CHARSET);
    System.out.println("URL decoding: "+decodeUrl);

    String ert = DESUtil.decrypt(decodeUrl,password);
    System.out.println("URL decryption: "+ert);
```
