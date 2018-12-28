using System;
using System.Collections.Generic;
using System.Text;

namespace YSWL.Common
{
    /// <summary>
    /// ʱ���ʽת��������
    /// </summary>
    public class TimeParser
    {
        #region ����ת���ɷ���
        /// <summary>
        /// ����ת���ɷ���
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        } 
        #endregion

        #region ����ĳ��ĳ�����һ��
        /// <summary>
        /// ����ĳ��ĳ�����һ��
        /// </summary>
        /// <param name="year">���</param>
        /// <param name="month">�·�</param>
        /// <returns>��</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region ʱ����ת������
        /// <summary>
        /// ʱ����ת������
        /// </summary>
        public static int TimeToSecond(int hour, int minute, int second)
        {
            TimeSpan ts = new TimeSpan(hour, minute, second);
            return (int)ts.TotalSeconds;
        } 
        #endregion

        #region ��ת��Ϊʱ����
        /// <summary>
        /// ��ת��Ϊʱ����
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string SecondToDateTime(int seconds)
        {
            TimeSpan ts = new TimeSpan(0, 0, seconds);
            string totalTime = string.Format("{0:00}:{1:00}:{2:00}", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
            return totalTime;// (int)ts.TotalHours + ":" + ts.Minutes + ":" + ts.Seconds;
        } 
        #endregion

        #region ����ʱ���
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "��" + DateTime1.Day.ToString() + "��";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "Сʱǰ";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "����ǰ";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion

        #region ʱ���ʽ��
        /// <summary>
        /// ʱ���ʽ��
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <param name="isFormat"></param>
        /// <returns></returns>
        public static string DateTimeFormat(object obj, string format, bool isFormat)
        {
            string str = string.Empty;
            if (null != obj && PageValidate.IsDateTime(obj.ToString()))
            {
                if (isFormat)
                {
                    str = Convert.ToDateTime(obj).ToString(format);
                }
                else
                {
                    str = obj.ToString();
                }
            }
            return str;
        }


        #endregion


        /// <summary>
        /// ʱ���תΪC#��ʽʱ��
        /// </summary>
        /// <param name="timeStamp">Unixʱ�����ʽ</param>
        /// <returns>C#��ʽʱ��</returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// DateTimeʱ���ʽת��ΪUnixʱ�����ʽ
        /// </summary>
        /// <param name="time"> DateTimeʱ���ʽ</param>
        /// <returns>Unixʱ�����ʽ</returns>
        public static string GetTimeStamp(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return ((int)(time - startTime).TotalSeconds).ToString();
        }
    }
}
