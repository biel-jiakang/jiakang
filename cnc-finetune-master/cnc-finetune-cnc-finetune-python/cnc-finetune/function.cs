using HslCommunication.ModBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using HslCommunication;

namespace cnc_finetune
{
    public class function_Data : IProjectFather
    {
        object intX;
        object intY;
        System.Timers.Timer t = new System.Timers.Timer(10000);
        List<double> nList = new List<double>();
        private ModbusTcpNet masterPLC = null;
        CDmSoft dm = new CDmSoft();
        private AsyncTcpClient CNCtcpClient;
        string Data = null;
        string Logdata = null;
        public CProfIniFile m_ProfINIFile;
        private OperateResult<ushort[]> xList = new OperateResult<ushort[]>();
        Form1 frMain;
        public void Init(Form1 frm)
        {
            frMain = frm;//窗口实例化
            InitUI();
        }
        //初始化界面
        public void InitUI() { //PLC_IP.Text = "127.0.0.1";
            Renew();
            t.Elapsed += new System.Timers.ElapsedEventHandler(Execute);//到达时间的时候执行事件；
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；               
            t.Start();
            btnListen();
        }

        /// <summary>
        ///定时触发扫描
        /// </summary>
        public void Execute(object source, System.Timers.ElapsedEventArgs e) {

            //初始化modbusmaster
            masterPLC = new ModbusTcpNet(frMain.PLC_IP.Text, 502, 1);
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            //t.Start(); //执行完毕后再开启器
            //t.Interval = sum * 1000;
            //    string postdata= JsonConvert.SerializeObject(需要转换的json对象);
            //    HttpUitls.Post(Post_url.Text, balance);
            UpdatecncStatus();
            UpdatePlcStatus();
            Data = Transform();
            SetMsg(Logdata);
            if (CNCtcpClient.Connected && Data != null) {

                CNCtcpClient.Send(Data);
            }
        }
        /// <summary>
        ///连接服务端
        /// </summary>
        private void btnListen() {
            Thread thread = new Thread(() => {
                try {
  
                    CNCtcpClient = new AsyncTcpClient(IPAddress.Parse(frMain.TCP_IP.Text), 1111);
                    CNCtcpClient.Connect();
                    CNCtcpClient.PlaintextReceived += CNCClient_PlaintextReceived;

                }

                catch (InvalidProgramException ex) {
                    SetMsg("【提示消息】：TCP服务器连接失败，" + ex.Message);
                }

                catch (Exception ex) {
                    SetMsg("【提示消息】：TCP服务器连接失败，" + ex.Message);
                }

            });
            thread.Start();
        }
        /// <summary>
        ///监听服务端数据
        /// </summary>
        private void CNCClient_PlaintextReceived(object sender, TcpDatagramReceivedEventArgs<string> e) {
            if (CNCtcpClient.Connected) {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                if (e.Datagram != "" && e.Datagram != null) {
                    List<string> list = new List<string>(e.Datagram.Split(','));
                    string rec = e.Datagram;
                    Rootobject x = JsonConvert.DeserializeObject<Rootobject>(rec);

                    SetMsg("D4:" + x.M209_1.D4 + "；D5:" + x.M209_1.D5 + "；D6:" + x.M209_1.D6);
                    //if (list[0] == "1") {
                    //    if (list[1] != "0") {
                    //    Thread thread = new Thread(() => {
                    //        //CDmSoft dm = new CDmSoft();
                    //        Thread.Sleep(2000);
                    //        int ret = Backmain();
                    //        if (ret >= 0) {
                    //            CompensateWrite .Scalecompensation();
                    //        }
                    //        else {
                    //            int againret = Backmain();
                    //            if (againret >= 0) {
                    //                SetMsg("返回主页面");
                    //                CompensateWrite .Scalecompensation();
                    //            }
                    //            else {
                    //                SetMsg("返回主页面出错");
                    //            }
                    //        }
                    //    });
                    //}
                    //if (list[2] !="0") {
                    //    CompensateWrite .Sizecompensation();
                    //}
                    //    }
                }
            }
        }
        /// <summary>
        ///更新cnc机台数据
        /// </summary>
        private void UpdatecncStatus() {
            Thread thread = new Thread(() => {
                //CDmSoft dm = new CDmSoft();
                dm.SetPath(GlobalVar.WordPath);
                dm.SetDict(0, "CNCword.txt");
                GlobalVar.Processingstatus = dm.Ocr(328, 573, 389, 589, "00ff32-000000|0000ff-000000", 1.0);
                GlobalVar.ProcessingNo = dm.Ocr(329, 481, 353, 496, "00ff32-000000|0000ff-000000", 1.0);
                GlobalVar.Processingprogress = dm.Ocr(376, 482, 391, 495, "00ff32-000000|0000ff-000000", 1.0);
                GlobalVar.Processingquantity = dm.Ocr(516, 503, 538, 520, "00ff32-000000|0000ff-000000", 1.0);
                //GlobalVar.Processingstatus = dm.Ocr(377, 574, 440, 596, "00ff32-000000|0000ff-000000", 1.0);
                //GlobalVar.ProcessingNo = dm.Ocr(385, 480, 420, 498, "00ff32-000000|0000ff-000000", 1.0);
                //GlobalVar.Processingprogress = dm.Ocr(435, 482, 459, 496, "00ff32-000000|0000ff-000000", 1.0);
                //GlobalVar.Processingquantity = dm.Ocr(573, 500, 614, 519, "00ff32-000000|0000ff-000000", 1.0);

                Logdata = "CNC工作状态：" + GlobalVar.Processingstatus + ";CNC路径序号：" + GlobalVar.ProcessingNo + ";CNC加工进度：" + GlobalVar.Processingprogress + ";工件计数：" + GlobalVar.Processingquantity;
                //SetMsg(data);
                //frMain.ptb_switch.Image = bStatus ? Resources.green : Resources.red;

            });
            thread.Start();
        }
        /// <summary>
        ///更新PLC连接状态数据
        /// </summary>

        void UpdatePlcStatus() {
            Thread thread = new Thread(() => {
                try {
                    masterPLC = new ModbusTcpNet(frMain.PLC_IP.Text, 502, 1);
                    OperateResult connect = masterPLC.ConnectServer();
                    if (connect.IsSuccess) {
                        xList = masterPLC.ReadUInt16("0", 10);
                        GlobalVar.PLCstatus = xList.Content[2];
                        GlobalVar.PLCquantity = masterPLC.ReadInt32("3", 1).Content[0];
                        GlobalVar.PLClack = xList.Content[1];
                        //SetMsg(GlobalVar.PLCstatus + "," + GlobalVar.PLCquantity + "," + GlobalVar.PLClack );
                        Logdata += "," + GlobalVar.PLCstatus + "," + GlobalVar.PLCquantity + "," + GlobalVar.PLClack;
                    }
                    else {
                        SetMsg("PLC连接失败，请检查ModBusTcp服务器是否开启");
                    }
                }
                catch (Exception ex) {
                    //UpdatePlcStatus(false);
                    SetMsg("PLC连接失败，请检查ModBusTcp服务器是否开启" + ex.Message + "\r\n");
                }
            });
            thread.Start();
        }
        /// <summary>
        ///输入框数据补偿运算
        /// </summary>
        private void Copyandpaste(float Compensationvalue) {
            CDmSoft dm = new CDmSoft();
            Thread.Sleep(6000);
            //dm.LeftClick();
            dm.LeftDoubleClick();
            dm.KeyDownChar("ctrl");
            dm.KeyDownChar("c");
            Thread.Sleep(1000);
            dm.KeyUpChar("c");
            dm.KeyUpChar("ctrl");
            Thread.Sleep(1000);

            float Initialvalue = Convert.ToSingle(dm.GetClipboard()) - Compensationvalue;
            string Result = Initialvalue.ToString();
            Clipboard.SetText(Result);
            Thread.Sleep(1000);
            dm.KeyDownChar("ctrl");
            dm.KeyDownChar("v");
            Thread.Sleep(100);
            dm.KeyUpChar("v");
            dm.KeyUpChar("ctrl");
        }
        public static int Backmain() {
            object intX;
            object intY;
            CDmSoft dm = new CDmSoft();
            dm.KeyPressChar("esc");
            Thread.Sleep(1000);
            int dm_ret = dm.FindPic(298, 93, 1068, 972, "findmain.bmp", "000000", 0.9, 0, out intX, out intY);
            return dm_ret;
        }
        //添加消息到log列表
        void AddToLsbLog(string str) {
            CsvServer.Instance.WriteLine(GlobalVar.LogPath, str, "Log");
        }


        /// <summary>
        ///保存参数设置
        /// </summary>
        private string Transform() {
            CNCModel model = new CNCModel();
            model.cnc_ip = GlobalVar.Local_IP;
            model.plc_ip = GlobalVar.PLC_IP;
            model.cnc_state = GlobalVar.Processingstatus;
            model.plc_state = GlobalVar.PLCstatus;
            //序列化
            string Senddate = JsonConvert.SerializeObject(model);
            //string PostUrl = Post_url.Text;
            //SetMsg(Senddate);
            //string date = Post(PostUrl, Postdate);
            return Senddate;
        }

      
        /// <summary>
        ///更新参数设置
        /// </summary>
        private void Renew() {
            m_ProfINIFile = new CProfIniFile(GlobalVar.DataPath);
            frMain.TCP_IP.Text = m_ProfINIFile.Read("参数设置", "TCP_IP", "");
            frMain.Port.Text = m_ProfINIFile.Read("参数设置", "Port", "");
            frMain.PLC_IP.Text = m_ProfINIFile.Read("参数设置", "PLC_IP", "");
            frMain.CNCName.Text = m_ProfINIFile.Read("参数设置", " CNCName", ""); IPAddress[] ip = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress address in ip) {
                if (address.AddressFamily == AddressFamily.InterNetwork) {
                    GlobalVar.Local_IP = address.ToString();
                    SetMsg(GlobalVar.Local_IP);
                }
            }
        }
        public void SetMsg(string msg) {
           
                frMain.BeginInvoke(new Action(() => {
                //richTextBox1.Invoke(new Action(() => { richTextBox1.AppendText(msg); }));
                string str = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + msg + "\n";
                frMain.richTextBox1.Invoke((MethodInvoker)delegate { frMain.richTextBox1.AppendText(str); });
                CsvServer.Instance.WriteLine(GlobalVar.LogPath, str, "Log");
            }));
        }
       
    }




}
