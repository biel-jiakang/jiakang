
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cnc_finetune

{
    public class CsvServer
    {
        private Thread _thread;
        private ConcurrentQueue<CsvInfo> queue = new ConcurrentQueue<CsvInfo>();
        private readonly static CsvServer instance = new CsvServer();
        public object obj = new object();
        CsvServer() { Start(); }
        public static CsvServer Instance
        {
            get { return instance; }
        }

        public void Start()
        {
            Stop();
            _thread = new Thread(new ThreadStart(ProcessEventQueue));
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void Stop()
        {
            if (this._thread != null)
            {
                this._thread.Abort();
            }
        }
        private void Kill()//20170327 XSF
        {
            Process[] process = Process.GetProcesses();
            foreach (Process p in process)
            {
                if (p.ProcessName.ToUpper() == "ET")
                {
                    p.CloseMainWindow();
                    p.WaitForExit();
                }
            }
        }
        private void ProcessEventQueue()
        {
            while (true)
            {
                if (queue.Count > 0)
                {
                    CsvInfo csvInfo;
                    queue.TryDequeue(out csvInfo);
                    try
                    {
                        lock (obj)
                        {
                            //Kill();//20170327 XSF
                            //StreamWriter sw = File.AppendText(csvInfo.Path);
                            FileInfo fi = new FileInfo(csvInfo.Path);
                            if (!fi.Directory.Exists)
                            {
                                fi.Directory.Create();
                            }
                            FileStream fs = new FileStream(csvInfo.Path, FileMode.Append, FileAccess.Write);
                            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                            sw.WriteLine(csvInfo.Line);
                            sw.Dispose();
                            fs.Dispose();
                        }
                    }
                    catch
                    {
                        queue.Enqueue(csvInfo);
                    }
                }
                Thread.Sleep(20);
            }
        }

        public void WriteLine(string path, string line, string filename)
        {
            CsvInfo csvInfo = new CsvInfo();
            int curr_time = DateTime.Now.Hour;
            int curr_date = DateTime.Now.Day;
            if (filename == "Data")
            {

                if (curr_time > 7 & curr_time < 20)
                {
                    csvInfo.Path = path + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\\" + "白班.csv";
                }
                else
                {
                    csvInfo.Path = path + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\\" + "夜班.csv";
                }
            }
            else
            {
                if (curr_time > 7 & curr_time < 20)
                {
                    csvInfo.Path = path + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\\" + "白班.txt";
                }
                else
                {
                    csvInfo.Path = path + DateTime.Now.Date.ToString("yyyy-MM-dd") + "\\" + "夜班.txt";
                }
            }

            csvInfo.Line = line;
            queue.Enqueue(csvInfo);
        }

        /// <summary>
        /// 定期清除文件
        /// </summary>
        /// <param name="fileDirect">文件夹</param>
        /// <param name="postFix">文件后缀</param>
        /// <param name="saveDay">保存天数</param>
        private void DeleteFile(string fileDirect, string postFix, int saveDay)
        {
            DateTime nowtime = DateTime.Now; //获取当前时间
            string[] files = Directory.GetFiles(fileDirect, postFix, SearchOption.AllDirectories);  //获取该目录下所有 .txt文件
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                TimeSpan t = nowtime - fileInfo.CreationTime;  //当前时间  减去 文件创建时间
                int day = t.Days;
                if (day > saveDay)   //保存的时间 ；  单位：天
                {
                    File.Delete(file);  //删除超过时间的文件
                }
            }
        }
        //定期清理文件夹
        public void DeleteFileInfo(string path, int saveDays)
        {
            //int saveDays = 3; //保存天数，可根据实际需求定义
            //string path = "D:\\Test"; //保存数据的路径
            var saveFileFolderList = new List<string>(); //保存哪些文件夹
            for (int i = 0; i < saveDays; i++)
            {
                var saveFile = "Log" + DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");
                saveFileFolderList.Add(saveFile);
                saveFile = "Data" + DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");
                saveFileFolderList.Add(saveFile);
            }
            //开启一个异步线程定期检查是否可以删除文件

            Thread dataThread = new Thread(delegate ()
            {
                while (true)
                {
                    if (Directory.Exists(path))
                    {
                        DirectoryInfo di = new DirectoryInfo(path);
                        var fsiArr = di.GetFileSystemInfos(); //获取所有的文件、文件夹

                        foreach (var fsi in fsiArr)
                        {
                            if (fsi is DirectoryInfo) //判断是否文件夹
                            {
                                //判断是否在删除范畴内
                                if (!saveFileFolderList.Exists(o => fsi.Name.Contains(o)))
                                {
                                    DirectoryInfo delDi = new DirectoryInfo(fsi.FullName);
                                    delDi.Delete(true); //删除文件夹及文件
                                }
                            }
                        }
                    }
                    Thread.Sleep(1000 * 60 * 60 * 12); //每隔12H检查一遍
                }
            });
        }
    }

    public class CsvInfo
    {
        public string Path { get; set; }
        public string Line { get; set; }
    }
}
