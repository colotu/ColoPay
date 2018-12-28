using System;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace YSWL.Common
{
    public class CacheOption
    {
        private int _defaultDb = 1;
        public CacheType CacheType { get; set; }
        public string ReadWriteHosts { get; set; }
        public string ReadOnlyHosts { get; set; }
        public bool CancelProductKey { get; set; }
        public bool CancelEnterpriseKey { get; set; }

        public int DefaultDb
        {
            get { return _defaultDb; }
            set { _defaultDb = value; }
        }
    }

    public enum CacheType
    {
        Redis,
        IIS
    }
    #region ��̬DataCache

    /// <summary>
    /// ������صĲ�����  (���ݶ�̬����) (����Redis����)
    /// Copyright (C) ����δ��    
    /// </summary>
    public class DataCache
    {
        private static DataCacheCore dateCache = new DataCacheCore();
        public static CacheType CacheType = CacheType.IIS;

        public static void Init(CacheOption option)
        {
            dateCache = new DataCacheCore(option);
            CacheType = option.CacheType;
        }

        /// <summary>
        /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object GetCache(string CacheKey)
        {
            return dateCache.GetCache(CacheKey);
        }

        /// <summary>
        /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        /// <remarks>���л������ȡר��</remarks>
        public static T GetCache<T>(string CacheKey) where T : class
        {
            return dateCache.GetCache<T>(CacheKey);
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static bool SetCache(string CacheKey, dynamic objObject)
        {
            return dateCache.SetCache(CacheKey, objObject);
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public static bool SetCache(string CacheKey, dynamic objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            return dateCache.SetCache(CacheKey, objObject, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// ɾ����ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static bool DeleteCache(string CacheKey)
        {
            return dateCache.DeleteCache(CacheKey);
        }

        /// <summary>
        /// �����Ƴ�����
        /// </summary>
        /// <param name="CacheKey"></param>
        public static bool ClearBatch(string CacheKey)
        {
            return dateCache.ClearBatch(CacheKey);
        }

        /// <summary>
        /// �Ƴ����еĻ���
        /// </summary>
        public static bool ClearAll(bool IsAutoConn = false)
        {
            return dateCache.ClearAll(IsAutoConn);
        }
    }
    #endregion

    #region DataCacheCore

    /// <summary>
    /// ������صĲ�����  (���ݶ�̬����) (����Redis����)
    /// Copyright (C) ����δ��    
    /// </summary>
    public class DataCacheCore
    {
        private IDataCacheBaseProvider dataCacheProvider = new YSWL.Common.IISDataCache();  //Ĭ��ʹ��IIS����
        private CacheOption _cacheOption = new CacheOption { CacheType = CacheType.IIS, CancelProductKey = false, CancelEnterpriseKey = false };

        public DataCacheCore(CacheOption option = null)
        {
            if (option == null) return;

            _cacheOption = option;
            switch (option.CacheType)
            {
                case CacheType.Redis:
                    dataCacheProvider = new YSWL.Common.RedisDataCache(option.ReadWriteHosts, option.ReadOnlyHosts, option.DefaultDb);
                    break;
                case CacheType.IIS:
                    dataCacheProvider = new YSWL.Common.IISDataCache();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string GetCacheKey(string key)
        {
            if (_cacheOption.CancelProductKey && _cacheOption.CancelEnterpriseKey)
            {
                return string.Format("{0}", key);
            }
            if (_cacheOption.CancelProductKey && !_cacheOption.CancelEnterpriseKey)
            {
                return string.Format("{0}-{1}", key, Common.CallContextHelper.GetClearTag());
            }
            return string.Format("{0}-{2}-{1}", key, Common.CallContextHelper.GetClearTag(), Common.Globals.AssemblyProduct);
        }

        /// <summary>
        /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        [Obsolete]
        public dynamic GetCache(string CacheKey)
        {
            #region ���ݶ�̬���ӵ�ַ

            CacheKey = GetCacheKey(CacheKey);

            #endregion

            dynamic value = dataCacheProvider.GetCache<dynamic>(CacheKey);
            if (value is string && (value == "[]" || value == "\"\"")) return null;
            return value;
        }

        /// <summary>
        /// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        /// <remarks>���л������ȡר��</remarks>
        public T GetCache<T>(string CacheKey) where T : class
        {
            #region ���ݶ�̬���ӵ�ַ

            CacheKey = GetCacheKey(CacheKey);

            #endregion

            return dataCacheProvider.GetCache<T>(CacheKey);
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public bool SetCache(string CacheKey, dynamic objObject)
        {
            #region ���ݶ�̬���ӵ�ַ

            CacheKey = GetCacheKey(CacheKey);

            #endregion
            //���Json String ""�Ĳ���
            if (objObject is string)
            {
                return dataCacheProvider.SetCache<string>(CacheKey, objObject);
            }
            return dataCacheProvider.SetCache<dynamic>(CacheKey, objObject);
        }

        /// <summary>
        /// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <param name="objObject"></param>
        public bool SetCache(string CacheKey, dynamic objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            #region ���ݶ�̬���ӵ�ַ

            CacheKey = GetCacheKey(CacheKey);

            #endregion
            //���Json String ""�Ĳ���
            if (objObject is string)
            {
                return dataCacheProvider.SetCache<string>(CacheKey, objObject, absoluteExpiration, slidingExpiration);
            }
            return dataCacheProvider.SetCache<dynamic>(CacheKey, objObject, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// ɾ����ǰӦ�ó���ָ��CacheKey��Cacheֵ
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public bool DeleteCache(string CacheKey)
        {
            #region ���ݶ�̬���ӵ�ַ

            CacheKey = GetCacheKey(CacheKey);

            #endregion


            return dataCacheProvider.DeleteCache(CacheKey);
        }

        /// <summary>
        /// �����Ƴ�����
        /// </summary>
        /// <param name="CacheKey"></param>
        public bool ClearBatch(string CacheKey)
        {
            #region ���ݶ�̬���ӵ�ַ

            CacheKey = GetCacheKey(CacheKey);

            #endregion

            return dataCacheProvider.ClearBatch(CacheKey);
        }

        /// <summary>
        /// �Ƴ����еĻ���
        /// </summary>
        public bool ClearAll(bool IsAutoConn = false)
        {
            return dataCacheProvider.ClearAll(IsAutoConn);
        }
    }
    #endregion
}
