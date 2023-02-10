namespace Tools
{

    /// <summary>
    /// 输入：{'idcard':'330781198509077211','date':'2021-11-11 19:04'} <br></br>
    /// 32位：066ACD44ECCB4D35667367390E920AD2<br></br>
    /// 16位：ECCB4D3566736739<br></br>
    /// </summary>
    public class MD5Input
    {
        /// <summary>
        /// 消息串
        /// </summary>
        public string SourceString { get; set; }
        /// <summary>
        /// 是否base64编码
        /// </summary>
        public bool Base64 { get; set; }
        /// <summary>
        /// 位数32/16
        /// </summary>
        public MD5Digit Digit { get; set; }
        /// <summary>
        /// 是否大写
        /// </summary>
        public bool Capital { get; set; }
    }
}
