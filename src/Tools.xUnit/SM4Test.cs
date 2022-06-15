
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

namespace SM4_Test
{
    public class SM4Test
    {
        ITestOutputHelper output;
        public SM4Test(ITestOutputHelper output)
        {
            this.output = output;
        }

        /// <summary>
        ///���ܺ������(hex): 11d9b2e155ae15a9525455ba0a7ceed0
        ///���ܺ������(base64) : Edmy4VWuFalSVFW6Cnzu0A==
        /// </summary>
        [Fact]
        public void ECB()
        {
            //��Կ
            SM4Crypto entity = new SM4Crypto();
            string data = "tuserid002";
            // entity.Iv = "!9^3mrLy8i^^eX2w";
            entity.Key = "sW93ZE8rjDeD3!1m";
            entity.Data = data;//"{\"type\":\"identity\",\"identity\":\"11111\",\"timestamp\":\"2021-11-11 11:11:11\"}";
            entity.HexString = false;
            string str = SM4Crypto.EncryptECB(entity);
            output.WriteLine("EncryptECB���ܺ������(hex): {0}", str);


            entity.Data = str;
            string obj = SM4Crypto.DecryptECB(entity);
            output.WriteLine("ECB�������ģ�(hex): {0}", obj);

            entity.Data = data;
            str = SM4Crypto.EncryptECBToBase64(entity);
            output.WriteLine("���ܺ������(base64): {0}", str);

            entity.Data = str;
            obj = SM4Crypto.DecryptECBFromBase64(entity);
            output.WriteLine("ECBFromBase64�������ģ�(base64): {0}", obj);

            //11d9b2e155ae15a9525455ba0a7ceed0
            Console.WriteLine(str);
        }


        /// <summary>
        ///���ܺ������(hex): 11D9B2E155AE15A9525455BA0A7CEED0
        ///���ܺ������(hex): 11D9B2E155AE15A9525455BA0A7CEED0
        ///���ܺ������(base64): Edmy4VWuFalSVFW6Cnzu0A==
        ///����: tuserid002
        /// </summary>
        [Fact]
        public void ECB_PaddingTest()
        {
            byte[] plaintext = Encoding.UTF8.GetBytes("tuserid002");
            byte[] keyBytes = Encoding.UTF8.GetBytes("sW93ZE8rjDeD3!1m");


            byte[] cipher = SM4Util.Encrypt_ECB_Padding(keyBytes, plaintext);
            output.WriteLine("���ܺ������(hex): {0}", Hex.ToHexString(cipher).ToUpper());
            output.WriteLine("���ܺ������(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
            output.WriteLine("���ܺ������(base64): {0}", Convert.ToBase64String(cipher));

            byte[] decryptedData = SM4Util.Decrypt_ECB_Padding(keyBytes, cipher);
            output.WriteLine("����: {0}", Encoding.UTF8.GetString(decryptedData));
        }

        /// <summary>
        /// ���ʹ��NoPadding,��Ҫ��֤keyBytes��plaintext���ֽ�����16�ı���:�������� (pSourceLen % 16 == 0)
        /// </summary>
        [Fact]
        public void ECB_NoPaddingTest()
        {
            byte[] plaintext = Hex.Decode("0123456789abcdeffedcba9876543210");// ������ (pSourceLen % 16 == 0)
            byte[] keyBytes = Hex.Decode("0123456789abcdeffedcba9876543210");//���ȣ� 16B

            byte[] cipher = SM4Util.Encrypt_ECB_NoPadding(keyBytes, plaintext);
            output.WriteLine("���ܺ������(hex): {0}", Hex.ToHexString(cipher).ToUpper());
            output.WriteLine("���ܺ������(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
            output.WriteLine("���ܺ������(base64): {0}", Convert.ToBase64String(cipher));

            byte[] decryptedData = SM4Util.Decrypt_ECB_NoPadding(keyBytes, cipher);
            output.WriteLine("����: {0}", Hex.ToHexString(decryptedData));
            //����Զ��Ჹ�ֽ���
            //output.WriteLine("����: {0}", Encoding.UTF8.GetString(decryptedData));
        }


        #region TEST
        /// <summary>
        /// CBC
        ///���ܺ������(hex): 36B784C41BEDC5F598BBED354F389029
        ///���ܺ������(hex) : NreExBvtxfWYu+01TziQKQ==
        /// </summary>
        [Fact]
        public void CBC()
        {
            byte[] plaintext = Encoding.ASCII.GetBytes("tuserid002");
            byte[] keyBytes = Encoding.ASCII.GetBytes("sW93ZE8rjDeD3!1m");
            byte[] iv = Encoding.ASCII.GetBytes("!9^3mrLy8i^^eX2w");
            // ����
            KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
            ParametersWithIV keyParamWithIv = new ParametersWithIV(key, iv);

            IBufferedCipher inCipher = CipherUtilities.GetCipher("SM4/CBC/PKCS7Padding");
            inCipher.Init(true, keyParamWithIv);
            byte[] cipher = inCipher.DoFinal(plaintext);
            output.WriteLine("���ܺ������(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
            output.WriteLine("���ܺ������(base64): {0}", Convert.ToBase64String(cipher));
            // ����
            inCipher.Reset();
            inCipher.Init(false, keyParamWithIv);
            byte[] bin = inCipher.DoFinal(cipher);
            string ans = Encoding.UTF8.GetString(bin);
            output.WriteLine("����(): {0}", ans);
        }

        /// <summary>
        ///���ܺ������(hex): 681EDF34D206965E86B3E94F536E4246
        ///���ܺ������(base64) : aB7fNNIGll6Gs+lPU25CRg==
        ///����(): 0123456789abcdeffedcba9876543210
        /// </summary>
        [Fact]
        public void CBC_NoPaddingTest()
        {
            byte[] plaintext = Hex.Decode("0123456789abcdeffedcba9876543210");// ������ (pSourceLen % 16 == 0)
            //byte[] plaintext = Hex.Encode(Encoding.ASCII.GetBytes("tuserid002")); 
            byte[] keyBytes = Hex.Decode("0123456789abcdeffedcba9876543210");//���ȣ� 16B, 24B, 32B
            // ����
            KeyParameter key = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
            IBufferedCipher inCipher = CipherUtilities.GetCipher("SM4/CBC/NoPadding");
            inCipher.Init(true, key);
            byte[] cipher = inCipher.DoFinal(plaintext);

            //MemoryStream bOut = new MemoryStream();
            //CipherStream cOut = new CipherStream(bOut, null, inCipher);
            //for (int i = 0; i != plaintext.Length / 2; i++)
            //{
            //    cOut.WriteByte(plaintext[i]);
            //}
            //cOut.Write(plaintext, plaintext.Length / 2, plaintext.Length - plaintext.Length / 2);
            //cOut.Close();
            //byte[] cipher = bOut.ToArray();


            output.WriteLine("���ܺ������(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
            output.WriteLine("���ܺ������(base64): {0}", Convert.ToBase64String(cipher));
            // ����
            inCipher.Reset();
            inCipher.Init(false, key);
            byte[] bin = inCipher.DoFinal(cipher);
            string ans = Hex.ToHexString(bin);
            output.WriteLine("����(): {0}", ans);
        }
        #endregion


        [Fact]
        public void SMSUtil_CBC_PaddingTest()
        {
            byte[] plaintext = Encoding.UTF8.GetBytes("tuserid002");
            byte[] keyBytes = Encoding.UTF8.GetBytes("sW93ZE8rjDeD3!1m");
            byte[] iv = Encoding.UTF8.GetBytes("!9^3mrLy8i^^eX2w");

            // byte[] keyBytes = SM4Util.GenerateKey(16);
            // byte[] iv = SM4Util.GenerateKey(16);

            byte[] cipher = SM4Util.Encrypt_CBC_Padding(keyBytes, iv, plaintext);
            output.WriteLine("���ܺ������(hex): {0}", Hex.ToHexString(cipher).ToUpper());
            output.WriteLine("���ܺ������(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
            output.WriteLine("���ܺ������(base64): {0}", Convert.ToBase64String(cipher));

            byte[] decryptedData = SM4Util.Decrypt_CBC_Padding(keyBytes, iv, cipher);
            output.WriteLine("����: {0}", Encoding.UTF8.GetString(decryptedData));

        }

        /// <summary>
        ///���ܺ������(hex): 89FD03EB48C8FCBCE723AD7E5E585515
        ///���ܺ������(hex): 89FD03EB48C8FCBCE723AD7E5E585515
        ///���ܺ������(base64): if0D60jI/LznI61+XlhVFQ==
        ///����: 0123456789abcdeffedcba9876543210
        /// </summary>
        [Fact]
        public void SMSUtil_CBC_NoPaddingTest()
        {
            byte[] plaintext = Hex.Decode("0123456789abcdeffedcba9876543210");// ������ (pSourceLen % 16 == 0)
            byte[] keyBytes = Hex.Decode("0123456789abcdeffedcba9876543210");//���ȣ� 16B
            byte[] iv = Encoding.UTF8.GetBytes("!9^3mrLy8i^^eX2w");//���ȣ� 16B
                                                                   //byte[] plaintext = Encoding.UTF8.GetBytes("tuserid002");
                                                                   //byte[] keyBytes = Encoding.UTF8.GetBytes("sW93ZE8rjDeD3!1m");

            // byte[] keyBytes = SM4Util.GenerateKey(16);
            //byte[] iv = SM4Util.GenerateKey(16);

            byte[] cipher = SM4Util.Encrypt_CBC_NoPadding(keyBytes, iv, plaintext);
            output.WriteLine("���ܺ������(hex): {0}", Hex.ToHexString(cipher).ToUpper());
            output.WriteLine("���ܺ������(hex): {0}", BitConverter.ToString(cipher, 0).Replace("-", string.Empty));
            output.WriteLine("���ܺ������(base64): {0}", Convert.ToBase64String(cipher));

            byte[] decryptedData = SM4Util.Decrypt_CBC_NoPadding(keyBytes, iv, cipher);
            output.WriteLine("����: {0}", Hex.ToHexString(decryptedData));
        }

        public static byte[] ZeroIv(String algo)
        {
            IBufferedCipher cipher = CipherUtilities.GetCipher(algo);
            int blockSize = cipher.GetBlockSize();
            byte[] iv = new byte[blockSize];
            Arrays.Fill(iv, (byte)0);
            return iv;
        }

        [Fact]
        public void ECB2322()
        {

        }

    }
}