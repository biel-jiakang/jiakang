using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cnc_finetune {
    public class GlobalVar {
        public static bool IsAutoRunning = false;//自动运行标志
        public static bool m_Tcp;
        //public static bool contain = false; //登录结果
        public static bool contain = true; //登录结果
        public static string m_getresultdata;//Biel服务器Get请求返回数据
        public static bool getTraceJson;//Trace服务器Get请求返回Json数据
        public static string getJson;//Post请求返回Json数据
        public static string getJsonToBiel;
        public static string getNgResult;//Get请求错误响应状态返回结果
        public static string postNgResult;//Post请求错误响应状态返回结果
        public static string httpGetErr;
        public static string httpPostErr;
        public static string CNCName;
        public static string Post_Url;
        public static string PLC_IP;
        public static string Local_IP;
        public static string Processingprogress;
        public static string Processingstatus;
        public static string ProcessingNo;
        public static string Processingquantity;
        public static int PLCstatus;
        public static int PLCquantity;
        public static int PLClack;
        public static readonly string Path = Application.StartupPath;
        public static readonly string ExePath = Path + "//SBU_CNC.exe";
        public static readonly string LogPath = Path + "//Log/";
        public static readonly string DataPath = Path + "//Data/ParamSetting.ini";
        public static readonly string WordPath = Path + "//Data/";
        // public static int Item { get => Item1; set => Item1 = value; }
    }
    public class CNCModel {
        public string cnc_ip { get; set; }
        public string plc_ip { get; set; }
        public string cnc_state { get; set; }
        public int plc_state { get; set; }
        //public string ProcessingNo { get; set; }
        //public string ProcessingQuantity { get; set; }
        //public int PLCStatus { get; set; }
        //public int PLCQuantity { get; set; }
        //public int PLCLack { get; set; }
    }


    public class Rootobject {
        public M209_1 M209_1 { get; set; }
        public M209_2 M209_2 { get; set; }
    }

    public class M209_1 {
        public float Rx1 { get; set; }
        public float Ry1 { get; set; }
        public float D4 { get; set; }
        public float D5 { get; set; }
        public float D6 { get; set; }
    }

    public class M209_2 {
        public float Rx2 { get; set; }
        public float Ry2 { get; set; }
        public float D5 { get; set; }
    }






}
