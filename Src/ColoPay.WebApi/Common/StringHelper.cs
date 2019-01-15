using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ColoPay.WebApi.Common
{
   
    public class StringHelper
    {
        private const string input_charset = "utf-8";
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

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="s"></param>
        /// <param name="_input_charset"></param>
        /// <returns></returns>
        public static string GetMD5(string s)
        {
            byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(Encoding.GetEncoding(input_charset).GetBytes(s));
            StringBuilder builder = new StringBuilder(0x20);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x").PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        public static string CreateField(string name, string strValue,int get_code=0)
        {
            if (get_code==1)
            {
                return string.Format(CultureInfo.InvariantCulture, "&{0}={1}", new object[] { name, strValue });
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, "<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\">", new object[] { name, strValue });
            } 
        }

        public static string CreateForm(string content, string action)
        {
            content = content + "<input type=\"submit\" value=\"在线支付\" style=\"display:none;\">";
            return string.Format(CultureInfo.InvariantCulture, "<form id=\"payform\" name=\"payform\" action=\"{0}\" method=\"POST\">{1}</form>", new object[] { action, content });
        }

        public static void SubmitPaymentForm(string formContent)
        {
            string s = formContent + "<script>document.forms['payform'].submit();</script>";
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }

    }
}