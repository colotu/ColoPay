namespace ColoPay.WebApi.Common
{
    public class StringHelper
    {
        /// <summary>
        /// <函数：Encode>
        /// 作用：将字符串内容转化为16进制数据编码，其逆过程是Decode
        /// 参数说明：
        /// strEncode 需要转化的原始字符串
        /// 转换的过程是直接把字符转换成Unicode字符,比如数字"3"-->0033,汉字"我"-->U+6211
        /// 函数decode的过程是encode的逆过程.
        /// </summary>
        /// <param name="strEncode"></param>
        /// <returns></returns>
        public static string Encode(string strEncode)
        {
            //string strReturn = "";//  存储转换后的编码
            //foreach (short shortx in strEncode.ToCharArray())
            //{
            //    strReturn += shortx.ToString("X4");
            //}
            //return strReturn;

            return YSWL.Common.DEncrypt.Hex16.Encode(strEncode);
        }

        /// <summary>
        /// <函数：Decode>
        /// 作用：将16进制数据编码转化为字符串，是Encode的逆过程
        /// </summary>
        /// <param name="strDecode"></param>
        /// <returns></returns>
        public static string Decode(string strDecode)
        {
            //string sResult = "";
            //for (int i = 0; i < strDecode.Length / 4; i++)
            //{
            //    sResult += (char)short.Parse(strDecode.Substring(i * 4, 4), global::System.Globalization.NumberStyles.HexNumber);
            //}
            //return sResult;

            return YSWL.Common.DEncrypt.Hex16.Decode(strDecode);
        }
    }
}