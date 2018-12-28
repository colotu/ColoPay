using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;

namespace YSWL.Web
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }


        protected void butSave_Click(object sender, EventArgs e)
        {
            string oldFolder = txtoldFolder.Text.Trim();
            string newFolder = txtnewFolder.Text.Trim();
            string targetFolder = txttargetFolder.Text.Trim();
            GenerateUpgradePackage(oldFolder, newFolder, targetFolder);
            Label1.Text = "执行完成";
        }

        #region 生成升级包
        /// <summary>
        /// 生成升级包
        /// </summary>
        /// <param name="oldFolder">旧文件夹</param>
        /// <param name="newFolder">新文件夹</param>
        /// <param name="targetFolder">目标文件夹</param>
        public static void GenerateUpgradePackage(string oldFolder, string newFolder, string targetFolder)
        {
            DirectoryInfo newdir = new DirectoryInfo(newFolder);
            FindDire(newdir, oldFolder, newFolder, targetFolder);
        }
        /// <summary>
        /// 递归查找所有目录及文件
        /// </summary>
        /// <param name="dir"></param>
        private static void FindDire(DirectoryInfo newdir, string oldFolder, string newFolder, string targetFolder)
        {
            //遍历一个目录下的全部目录
            foreach (DirectoryInfo dChild in newdir.GetDirectories("*"))
            {
                FindDire(dChild, oldFolder, newFolder, targetFolder);
            }
            FindFile(newdir, oldFolder, newFolder, targetFolder);
        }
        /// <summary>
        /// 查找所有文件
        /// </summary>
        /// <param name="dir"></param>
        private static void FindFile(DirectoryInfo dir, string oldFolder, string newFolder, string targetFolder)
        {
            string ddd = oldFolder;
            string oldFile;
            string newFile;
            string targetFile;
            string targetPath;
            //遍历一个目录下的全部文件
            foreach (FileInfo dChild in dir.GetFiles("*"))
            {
                oldFile = dChild.FullName.Replace(newFolder, oldFolder);
                newFile = dChild.FullName;
                if (!File.Exists(oldFile) || !CompareFile(oldFile, newFile))
                {
                    targetPath = dChild.DirectoryName.Replace(newFolder, targetFolder);
                    targetFile = dChild.FullName.Replace(newFolder, targetFolder);
                    if (!Directory.Exists(targetPath))
                    {
                        Directory.CreateDirectory(targetPath);
                    }
                    File.Copy(newFile, targetFile, true);
                }
            }
        }

        ///// <summary>
        ///// 比较两个文件是否完全相等  (哈希值)
        ///// </summary>
        //private static bool CompareFile(string p_1, string p_2)
        //{
        //    //计算第一个文件的哈希值
        //    var hash = System.Security.Cryptography.HashAlgorithm.Create();
        //    var stream_1 = new System.IO.FileStream(p_1, System.IO.FileMode.Open);
        //    byte[] hashByte_1 = hash.ComputeHash(stream_1);
        //    stream_1.Close();
        //    //计算第二个文件的哈希值
        //    var stream_2 = new System.IO.FileStream(p_2, System.IO.FileMode.Open);
        //    byte[] hashByte_2 = hash.ComputeHash(stream_2);
        //    stream_2.Close();

        //    //比较两个哈希值
        //    return BitConverter.ToString(hashByte_1) == BitConverter.ToString(hashByte_2) ? true : false;
        //}

        /// <summary>
        /// 比较两个文件是否完全相等(字节)
        /// 如果两个文件的内容完全相同，将返回 True;有任何差异将返回False。
        /// </summary>
        private static bool CompareFile(string file1, string file2)
        {
            // 判断是否是同一个地址
            if (file1 == file2) { return true; }
            int file1byte = 0;
            int file2byte = 0;
            FileStream fs2 = new FileStream(file2, FileMode.Open);
            using (FileStream fs1 = new FileStream(file1, FileMode.Open))
            {
                // 检查文件大小。如果两个文件的大小并不相同，则视为不相同。
                if (fs1.Length != fs2.Length)
                {
                    fs1.Close();
                    fs2.Close();
                    return false;
                }
                // 逐一比较两个文件的每一个字节，直到发现不相符或已到达文件尾端为止。
                do
                {
                    // 从每一个文件读取一个字节。
                    file1byte = fs1.ReadByte();
                    file2byte = fs2.ReadByte();
                } while ((file1byte == file2byte) && (file1byte != -1));
                // 关闭文件。
                fs1.Close();
                fs2.Close();
            }
            //  返回比较的结果。在这个时候，只有当两个文件的内容完全相同时，"file1byte" 才会等于 "file2byte".
            return (file1byte == file2byte);
        }

        #endregion


 


    }


}