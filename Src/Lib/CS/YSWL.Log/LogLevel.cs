﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YSWL.Log
{
    /// <summary>
    /// 日志级别枚举（可通过配置控制那些日志级别可以输出）
    /// 如：配置Debug级别，那么大于debug级别以上的均可以输出
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 输出所有级别的日志
        /// </summary>
        All = 0,

        /// <summary>
        /// 表示跟踪的日志级别
        /// </summary>
        Trace = 1,

        /// <summary>
        /// 表示调试的日志级别
        /// </summary>
        Debug = 2,

        /// <summary>
        /// 表示消息的日志级别
        /// </summary>
        Info = 3,

        /// <summary>
        /// 表示警告的日志级别
        /// </summary>
        Warn = 4,

        /// <summary>
        /// 表示错误的日志级别
        /// </summary>
        Error = 5,

        /// <summary>
        /// 表示严重错误的日志级别
        /// </summary>
        Fatal = 6,

        /// <summary>
        /// 关闭所有日志，不输出日志
        /// </summary>
        Off = 7

    }
}
