using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace ColoPay.WebApi.Common
{
    public class FileHelper
    {
        /// <summary>
        /// 保存文件(将base64编码的文件保存在指定的路径)
        /// </summary>
        /// <param name="filePath">文件路径(本地路径)</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="fileStr">文件(base64字符串)</param>
        /// <returns></returns>
        public static void SavaFile(string filePath, string fileName, string fileStr)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(fileStr)))
            {
                ms.Seek(0, SeekOrigin.Begin);
                using (Stream fs = new FileStream(filePath + fileName, FileMode.Create))
                {
                    ms.WriteTo(fs);
                }
            }
        }

       
        #region 生成分享推广二维码
        /// <summary>
        /// 生成分享推广二维码
        /// </summary>
        /// <param name="nickName">昵称</param>
        /// <param name="advertise">推广宣传语</param>
        /// <param name="advPoint">推广宣传语坐标</param>
        public static void GetQRCode(string nickName, string advertise, Point advPoint, string imagePath, string QRUrl, string savaPath)
        {
            //基本参数
            PixelFormat pixelFormat = PixelFormat.Format24bppRgb;   //指定图像中每个像素的颜色数据的格式
            int width = 430;    //图像宽
            int higth = 740;   //图像高
            Bitmap bit = new Bitmap(width, higth, pixelFormat);
            Graphics picture = Graphics.FromImage(bit);
            picture.Clear(Color.White);     //以白色背景色填充
            picture.SmoothingMode = SmoothingMode.AntiAlias;


            //头像
            try
            {
                Avatar(149, 30, 132, 132, Image.FromFile(imagePath), picture);
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(ex.Message, ex.StackTrace);
            }


            //昵称
            Font font1 = new Font("微软雅黑", 20);  //字体 字体大小单位为磅不是px!
            SolidBrush brush1 = new SolidBrush(Color.Black); //格式刷
            nickName = "我是:" + nickName;
            int x = 0;


            if (nickName.Length > 10)
            {
                nickName = nickName.Substring(0, 10) + "\n" + nickName.Substring(10);
                x = 155;
            }
            else
            {
                x = (430 - (nickName.Length - 1) * 27) / 2;

            }

            Point nickPoint = new Point(x, 180);
            try
            {

                Text(nickPoint, font1, brush1, nickName, picture);

            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(ex.Message, ex.StackTrace);
            }



            //绘制二维码

            int x1 = 0;     //二维码横轴坐标
            int y1 = 280;     //二维码纵轴坐标
            int width1 = 430;  //二维码的宽
            int hight1 = 430;  //二维码的高 
            try
            {
                Qrcode(x1, y1, width1, hight1, picture, QRUrl);
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(ex.Message, ex.StackTrace);
            }

            //推广文字
            Font font2 = new Font("微软雅黑", 13);  //字体
            SolidBrush brush2 = new SolidBrush(Color.FromArgb(113, 113, 113)); //格式刷
            try
            {
                Text(advPoint, font2, brush2, advertise, picture);
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(ex.Message, ex.StackTrace);
            }




            //绘制说明文字
            Font font3 = new Font("微软雅黑", 12);  //字体
            SolidBrush brush3 = new SolidBrush(Color.FromArgb(113, 113, 113)); //格式刷
            try
            {
                Text(new Point(124, 700), font3, brush3, "长按此图识别图中二维码", picture);
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(ex.Message, ex.StackTrace);
            }

            //保存及释放资源
            try
            {
                picture.Dispose();
                Bitmap bit2 = new Bitmap(width, higth, pixelFormat);
                Graphics draw = Graphics.FromImage(bit2);
                draw.DrawImage(bit, 0, 0);
                bit.Dispose();
                bit2.Save(savaPath);
                bit2.Dispose();
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(ex.Message, ex.StackTrace);
            }
        }

        public static void GetQRCode(string imagePath, string QRUrl, string savaPath, string nickName)
        {

            int adv_x = 50;

            string advertise = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MShop_QRAd");

            if (advertise.Length > 22 && advertise.Length < 44)
            {
                advertise = advertise.Substring(0, 22) + "\n" + advertise.Substring(22);

            }
            else
            {
                adv_x = (430 - (advertise.Length * 17)) / 2;
            }

            Point advPoint = new Point(adv_x, 250);

            GetQRCode(nickName, advertise, advPoint, imagePath, QRUrl, savaPath);

        }


        /// <summary>
        /// 绘制二维码部分
        /// </summary>
        /// <param name="xpos">距画布左顶点横轴坐标</param>
        /// <param name="ypos">距画布左顶点纵轴坐标</param>
        /// <param name="width">绘制图像的宽度</param>
        /// <param name="hight">绘制图像的高度</param>
        /// <param name="picture">画布</param>
        /// <param name="QRUrl">二维码的地址(远程)</param>
        private static void Qrcode(int xpos, int ypos, int width, int hight, Graphics picture, string QRUrl)
        {
            Image img = null;

            System.Net.WebRequest webreq = System.Net.WebRequest.Create(QRUrl);
            System.Net.WebResponse webres = webreq.GetResponse();
            using (System.IO.Stream stream = webres.GetResponseStream())
            {
                img = Image.FromStream(stream);
            }
            picture.DrawImage(img, xpos, ypos, width, hight);
        }


        /// <summary>
        /// 绘制文字部分
        /// </summary>
        /// <param name="point">距离画布左顶点的坐标对</param>
        /// <param name="font">字体</param>
        /// <param name="brush">格式刷</param>
        /// <param name="text">文字内容</param>
        /// <param name="picture">画布</param>
        private static void Text(Point point, Font font, SolidBrush brush, string text, Graphics picture)
        {
            picture.DrawString(text, font, brush, point);
        }

        /// <summary>
        /// 绘制头像部分
        /// </summary>
        /// <param name="xpos">距画布左顶点横轴坐标</param>
        /// <param name="ypos">距画布左顶点纵轴坐标</param>
        /// <param name="width">绘制图像的宽度</param>
        /// <param name="hight">绘制图像的高度</param>
        /// <param name="img">待绘制图像</param>
        /// <param name="picture">画布</param>
        private static void Avatar(int xpos, int ypos, int width, int hight, Image img, Graphics picture)
        {
            picture.DrawImage(img, xpos, ypos, width, hight);
        }
        #endregion

        /// <summary>
        /// 生成水印
        /// </summary>
        /// <param name="oldpath"></param>
        /// <param name="newpath"></param>
        public static void MakeWater(string oldpath, string newpath)
        {
            string waterMarkType = ColoPay.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkType");
            int type = YSWL.Common.Globals.SafeInt(waterMarkType, 0);
            if (type == 0)
            {
                MakeTextWater(oldpath, newpath);
            }
            else
            {
                MakeImageWater(oldpath, newpath);
            }
        }

        /// <summary>
        /// 图片水印
        /// </summary>
        /// <param name="oldpath"></param>
        /// <param name="newpath"></param>
        public static void MakeImageWater(string oldpath, string newpath)
        {
            string ImageMarkPosition = ColoPay.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkPosition");
            if (String.IsNullOrWhiteSpace(ImageMarkPosition))
            {
                ImageMarkPosition = "WM_CENTER";
            }
            string waterMarkTransparent = ColoPay.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkTransparent");
            if (String.IsNullOrEmpty(waterMarkTransparent))
            {
                waterMarkTransparent = "30";
            }
            string waterMarkPhotoUrl = ColoPay.BLL.SysManage.ConfigSystem.GetValue("System_waterMarkPhotoUrl");
            if (String.IsNullOrEmpty(waterMarkPhotoUrl) || !File.Exists(HttpContext.Current.Server.MapPath(waterMarkPhotoUrl)))
            {
                waterMarkPhotoUrl = "/Upload/WebSiteLogo/sitelogo.png";
            }
            if (File.Exists(HttpContext.Current.Server.MapPath(waterMarkPhotoUrl)))
            {
                try
                {
                    YSWL.Common.ImageTools.addWatermarkImage(oldpath, newpath, HttpContext.Current.Server.MapPath(waterMarkPhotoUrl), ImageMarkPosition, YSWL.Common.Globals.SafeInt(waterMarkTransparent, 30));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                File.Copy(oldpath, newpath, true);
            }
        }

        /// <summary>
        /// 文字水印
        /// </summary>
        /// <param name="oldpath"></param>
        /// <param name="newpath"></param>
        /// <param name="_watermarkText"></param>
        public static void MakeTextWater(string oldpath, string newpath)
        {
            string waterMarkContent = ColoPay.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkContent");
            if (String.IsNullOrWhiteSpace(waterMarkContent))
            {
                waterMarkContent = "YSWL ";
            }
            string waterMarkFont = ColoPay.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFont");
            if (String.IsNullOrWhiteSpace(waterMarkFont))
            {
                waterMarkFont = "arial";
            }
            string waterMarkFontSize = ColoPay.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFontSize");
            if (String.IsNullOrWhiteSpace(waterMarkFontSize))
            {
                waterMarkFontSize = "14";
            }
            string waterMarkPosition = ColoPay.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkPosition");
            if (String.IsNullOrWhiteSpace(waterMarkPosition))
            {
                waterMarkPosition = "WM_CENTER";
            }
            string waterMarkFontColor = ColoPay.BLL.SysManage.ConfigSystem.GetValueByCache("System_waterMarkFontColor");
            if (String.IsNullOrWhiteSpace(waterMarkFontColor))
            {
                waterMarkFontColor = "#FFFFFF";
            }
            try
            {
                YSWL.Common.ImageTools.addWatermarkText(oldpath, newpath, waterMarkContent, waterMarkPosition, waterMarkFont, YSWL.Common.Globals.SafeInt(waterMarkFontSize, 14), waterMarkFontColor);

                //  , string _watermarkPosition = "WM_CENTER", string fontStyle = "arial", int fontSize = 14, string color = "#FFFFFF"
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}