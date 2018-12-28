using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace YSWL.Common.DEncrypt
{
    /// <summary>
    /// Encrypt ��ժҪ˵����
    /// </summary>
    public class DEncrypt
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        public DEncrypt()
        {
        }

        #region ʹ�� ȱʡ��Կ�ַ��� ����/����string

        /// <summary>
        /// ʹ��ȱʡ��Կ�ַ�������string
        /// </summary>
        /// <param name="original">����</param>
        /// <returns>����</returns>
        public static string Encrypt(string original)
        {
            return Encrypt(original, "YSWL");
        }
        /// <summary>
        /// ʹ��ȱʡ��Կ�ַ�������string
        /// </summary>
        /// <param name="original">����</param>
        /// <returns>����</returns>
        public static string Decrypt(string original)
        {
            return Decrypt(original, "YSWL", System.Text.Encoding.Default);
        }

        #endregion

        #region ʹ�� ������Կ�ַ��� ����/����string
        /// <summary>
        /// ʹ�ø�����Կ�ַ�������string
        /// </summary>
        /// <param name="original">ԭʼ����</param>
        /// <param name="key">��Կ</param>
        /// <param name="encoding">�ַ����뷽��</param>
        /// <returns>����</returns>
        public static string Encrypt(string original, string key)
        {
            byte[] buff = System.Text.Encoding.Default.GetBytes(original);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return Convert.ToBase64String(Encrypt(buff, kb));
        }
        /// <summary>
        /// ʹ�ø�����Կ�ַ�������string
        /// </summary>
        /// <param name="original">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static string Decrypt(string original, string key)
        {
            return Decrypt(original, key, System.Text.Encoding.Default);
        }

        /// <summary>
        /// ʹ�ø�����Կ�ַ�������string,����ָ�����뷽ʽ����
        /// </summary>
        /// <param name="encrypted">����</param>
        /// <param name="key">��Կ</param>
        /// <param name="encoding">�ַ����뷽��</param>
        /// <returns>����</returns>
        public static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            byte[] buff = Convert.FromBase64String(encrypted);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return encoding.GetString(Decrypt(buff, kb));
        }
        #endregion

        #region ʹ�� ȱʡ��Կ�ַ��� ����/����/byte[]
        /// <summary>
        /// ʹ��ȱʡ��Կ�ַ�������byte[]
        /// </summary>
        /// <param name="encrypted">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes("YSWL");
            return Decrypt(encrypted, key);
        }
        /// <summary>
        /// ʹ��ȱʡ��Կ�ַ�������
        /// </summary>
        /// <param name="original">ԭʼ����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static byte[] Encrypt(byte[] original)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes("YSWL");
            return Encrypt(original, key);
        }
        #endregion

        #region  ʹ�� ������Կ ����/����/byte[]

        /// <summary>
        /// ����MD5ժҪ
        /// </summary>
        /// <param name="original">����Դ</param>
        /// <returns>ժҪ</returns>
        public static byte[] MakeMD5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(original);
            hashmd5 = null;
            return keyhash;
        }
        /// <summary>
        /// ����MD5�����ַ���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5FromStr(string str)
        {
            byte[] by = System.Text.Encoding.Default.GetBytes(str); //���ַ��������ֽ�������
            byte[] by1 = MD5.Create().ComputeHash(by);
            StringBuilder builder = new StringBuilder();//�ɱ���ַ��� ����������յ�MD5ֵ
            for (int i = 0; i < by1.Length; i++)
            {
                builder.Append(by1[i].ToString("x2"));  //by1[i].ToString ("x2")���趨��ʽ
            }
            return builder.ToString();
        }
        /// <summary>
        /// ʹ�ø�����Կ����
        /// </summary>
        /// <param name="original">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        /// <summary>
        /// ʹ�ø�����Կ��������
        /// </summary>
        /// <param name="encrypted">����</param>
        /// <param name="key">��Կ</param>
        /// <returns>����</returns>
        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        #endregion


        /// <summary>
        /// ��ȡ��ҵ���ܴ�
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <param name="sjc"></param>
        /// <returns></returns>
        public static string GetEncryptionStr(long enterpriseId)
        {
            string temp = ((enterpriseId + 31) * 19) + "";
            int lenth = temp.ToString().Length;

            string[] strs = new string[] { };

            if (lenth > 3 && lenth <= 6)
            {
                lenth = 4;
            }
            else if (lenth > 6)
            {
                lenth = 5;
            }

            StringBuilder sb = new StringBuilder();
            int i = 0;
            for (; i < lenth; i++)
            {
                var t = int.Parse(temp.Substring(i, 1));
                //�������ִ�С��ȡ�ַ���  
                if (t <= 5)
                {
                    sb.Append(t + GetRandomStr(1, t));
                }
                else
                {
                    sb.Append(t + GetRandomStr(2, t));
                }
                //0-5 ȡ1  6-9ȡ2 
            }
            sb.Append(temp.Substring(i));
            return sb.ToString();
        }

        /// <summary>
        /// ��ȡ�����
        /// </summary>
        /// <param name="codeCount">��ȡ����λ�����</param>
        /// <param name="number">ԭ���н�ȡ������</param>
        /// <returns></returns>
        public static string GetRandomStr(int codeCount, int number)
        {
            string allChar = "c d e f A B I J K T U V x y W X a b g L M N O h i E j k l C D F G H m n o p P Q R S q r s t Y Z u v w z";
            string[] allCharArray = allChar.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string randomCode = "";
            Random rand = new Random(codeCount * ((int)(DateTime.Now.AddMonths(number).Ticks)));
            int temp = rand.Next(43);
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(43);
                if (temp == t)
                {
                    return GetRandomStr(codeCount, number);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
        /// <summary>
        /// ǿת����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ConvertToNumber(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            string regex = @"[0-9]";
            Regex rgClass = new Regex(regex, RegexOptions.Singleline);

            MatchCollection matchs = rgClass.Matches(str);
            StringBuilder sb = new StringBuilder();
            foreach (var item in matchs)
            {
                sb.Append(item);
            }
            if (String.IsNullOrWhiteSpace(sb.ToString()))
            {
                return 0;
            }
            double temp = double.Parse(sb + "") / 19 - 31;
            if (temp <= 0 || (temp + "").IndexOf('.') >= 0)
            {
                return 0;
            }
            return long.Parse(temp + "");
        }
    }
}
