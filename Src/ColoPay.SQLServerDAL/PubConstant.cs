using System;
using System.Configuration;
namespace ColoPay.SQLServerDAL
{
    public class PubConstant
    {
        /// <summary>
        /// �õ�web.config������������ݿ������ַ�����
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = YSWL.Common.ConfigHelper.GetConfigString(configName);
            string ConStringEncrypt = YSWL.Common.ConfigHelper.GetConfigString("ConStringEncrypt");
            if (ConStringEncrypt == "true")
            {
                connectionString = YSWL.Common.DEncrypt.DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }

    }
}
