
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System.Text;
using Xunit.Abstractions;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using DSSM;
using Tools;
using System.Security.Cryptography;

namespace Tools.xUnit
{
    public class MD5UtilTest
    {
        ITestOutputHelper output;
        public MD5UtilTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        ///             md5:066ACD44ECCB4D35667367390E920AD2
        ///             md5:066ACD44ECCB4D35667367390E920AD2
        ///      base64-md5:BMRNROZLTTVMC2C5DPIK0G==
        /// 16Œª base64-md5:7MTNNWZZZZK=
        /// </summary>
        [Fact]
        public void Md5()
        {
            string sourceString = "{'idcard':'330781198509077211','date':'2021-11-11 19:04'}";
            //√ÿ‘ø
            string md5 = MD5Util.Md5(sourceString);
            output.WriteLine("            md5:" + md5);

            md5 = MD5Util.Md5Hash(sourceString);
            output.WriteLine("            md5:" + md5);

            md5 = MD5Util.Md5ToBase64(sourceString);
            output.WriteLine("     base64-md5:" + md5);

            md5 = MD5Util.Md5ToBase64(sourceString, MD5Digit.Digit16);
            output.WriteLine("16Œª base64-md5:" + md5);
        }



    }
}