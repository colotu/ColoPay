using System;
using System.Text;
using System.Web.UI;
namespace YSWL.Common
{
	/// <summary>
	/// ��ʾ��Ϣ��ʾ�Ի���
	/// ����δ��	
	/// </summary>
	public class MessageBox
	{		
		private  MessageBox()
		{			
		}

		/// <summary>
		/// ��ʾ��Ϣ��ʾ�Ի���
		/// </summary>
		/// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
		/// <param name="msg">��ʾ��Ϣ</param>
		public static void  Show(System.Web.UI.Page page,string msg)
		{            
            page.ClientScript.RegisterStartupScript(page.GetType(),"message", "<script language='javascript' defer>alert('" + msg + "');</script>");

            // UpdatePanel�������·�ʽ�����Ի���
            //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "javascript", "alert('���Ѿ�Ͷ��Ʊ��');", true);
		}

        /// <summary>
        /// ��ʾ��������æ��ʾ��Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowServerBusyTip(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowServerBusyTip('" + msg + "');</script>");
        }

        /// <summary>
        /// ��ʾ�����ɹ���ʾ��Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowSuccessTip(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowSuccessTip('" + msg + "');</script>");
        }

        /// <summary>
        /// ��ʾ����ʧ�ܵ���ʾ��Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowFailTip(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowFailTip('" + msg + "');</script>");
        }

        /// <summary>
        /// ��ʾ���ڼ��ص���ʾ��Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowLoadingTip(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowLoadingTip('" + msg + "');</script>");
        }
        
        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򣬲�����ԭҳ��
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void ShowAndBack(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');history.back();</script>");
        }

		/// <summary>
		/// �ؼ���� ��Ϣȷ����ʾ��
		/// </summary>
		/// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
		/// <param name="msg">��ʾ��Ϣ</param>
		public static void  ShowConfirm(System.Web.UI.WebControls.WebControl Control,string msg)
		{
			//Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
			Control.Attributes.Add("onclick", "return confirm('" + msg + "');") ;
		}

		/// <summary>
		/// ��ʾ��Ϣ��ʾ�Ի��򣬲�����ҳ����ת
		/// </summary>
		/// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
		/// <param name="msg">��ʾ��Ϣ</param>
		/// <param name="url">��ת��Ŀ��URL</param>
		public static void ShowAndRedirect(System.Web.UI.Page page,string msg,string url)
		{            
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');window.location=\"" + url + "\"</script>");
		}

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի���(��ҳ��)������ҳ����ת
        /// </summary>
        /// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="url">��ת��Ŀ��URL</param>
        public static void ShowAndRedirects(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript'defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());

        }

		/// <summary>
		/// ����Զ���ű���Ϣ
		/// </summary>
		/// <param name="page">��ǰҳ��ָ�룬һ��Ϊthis</param>
		/// <param name="script">����ű�</param>
		public static void ResponseScript(System.Web.UI.Page page,string script)
		{
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");             
		}

        /// <summary>
        /// ��ʾ��������æ��ʾ��Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowServerBusyTip(System.Web.UI.Page page, string msg,string url )
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowServerBusyTip('" + msg + "');function jump(count){window.setTimeout(function(){count--;if(count>0){jump(count)}else{window.location.href=\"" + url + "\"}},1000)}jump(1);</script>");
        }

        /// <summary>
        /// ��ʾ�����ɹ���ʾ��Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowSuccessTip(System.Web.UI.Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowSuccessTip('" + msg + "');function jump(count){window.setTimeout(function(){count--;if(count>0){jump(count)}else{window.location.href=\"" + url + "\"}},1000)}jump(1);</script>");
        }
        /// <summary>
        /// ��ʾ�����ɹ���ʾ��Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowSuccessTipScript(System.Web.UI.Page page, string msg, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowSuccessTip('" + msg + "');function jump(count){window.setTimeout(function(){count--;if(count>0){jump(count)}else{" + script + "}},1000)}jump(1);</script>");
        }
        /// <summary>
        /// ��ʾ����ʧ�ܵ���ʾ��Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowFailTip(System.Web.UI.Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowFailTip('" + msg + "');function jump(count){window.setTimeout(function(){count--;if(count>0){jump(count)}else{window.location.href=\"" + url + "\"}},1000)}jump(1);</script>");
        }


        /// <summary>
        /// ��ʾ�����ɹ���ʾ��Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowFailTipScript(System.Web.UI.Page page, string msg, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowFailTip('" + msg + "');function jump(count){window.setTimeout(function(){count--;if(count>0){jump(count)}else{" + script + "}},1000)}jump(1);</script>");
        }

        /// <summary>
        /// ��ʾ���ڼ��ص���ʾ��Ϣ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowLoadingTip(System.Web.UI.Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowLoadingTip('" + msg + "');function jump(count){window.setTimeout(function(){count--;if(count>0){jump(count)}else{window.location.href=\"" + url + "\"}},1000)}jump(1);</script>");
        }

	}
}
