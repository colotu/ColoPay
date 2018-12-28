using System;
using System.ComponentModel;
using System.Linq;

namespace ColoPay.WebApi.Common
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
            .GetField(value.ToString())
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
