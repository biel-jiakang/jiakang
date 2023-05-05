using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace cnc_finetune {
    public class CompensateWrite {

        public void Quitmain() {
            CDmSoft dm = new CDmSoft();
            dm.KeyDownChar("ctrl");
                dm.KeyDownChar("f3");//退出主页面
                Thread.Sleep(2000);
                dm.KeyUpChar("ctrl");
                dm.KeyUpChar("f5");//确定退出
                Thread.Sleep(3000);
                dm.KeyPressChar("f3");
                Thread.Sleep(500);
                dm.KeyPressChar("f2");
                Thread.Sleep(500);
                dm.KeyPressChar("esc");
                Thread.Sleep(500);
                dm.KeyPressChar("f3");
                Thread.Sleep(500);
                dm.KeyPressChar("f7");
                Thread.Sleep(500);
                dm.KeyPressChar("tab");  //选择
                Thread.Sleep(500);
                dm.KeyPressChar("f5");
                Thread.Sleep(500);
                dm.KeyPressChar("esc");
                Thread.Sleep(500);
                dm.KeyPressChar("f4");
                Thread.Sleep(500);
                dm.KeyPressChar("f2");
                Thread.Sleep(500);
                dm.KeyUpChar("ctrl");
                dm.KeyUpChar("f4");
                Thread.Sleep(500);
                //填入缩放比例
                dm.KeyPressChar("f9");//取消等比列
                Thread.Sleep(500);
                dm.KeyPressChar("enter");
            
        }

        public int Backmain() {
            object intX;
            object intY;
            CDmSoft dm = new CDmSoft();
            dm.KeyPressChar("esc");
            Thread.Sleep(1000);
            int dm_ret = dm.FindPic(298, 93, 1068, 972, "findmain.bmp", "000000", 0.9, 0, out intX, out intY);
            return dm_ret;
        }
        public static void Sizecompensation() {
            CDmSoft dm = new CDmSoft();
            dm.KeyDownChar("ctrl");
            dm.KeyDownChar("f6");
            Thread.Sleep(2000);
            dm.KeyUpChar("ctrl");
            dm.KeyUpChar("f6");
            Thread.Sleep(3000);
            dm.KeyPressChar("f10");
            Thread.Sleep(3000);
            //dm.MoveTo(int.Parse(X_text.Text), int.Parse(Y_text.Text));
            Thread.Sleep(3000);

            //Copyandpaste(0.1f);

            //Copyandpaste(0.1f);

            Thread.Sleep(3000);
            dm.KeyPressChar("tab");
            Thread.Sleep(3000);
            dm.KeyPressChar("tab");
            Thread.Sleep(3000);
            dm.KeyPressChar("tab");
            Thread.Sleep(3000);
            dm.KeyPressChar("enter");
            Thread.Sleep(3000);
            dm.KeyPressChar("enter");
            Thread.Sleep(3000);
            dm.KeyPressChar("esc");
        }


    //缩放比例补偿
    public static void Scalecompensation() {
            CDmSoft dm = new CDmSoft();
            dm.KeyDownChar("ctrl");
        dm.KeyDownChar("f3");//退出主页面
        Thread.Sleep(2000);
        dm.KeyUpChar("ctrl");
        dm.KeyUpChar("f5");//确定退出
        Thread.Sleep(3000);
        dm.KeyPressChar("f3");
        Thread.Sleep(500);
        dm.KeyPressChar("f2");
        Thread.Sleep(500);
        dm.KeyPressChar("esc");
        Thread.Sleep(500);
        dm.KeyPressChar("f3");
        Thread.Sleep(500);
        dm.KeyPressChar("f7");
        Thread.Sleep(500);
        dm.KeyPressChar("tab");  //选择
        Thread.Sleep(500);
        dm.KeyPressChar("f5");
        Thread.Sleep(500);
        dm.KeyPressChar("esc");
        Thread.Sleep(500);
        dm.KeyPressChar("f4");
        Thread.Sleep(500);
        dm.KeyPressChar("f2");
        Thread.Sleep(500);
        dm.KeyUpChar("ctrl");
        dm.KeyUpChar("f4");
        Thread.Sleep(500);
        //填入缩放比例
        dm.KeyPressChar("f9");//取消等比列
        Thread.Sleep(500);
        dm.KeyPressChar("enter");

    }
}
}
