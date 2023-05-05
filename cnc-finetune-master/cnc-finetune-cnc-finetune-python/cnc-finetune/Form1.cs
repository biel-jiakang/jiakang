using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using HslCommunication.ModBus;
using HslCommunication;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Remoting.Contexts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Sockets;

namespace cnc_finetune {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }
        object intX;
        object intY;
        CDmSoft dm = new CDmSoft();
        public CProfIniFile m_ProfINIFile;        
        IProjectFather projectFather = new function_Data(); //实例化接口类

        private void Form1_Load(object sender, EventArgs e) {
            CheckForIllegalCrossThreadCalls = false;//线程间访问
            InitUI();
        }
        void InitUI() {
         
            projectFather.Init(this);//传递主窗体
        }


        /// <summary>
        ///测试尺寸补偿
        /// </summary>
        private void button1_Click(object sender, EventArgs e) {
            Thread thread = new Thread(() => {
                CDmSoft dm = new CDmSoft();
                Thread.Sleep(2000);
                int ret = function_Data.Backmain();
                if (ret >= 0) {
                    CompensateWrite.Sizecompensation();
                }
                else {
                    int againret = function_Data.Backmain();
                    if (againret >= 0) {
                        SetMsg("返回主页面");
                        CompensateWrite.Sizecompensation();
                    }
                    else {
                        SetMsg("返回主页面出错");

                    }
                }
            });
            thread.Start();


        }
        /// <summary>
        ///获取坐标
        /// </summary>
        private void button2_Click(object sender, EventArgs e) {
            Thread thread = new Thread(() => {
                object x;
                object y;
                dm.GetCursorPos(out x, out y);
                SetMsg(x + "------" + y);
            });
            thread.Start();

        }
        /// <summary>
        ///截图
        /// </summary>
        private void button3_Click(object sender, EventArgs e) {
            Thread thread = new Thread(() => {
                string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + "screenshot.png";
                SetMsg(filename);
                CDmSoft dm = new CDmSoft();
                dm.SetPath("/");
                Thread.Sleep(5000);
                int dm_ret = dm.CapturePng(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, filename);
                // int dm_ret = dm.CapturePng(0, 0, int.Parse(X_text.Text), int.Parse(Y_text.Text), "screenshot.png");

                if (dm_ret == 0) {
                    MessageBox.Show("失败啊！");
                    return;
                }
                //string msg = String.Format("成功截图！\n已经保存在{0}\\screenshot.png", Directory.GetCurrentDirectory());
                //MessageBox.Show(msg);
                //System.Diagnostics.Process.Start("screenshot.png");
            });
            thread.Start();
        }

        /// <summary>
        ///消息打印显示
        /// </summary>
        public void SetMsg(string msg) {
           
            string str = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + msg + "\n";
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText(str); });
            CsvServer.Instance.WriteLine(GlobalVar.LogPath, str, "Log");
        }



        private void button3_Click_1(object sender, EventArgs e) {
            CDmSoft dm = new CDmSoft();
            dm.SetPath("");
            dm.SetDict(0, "cnc.txt");
            string s = dm.Ocr(924, 198, 961, 212, "00ff32-000000|ff0000-000000|000000-000000", 1.0);
            float data = float.Parse(s);
            SetMsg(data + "------" + s + "\n");
            //string Compensation = Transform();
            //SetMsg(Compensation + "\n");
            int sum = 7;
            int bit1 = sum % 2;
            int bit2 = (sum % 4) - bit1;
            int bit3 = sum % 8 - bit1 - bit2;
            SetMsg(bit1 + "/n"
              + bit2 + "/n"
              + bit3 + "/n");
            //Transform();
        }


        /// <summary>
        ///保存设置参数
        /// </summary>
        private void button1_Click_1(object sender, EventArgs e) {
            m_ProfINIFile = new CProfIniFile(GlobalVar.DataPath);
            m_ProfINIFile.Write("参数设置", "Post_Url", TCP_IP.Text);
            m_ProfINIFile.Write("参数设置", "Port", Port.Text);
            m_ProfINIFile.Write("参数设置", "PLC_IP", PLC_IP.Text);
            m_ProfINIFile.Write("参数设置", "CNCName", CNCName.Text);
        }

       
    }

}
