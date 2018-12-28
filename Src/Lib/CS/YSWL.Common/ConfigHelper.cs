 using System;

namespace YSWL.Common
{
    /// <summary>
    /// web.config������
    /// Copyright (C) ����δ��
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// �õ�AppSettings�е������ַ�����Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            string cacheKey = "AppSettings-" + key;
            object objModel = YSWL.Common.DataCache.GetCache(cacheKey);
            if (objModel == null)
            {
                try
                {
                    //TODO: Ӧ����exeʱ��config�ļ���ȡ BEN ADD 2013-03-18 11:54
                    //DONE: ���������ļ���ȡ����BUG BEN MODIFY 2013-03-05 20:07
                    System.Configuration.Configuration rootWebConfig =
                        System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                    if (0 < rootWebConfig.AppSettings.Settings.Count &&
                        rootWebConfig.AppSettings.Settings[key] != null)
                    {
                        objModel = rootWebConfig.AppSettings.Settings[key].Value;
                        if (objModel == null || String.IsNullOrWhiteSpace(objModel.ToString())) //����exeʱ��config�ļ���ȡ
                        {
                            objModel = System.Configuration.ConfigurationManager.AppSettings[key];
                        }
                        YSWL.Common.DataCache.SetCache(cacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                }
                catch (Exception ) {}
            }
            return objModel != null ? objModel.ToString() : null;
        }

        /// <summary>
        /// �õ�exe AppSettings�е������ַ�����Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetExeConfigString(string key)
        {
               return      System.Configuration.ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// �õ�AppSettings�е�����Bool��Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(key);
            if (!string.IsNullOrWhiteSpace(cfgVal))
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
        /// <summary>
        /// �õ�AppSettings�е�����Decimal��Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (!string.IsNullOrWhiteSpace(cfgVal))
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        /// <summary>
        /// �õ�AppSettings�е�����int��Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (!string.IsNullOrWhiteSpace(cfgVal))
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
    }
}
