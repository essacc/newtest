using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;
using System.Xml;
using TCatSysManagerLib;
using System.Net;
using FluentFTP;

namespace test
{
    public partial class Form1 : Form
    {
        bool progres = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = "ftp://" + "192.168.1.12" + "/TwinCAT/3.1/";
            deletboot(path);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string basePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            if (Directory.Exists(basePath))
            {
                MessageBox.Show("根目录存在");
            }
            else
            {
                //DirectoryInfo directoryInfo = new DirectoryInfo(basePath);
                //directoryInfo.Create();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FtpClient conn = new FtpClient();
            conn.Host = "ftp://" + "192.168.1.12";
            conn.AutoConnect();

        }
        private void downloadallfiles(string ftppath,string savepath)
        {
            
        }

        public static string[] GetFolderAndFileList(string pathname, string FtpPath)
        {
            string[] getfolderandfilelist;
            FtpWebRequest request;
            StringBuilder sb = new StringBuilder();
            try
            {
                string uri = FtpPath + "/" + pathname;
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.UseBinary = true;
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    sb.Append(line);
                    sb.Append("\n");
                    line = reader.ReadLine();
                }
                sb.Remove(sb.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return sb.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取FTP上面的文件夹和文件：" + ex.Message);
                getfolderandfilelist = null;
                return getfolderandfilelist;
            }
        }
        private void deletboot(string path)
        {
            string basePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            string localpath = basePath + "/data/Boot";
            if (Directory.Exists(localpath))
            {

            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(localpath);
                directoryInfo.Create();
            }
            string[] n1 = GetFolderAndFileList("Bootold", path);
            if (n1 != null)
            {
                foreach (string name1 in n1)
                {
                    if(name1.Contains("."))
                    {
                        FileDownLoad(name1, path, localpath);
                    }
                    else
                    {
                        if (Directory.Exists(localpath + "/" + name1+"/"))
                        {

                        }
                        else
                        {
                            DirectoryInfo directoryInfo = new DirectoryInfo(localpath + "/" + name1+"/");
                            directoryInfo.Create();
                        }
                        string[] n2 = GetFolderAndFileList(name1, path + "/Bootold/");
                        if(n2!=null)
                        {
                            foreach(string name2 in n2)
                            {
                                if(name2.Contains("."))
                                {
                                    FileDownLoad(name2, path + "/" + name1 + "/", localpath + "/" + name1 + "/");
                                }
                                else
                                {
                                    if (Directory.Exists(localpath + "/" + name1 + "/"+name2+"/"))
                                    {

                                    }
                                    else
                                    {
                                        DirectoryInfo directoryInfo = new DirectoryInfo(localpath + "/" + name1 + "/"+name2+"/");
                                        directoryInfo.Create();
                                    }
                                    string[] n3 = GetFolderAndFileList(name2, path + "/Bootold/" + name1 + "/");
                                    if(n3!=null)
                                    {
                                        foreach (string name3 in n3)
                                        {
                                            if (name3.Contains("."))
                                            {
                                                FileDownLoad(name3, path + "/" + name1 + "/", localpath + "/" + name1 + "/"+name2+"/");
                                            }
                                            else
                                            {
                                                if (Directory.Exists(localpath + "/" + name1 + "/" + name2 + "/"+name3+"/"))
                                                {

                                                }
                                                else
                                                {
                                                    DirectoryInfo directoryInfo = new DirectoryInfo(localpath + "/" + name1 + "/" + name2 + "/" + name3 + "/");
                                                    directoryInfo.Create();
                                                }
                                                string[] n4 = GetFolderAndFileList(name3, path + "/Bootold/" + name1 + "/"+name2+"/");
                                                if (n4 != null)
                                                {
                                                    foreach (string name4 in n4)
                                                    {
                                                        if (name4.Contains("."))
                                                        {
                                                            FileDownLoad(name4, path + "/" + name1 + "/", localpath + "/" + name1 + "/" + name2 + "/" + name3 + "/");

                                                        }
                                                    }
                                                }

                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }

                }
            }






















                //if (n1 != null)
                //{
                //    foreach (string name1 in n1)
                //    {
                //        string path2 = path + "/Bootold/";

                //        if (DeleteFile(name1, path2))
                //        {

                //        }
                //        else
                //        {
                //            string[] n2 = GetFolderAndFileList(name1, path2);
                //            if (n2 != null)
                //            {
                //                foreach (string name2 in n2)
                //                {

                //                    if (DeleteFile(name2, path2 + name1))
                //                    {

                //                    }
                //                    else
                //                    {
                //                        string[] n3 = GetFolderAndFileList(name2, path2 + name1);
                //                        foreach (string name3 in n3)
                //                        {
                //                            if (DeleteFile(name3, path2 + name1 + "/" + name2))
                //                            {

                //                            }
                //                            else
                //                            {
                //                                string[] n4 = GetFolderAndFileList(name3, path2 + name1 + "/" + name2);
                //                                if (n4 != null)
                //                                {
                //                                    foreach (string name4 in n4)
                //                                    {
                //                                        DeleteFile(name4, path2 + name1 + "/" + name2 + "/" + name3);
                //                                    }
                //                                }
                //                            }
                //                            DeletePath("/" + name3, path2 + name1 + "/" + name2);
                //                        }
                //                    }
                //                    DeletePath("/" + name2, path2 + name1);

                //                }

                //            }
                //            DeletePath("/" + name1, path2);
                //        }
                //        DeletePath("/Bootold/", path);
                //    }
                //}
            }

        public static bool DeleteFile(string fileName, string FtpPath)
        {
            try
            {
                FileInfo fileInf = new FileInfo(fileName);

                string uri = Path.Combine(FtpPath, fileInf.Name);

                //string uri = FtpPath;

                // 根据uri创建FtpWebRequest对象
                FtpWebRequest reqFTP = (FtpWebRequest)WebRequest.Create(new Uri(uri));

                // 指定数据传输类型
                reqFTP.UseBinary = true;

                // ftp用户名和密码                

                // 默认为true，连接不会被关闭

                // 在一个命令之后被执行

                reqFTP.KeepAlive = false;

                // 指定执行什么命令

                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                response.Close();

                //Successinfo

                return true;
            }

            catch (Exception ex)
            {
                //ErrorInfo 
                return false;
            }

        }

        public static void DeletePath(string pathname, string FtpPath)
        {
            try
            {
                string uri = FtpPath + pathname;
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP删除目录：" + ex.Message);
            }
        }

        public void FileDownLoad(string fileName,string path, string localpath)        //ftp下载文件
        {
            FtpWebRequest request;
            try
            {
                FileStream fs = new FileStream(localpath +"/"+ fileName, FileMode.Create);
                request = (FtpWebRequest)FtpWebRequest.Create(new Uri(path + "/" + fileName));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.UseBinary = true;
                request.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                int bufferLength = 5120;
                int i;
                byte[] buffer = new byte[bufferLength];
                i = stream.Read(buffer, 0, bufferLength);
                while (i > 0)
                {
                    fs.Write(buffer, 0, i);
                    i = stream.Read(buffer, 0, bufferLength);
                }
                stream.Close();
                fs.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("FTP下载文件：" + ex.Message);
            }
        }

    }
}