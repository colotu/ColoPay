using System;
using System.Collections;
using System.Text;
using YSWL.Common;

namespace ColoPay.Web.Admin.SysManage
{
    public partial class ClearCache : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 62; } } //系统管理_是否显示清空缓存

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnClear_Click(object sender, System.EventArgs e)
        {
            IDictionaryEnumerator de = Cache.GetEnumerator();
            ArrayList list = new ArrayList();
            StringBuilder str = new StringBuilder();
            while (de.MoveNext())
            {
                if (MvcApplication.IsAutoConn)//开启自动链接
                {
                    string tag = YSWL.Common.CallContextHelper.GetClearTag();
                    if (de.Key.ToString().EndsWith("-" + tag)&&!de.Key.ToString().StartsWith("ValidateLoginEx-"))
                    {
                        list.Add(de.Key.ToString());
                    }
                }
                else
                {
                    list.Add(de.Key.ToString());
                }
            }
            foreach (string key in list)
            {
                Cache.Remove(key);
                str.Append("<li>" + key + "......OK! <br>");
            }

            
            YSWL.Common.DataCache.ClearAll();
            Label1.Text = string.Format("<br>{0}<br>{1}", str.ToString(), Resources.SysManage.lblClearSucceed);
        }
    }
}