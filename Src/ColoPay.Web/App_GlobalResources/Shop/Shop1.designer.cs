//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   强类型资源类，用于查找本地化字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或删除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 Visual Studio 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Shop {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Shop() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.Shop", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   覆盖当前线程的 CurrentUICulture 属性
        ///   使用此强类型的资源类的资源查找。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///  查找类似 上传图片大小限制 的本地化字符串。
        /// </summary>
        internal static string ImageSizes {
            get {
                return ResourceManager.GetString("ImageSizes", resourceCulture);
            }
        }
        
        /// <summary>
        ///  查找类似 产品清晰图高度 的本地化字符串。
        /// </summary>
        internal static string NormalImageHeight {
            get {
                return ResourceManager.GetString("NormalImageHeight", resourceCulture);
            }
        }
        
        /// <summary>
        ///  查找类似 产品清晰图宽度 的本地化字符串。
        /// </summary>
        internal static string NormalImageWidth {
            get {
                return ResourceManager.GetString("NormalImageWidth", resourceCulture);
            }
        }
        
        /// <summary>
        ///  查找类似 产品缩略图高度 的本地化字符串。
        /// </summary>
        internal static string ThumbImageHeight {
            get {
                return ResourceManager.GetString("ThumbImageHeight", resourceCulture);
            }
        }
        
        /// <summary>
        ///  查找类似 产品缩略图宽度 的本地化字符串。
        /// </summary>
        internal static string ThumbImageWidth {
            get {
                return ResourceManager.GetString("ThumbImageWidth", resourceCulture);
            }
        }
    }
}
