using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YSWL.Web
{
    public partial class ClearCache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IDictionaryEnumerator de = Cache.GetEnumerator();
            ArrayList list = new ArrayList();
            StringBuilder str = new StringBuilder();
            while (de.MoveNext())
            {
                if (MvcApplication.IsAutoConn)//开启自动链接
                {
                    string tag = Common.CallContextHelper.GetClearTag();
                    if (de.Key.ToString().EndsWith("-" + tag) && !de.Key.ToString().StartsWith("ValidateLoginEx-"))
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
            Label1.Text = string.Format("<br>{0}<br>{1}", str.ToString(), Resources.SysManage.lblClearSucceed);
        }
    }
}