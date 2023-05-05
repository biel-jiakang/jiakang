using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cnc_finetune
{
    //项目接口父类，包含子类的5个方法
    interface IProjectFather
    {
        void InitUI();
        //void Start();
        //void Stop();
        void Init(Form1 frm);
    }
}
