using Newtonsoft.Json;
using System.Text;
using System.Web;
using Tools;
using Xunit.Abstractions;

namespace Tools.xUnit
{
    public record User(string idcard, string date);

    public class DESUtilTest
    {
        ITestOutputHelper output;
        public DESUtilTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Encrypt()
        {
            string password = "12345678";
            string sourceString = "{'idcard':'330781198509077211','date':'2021-11-11 19:04'}";
            output.WriteLine("源字符串：" + sourceString);
            var c = DESUtil.Encrypt(sourceString, password);
            //加密：3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==
            output.WriteLine("加密：" + c);

            c = HttpUtility.UrlEncode(c);
            output.WriteLine("url编码：" + c);

            output.WriteLine("================");

            c = HttpUtility.UrlDecode(c);
            output.WriteLine("url解码：" + c);
        }

        [Fact]
        public void Decrypt()
        {
            string password = "12345678";
            string c = "3tL0BBKZyUpIfO+XJKL1VoeQhEWc0enGG8R//RPBJQCiykspXEBmvabp8yrWTBv+QUL62K7dUL+vbpYV/PwZvw==";
            c = DESUtil.Decrypt(c, password);
            output.WriteLine("url解密：" + c);

            var user = JsonConvert.DeserializeObject<User>(c);
            output.WriteLine("反序列化\n日期：" + user.date);
            output.WriteLine("身份证号：" + user.idcard);

        }
    }
}
