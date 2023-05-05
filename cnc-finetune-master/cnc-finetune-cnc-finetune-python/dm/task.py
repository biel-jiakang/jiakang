import time
from configparser import ConfigParser
config = ConfigParser()
config.read('cncset.ini')

def read_cncstatus(Dm):
    ''' 获取机台信息 '''
    Processtatus = Dm.ocr(int(config['Processtatus']['x1']), 
                          int(config['Processtatus']['y1']), 
                          int(config['Processtatus']['x2']), 
                          int(config['Processtatus']['y2']), 
                          "000000-000000|0000ff-000000", 
                          1.0)
    
    ProcessingNo = Dm.ocr(int(config['ProcessingNo']['x1']), 
                          int(config['ProcessingNo']['y1']), 
                          int(config['ProcessingNo']['x2']), 
                          int(config['ProcessingNo']['y2']), 
                          "000000-000000|0000ff-000000", 
                          1.0)
    
    Processingprogress = Dm.ocr(int(config['Processingprogress']['x1']), 
                                int(config['Processingprogress']['y1']), 
                                int(config['Processingprogress']['x2']), 
                                int(config['Processingprogress']['y2']), 
                                "000000-000000|0000ff-000000", 
                                1.0)
    
    Processingquantity = Dm.ocr(int(config['Processingquantity']['x1']), 
                                int(config['Processingquantity']['y1']), 
                                int(config['Processingquantity']['x2']), 
                                int(config['Processingquantity']['y2']), 
                                "000000-000000|0000ff-000000", 
                                1.0)
    return (Processtatus, ProcessingNo, Processingprogress, Processingquantity)

def back_main(dm):
    ''' 返回到主页面'''
    time.sleep(1)
    dm.Key_PressChar("esc")
    time.sleep(0.1)
    dm.Key_PressChar("esc")
    time.sleep(0.1)
    intX, intY  = dm.Find_Pic(int(config['back_main']['x1']), 
                             int(config['back_main']['y1']), 
                             int(config['back_main']['x2']), 
                             int(config['back_main']['y2']), 
                             "findmain.bmp", 
                             "000000", 
                             0.9, 
                             0)
    return intX, intY 

def quit_main(dm):
    ''' 退出主页面 '''
    time.sleep(1)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("f3")
    time.sleep(0.1)
    dm.Key_PressChar("f5")
    dm.Key_UpChar("ctrl")
    time.sleep(0.1)
    dm.Key_PressChar("esc")
    time.sleep(0.1)
    dm.Key_PressChar("f3")
    time.sleep(0.1)
    dm.Key_PressChar("f2")
    time.sleep(0.1)
    dm.Key_PressChar("esc")

def size_compensation(dm, D6, D7):
    ''' 尺寸补偿 (不用进到缩放界面，在软件内操作) 凹槽长 '''
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("f6")
    time.sleep(0.2)
    dm.Key_UpChar("ctrl")
    time.sleep(0.1)
    dm.Key_PressChar("f10")
    time.sleep(0.1)
    dm.Move_To(int(config['size_compensation']['x']), 
               int(config['size_compensation']['y']))
    time.sleep(0.50)
    dm.Left_DoubleClick()
    time.sleep(0.3)
    dm.Key_PressChar("tab")
    time.sleep(0.3)
    if D6 != 0:
        copyandpaste(dm, D6)
    time.sleep(0.1)
    dm.Key_PressChar("tab")
    if D7 != 0:
        copyandpaste(dm, D7)
    time.sleep(0.1)
    dm.Key_PressChar("enter")
    time.sleep(0.1)
    dm.Key_PressChar("enter")
    time.sleep(0.1)
    dm.Key_PressChar("esc")

def copyandpaste(dm, data):
    ''' 复制粘贴 '''
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("c")
    time.sleep(0.050)
    dm.Key_UpChar("ctrl")
    time.sleep(0.100)
    Initialvalue = float(dm.Get_Clipboard()) + data
    Result = str(Initialvalue)
    dm.Set_Clipboard(Result)
    time.sleep(0.50)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("v")
    time.sleep(0.20)
    dm.Key_UpChar("ctrl")
      
def SizeRx(dm, Rx1, Ry1):
    ''' 缩放 Rx '''
    dm.Key_PressChar("esc")
    time.sleep(1)
    dm.Key_PressChar("f3")
    time.sleep(0.50)
    dm.Key_PressChar("f2")
    time.sleep(0.50)
    dm.Key_PressChar("f3")
    time.sleep(0.50)
    dm.Key_PressChar("f7")
    time.sleep(0.50)
    dm.Key_PressChar("down");  
    time.sleep(0.50)
    dm.Key_PressChar("f5")
    time.sleep(0.500)
    dm.Key_PressChar("esc")
    time.sleep(0.500)
    dm.Key_PressChar("f4")
    time.sleep(0.500)
    dm.Key_PressChar("f2")
    time.sleep(0.50)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("f4")
    time.sleep(0.50)
    dm.Key_UpChar("ctrl")
    time.sleep(0.500)
    dm.Key_PressChar("f9")
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.1)
    if Rx1 != 0:
        Scalecalculatex(dm,Rx1)
    time.sleep(1)
    dm.Key_PressChar("tab")
    if Ry1 != 0:
        Scalecalculatey(dm,Ry1)
    time.sleep(1)
    dm.Key_PressChar("enter")
    time.sleep(0.50)
    dm.Key_PressChar("esc")

def SizeRy(dm, Rx2, Ry2):
    ''' 缩放 Ry'''
    dm.Key_PressChar("esc")
    time.sleep(1)
    dm.Key_PressChar("f3")
    time.sleep(0.50)
    dm.Key_PressChar("f2")
    time.sleep(0.50)
    dm.Key_PressChar("f3")
    time.sleep(0.50)
    dm.Key_PressChar("f7")
    time.sleep(0.50)
    dm.Key_PressChar("down")
    time.sleep(0.50)
    dm.Key_PressChar("down")
    time.sleep(0.50)
    dm.Key_PressChar("f5")
    time.sleep(0.500)
    dm.Key_PressChar("esc")
    time.sleep(0.500)
    dm.Key_PressChar("f4")
    time.sleep(0.500)
    dm.Key_PressChar("f2")
    time.sleep(0.50)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("f4")
    time.sleep(0.50)
    dm.Key_UpChar("ctrl")
    time.sleep(0.500)
    dm.Key_PressChar("f9")
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.1)
    if(Rx2 != 0):
        Scalecalculatex(dm,Rx2)
    time.sleep(1)
    dm.Key_PressChar("tab")
    if (Ry2 != 0) :       
        Scalecalculatey(dm,Ry2)
    time.sleep(1)
    dm.Key_PressChar("enter")
    time.sleep(0.50)
    dm.Key_PressChar("esc")
    
def Scalecalculatex(dm, fx):
    ''' 计算x缩放数值 '''
    X = float(dm.ocr(int(config['Scalecalculatex']['x1']), 
                    int(config['Scalecalculatex']['y1']), 
                    int(config['Scalecalculatex']['x2']), 
                    int(config['Scalecalculatex']['y2']), 
                     "000000-000000", 
                     1.0))
    dm.Set_Clipboard(str(0.9985))              
    time.sleep(0.1000)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("v")
    time.sleep(0.10)
    dm.Key_UpChar("ctrl")
    time.sleep(1)
    dm.Key_PressChar("enter")
    time.sleep(0.50)
    dm.Key_PressChar("esc")
    time.sleep(0.50)
    dm.Key_PressChar("esc") 
    time.sleep(0.500)
    dm.Key_PressChar("f4")
    time.sleep(0.200)
    dm.Key_PressChar("f2")
    time.sleep(0.20)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("f4")
    time.sleep(0.10)
    dm.Key_UpChar("ctrl")
    time.sleep(0.500)
    dm.Key_PressChar("f9")
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.1)              
    Fx1 = round((X + fx) / (0.9985*X),6)
    Result = str(Fx1)
    dm.Set_Clipboard(Result)
    print("Rx放缩补偿",X,fx,0.9985,Fx1)
    time.sleep(0.1)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("v")
    time.sleep(0.1)
    dm.Key_UpChar("ctrl")

def Scalecalculatey(dm,fy):
    ''' 计算y缩放数值 '''
    X = float(dm.ocr(int(config['Scalecalculatey']['x1']), 
                    int(config['Scalecalculatey']['y1']), 
                    int(config['Scalecalculatey']['x2']), 
                    int(config['Scalecalculatey']['y2']), 
                     "000000-000000", 
                     1.0))
    dm.Set_Clipboard(str(0.9985))
    time.sleep(0.1000)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("v")
    time.sleep(0.10)
    dm.Key_UpChar("ctrl")
    time.sleep(1)
    dm.Key_PressChar("enter")
    time.sleep(0.50)
    dm.Key_PressChar("esc")
    time.sleep(0.50)
    dm.Key_PressChar("esc") 
    time.sleep(0.500)
    dm.Key_PressChar("f4")
    time.sleep(0.200)
    dm.Key_PressChar("f2")
    time.sleep(0.20)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("f4")
    time.sleep(0.10)
    dm.Key_UpChar("ctrl")
    time.sleep(0.500)
    dm.Key_PressChar("f9")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.10)
    dm.Key_PressChar("tab")
    time.sleep(0.1)
    dm.Key_PressChar("tab")
    time.sleep(0.1)
    Fx1 =round((X + fy) /(0.9985*X),6)
    Result = str(Fx1)
    dm.Set_Clipboard(Result)
    print("Ry放缩补偿",X,fy,0.9985,Fx1)
    time.sleep(0.1000)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("v")
    time.sleep(0.10)
    dm.Key_UpChar("ctrl")

def SizeCx(dm,Cx,Cy):
    ''' 平移 凹槽边距 '''
    dm.Key_PressChar("esc")
    time.sleep(1)
    dm.Key_PressChar("f3")
    time.sleep(0.10)
    dm.Key_PressChar("f2")
    time.sleep(0.30)
    dm.Key_PressChar("f3")
    time.sleep(0.30)
    dm.Key_PressChar("f7")
    time.sleep(0.30)
    dm.Key_PressChar("down")
    time.sleep(0.50)
    dm.Key_PressChar("down")
    time.sleep(0.50)
    dm.Key_PressChar("down")
    time.sleep(0.50)
    dm.Key_PressChar("down")
    time.sleep(0.50)
    dm.Key_PressChar("down")
    time.sleep(0.50)
    dm.Key_PressChar("f5")
    time.sleep(0.500)
    dm.Key_PressChar("esc")
    time.sleep(0.500)
    dm.Key_PressChar("f4")
    time.sleep(0.500)
    dm.Key_PressChar("f2")
    time.sleep(0.30)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("f1")
    time.sleep(0.50)
    dm.Key_UpChar("ctrl")
    time.sleep(0.300)
    if(Cx != 0):
        dm.Set_Clipboard(str(Cx))
        time.sleep(0.50)
        dm.Key_DownChar("ctrl")
        dm.Key_PressChar("v")
        time.sleep(0.50)
        dm.Key_UpChar("ctrl")
    time.sleep(0.5)
    dm.Key_PressChar("tab")
    if (Cy != 0) :
        dm.Set_Clipboard(str(Cy))
        time.sleep(0.20)
        dm.Key_DownChar("ctrl")
        dm.Key_PressChar("v")
        time.sleep(0.20)
        dm.Key_UpChar("ctrl")
    print("平移凹槽边距偿：",Cx,Cy)
    time.sleep(1)
    dm.Key_PressChar("enter")
    time.sleep(0.50)
    dm.Key_PressChar("esc")
    
def SizeCy(dm,Cx,Cy):
    ''' 平移y 凹槽宽'''
    dm.Key_PressChar("esc")
    time.sleep(1)
    dm.Key_PressChar("f3")
    time.sleep(0.50)
    dm.Key_PressChar("f2")
    time.sleep(0.50)
    dm.Key_PressChar("f3")
    time.sleep(0.50)
    dm.Key_PressChar("f7")
    time.sleep(0.20)
    dm.Key_PressChar("down")
    time.sleep(0.20)
    dm.Key_PressChar("down")
    time.sleep(0.50)
    dm.Key_PressChar("down")
    time.sleep(0.20)
    dm.Key_PressChar("down")
    time.sleep(0.20)
    dm.Key_PressChar("down")
    time.sleep(0.20)
    dm.Key_PressChar("down")
    time.sleep(0.20)
    dm.Key_PressChar("f5")
    time.sleep(0.500)
    dm.Key_PressChar("esc")
    time.sleep(0.500)
    dm.Key_PressChar("f4")
    time.sleep(0.500)
    dm.Key_PressChar("f2")
    time.sleep(0.50)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("f1")
    time.sleep(0.50)
    dm.Key_UpChar("ctrl")
    time.sleep(0.500)
    if(Cx != 0):
        dm.Set_Clipboard(str(Cx))
        time.sleep(0.50)
        dm.Key_DownChar("ctrl")
        dm.Key_PressChar("v")
        time.sleep(0.50)
        dm.Key_UpChar("ctrl")
    time.sleep(1)
    dm.Key_PressChar("tab")
    if (Cy != 0) :
        dm.Set_Clipboard(str(Cy))
        time.sleep(0.50)
        dm.Key_DownChar("ctrl")
        dm.Key_PressChar("v")
        time.sleep(0.50)
        dm.Key_UpChar("ctrl")
    print("平移y 凹槽宽补偿：",Cx,Cy)
    time.sleep(1)
    dm.Key_PressChar("enter")
    time.sleep(0.50)
    dm.Key_PressChar("esc")
    
def enter_main(dm):
    dm.Key_PressChar("esc")
    time.sleep(0.1)
    dm.Key_PressChar("esc")
    time.sleep(0.1)
    dm.Key_PressChar("f3")
    time.sleep(0.10)
    dm.Key_PressChar("f1")
    time.sleep(0.10)
    dm.Key_DownChar("ctrl")
    dm.Key_PressChar("f7")
    time.sleep(0.10)
    dm.Key_PressChar("f1")
    dm.Key_UpChar("ctrl")
    
