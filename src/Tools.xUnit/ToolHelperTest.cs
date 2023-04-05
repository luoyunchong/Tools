using System.Text;
using Xunit.Abstractions;

namespace Tools.xUnit
{
    public class ToolHelperTest
    {

        ITestOutputHelper output;
        public ToolHelperTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Test2()
        {
            byte b = 1;
            byte b1 = 0xf;//以0x开头 即16进制的写法
            byte b2 = 15;
            Assert.Equal(b1, b2);

            byte b3 = (byte)(b1 + b2);
            //或
            int b4 = b1 + b2;

            byte[] myByteArray = new byte[10];
            for (var i = 0; i < myByteArray.Length; i++)
            {
                myByteArray[i] = 0x01;
            }
        }

        [Fact]
        public void Test()
        {
            byte[] plaintext = Encoding.UTF8.GetBytes("0123456789");
            plaintext = Encoding.UTF8.GetBytes("ABCDabcd");
            plaintext = Encoding.UTF8.GetBytes("中国");

            string hex = ByteToHex("中国");

            Assert.Equal(ByteToHex(hex), ByteToHex2(hex));

            string hexChinese = "E4B8ADE59BBD";
            byte[] bytes = HexToByte(hexChinese);
            string text = Encoding.UTF8.GetString(bytes);

        }

        public string ByteToHex(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            string hex = BitConverter.ToString(bytes, 0).Replace("-", string.Empty);
            return hex;
        }

        public string ByteToHex2(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            StringBuilder ret = new StringBuilder();
            foreach (byte b in bytes)
            {
                //{0:x2} 小写
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        public byte[] HexToByte(string hex)
        {
            byte[] inputByteArray = new byte[hex.Length / 2];
            for (var x = 0; x < inputByteArray.Length; x++)
            {
                var i = Convert.ToInt32(hex.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            return inputByteArray;
        }
    }
}
