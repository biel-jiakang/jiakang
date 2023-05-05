using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace cnc_finetune {
    internal static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            bool ret;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, Application.ProductName, out ret);
            if (ret)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                MessageBox.Show(null, "程序已经在运行，请不要同时运行多个本程序。\n\n这个程序即将退出。", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();//退出程序   
            }
        }
    }
}
