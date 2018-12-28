using System;
using System.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace YSWL.DBUtility
{
    /// <summary>
    /// ����������
    /// </summary>
    public class PubConstant
    {
        public const string KEY_CONNECTION = "ConnectionString";
        public const string KEY_ENCRYPT = "ConStringEncrypt";

        private const string SQLSERVERDAL = "SQLServerDAL";

        public const string  KEY_AUTOCONNECTION = "AutoConnection"; 

        public static bool IsSQLServer = (GetConfigString("DAL").EndsWith(SQLSERVERDAL));


        /// <summary>
        /// �Ƿ�����̬�������ݿ�
        /// </summary>
        public static bool isAutoConn
        {
            get
            {
                return Common.Globals.SafeBool(ConfigurationManager.AppSettings[KEY_AUTOCONNECTION], false);
            }
        }


        /// <summary>
        /// ��ȡ�̶������ַ���
        /// </summary>
        public static string ConnectionString
        {
            get
            {

                if (isAutoConn)
                {
                    return YSWL.SAAS.PubConstant.GetConnectionStr();
                }
                //���»���
                ConfigurationManager.RefreshSection("appSettings");

                string connectionString = ConfigurationManager.AppSettings[KEY_CONNECTION];
                string conStringEncrypt = ConfigurationManager.AppSettings[KEY_ENCRYPT];
                if (conStringEncrypt == "true")
                {
                    connectionString = DESEncrypt.Decrypt(connectionString);
                }

                return connectionString;
            }
        }

        /// <summary>
        /// ��̬�õ�web.config������������ݿ������ַ�����
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            //���»���
            ConfigurationManager.RefreshSection("appSettings");
            string connectionString = ConfigurationManager.AppSettings[configName];
            string conStringEncrypt = ConfigurationManager.AppSettings[KEY_ENCRYPT];
            if (conStringEncrypt == "true")
            {
                connectionString = DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }

        /// <summary>
        /// �ӻ����еõ�AppSettings�е������ַ�����Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            string CacheKey = "AppSettings-" + key;
            object objModel = GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key];
                    if (objModel != null)
                    {
                        int CacheTime = 30;
                        SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel.ToString();
        }

        /// <summary>
        /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[CacheKey];
        }
        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }
    }
}
