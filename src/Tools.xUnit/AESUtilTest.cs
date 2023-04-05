using System.Text;
using Tools;
using Xunit.Abstractions;

namespace Tools.xUnit
{
    public class AESUtilTest
    {

        ITestOutputHelper output;
        public AESUtilTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void Encrypt()
        {
            string text = AESUtil.Encrypt("8435u90503250", "szoaznzwbgxtdbjk");
            output.WriteLine(text);
            Assert.Equal("b2feB27R6NDcavxvIvtwEQ==", text);

            text = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            output.WriteLine(text);
            Assert.Equal("YjJmZUIyN1I2TkRjYXZ4dkl2dHdFUT09", text);
        }

        [Fact]
        public void Decrypt()
        {
            string text = Encoding.UTF8.GetString(Convert.FromBase64String("YjJmZUIyN1I2TkRjYXZ4dkl2dHdFUT09"));
            output.WriteLine(text);
            Assert.Equal("b2feB27R6NDcavxvIvtwEQ==", text);

            text = AESUtil.Decrypt("b2feB27R6NDcavxvIvtwEQ==", "szoaznzwbgxtdbjk");
            output.WriteLine(text);
            Assert.Equal("8435u90503250", text);

        }
    }
}
