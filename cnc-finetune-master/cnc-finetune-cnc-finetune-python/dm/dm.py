
import win32com.client
from os import getcwd


class DMClient:
    def __init__(self):
        self.dm = win32com.client.Dispatch('dm.dmsoft') 
        self.dm.SetPath(getcwd()) # 根目录
        self.dm.SetDict(0, "CNCword.txt")

    def find_string(self, text, x1, y1, x2, y2, font_name, color, sim=1.0):
        """
        在屏幕区域内查找指定文本
        :param text: 要查找的文本"准备状态"
        :param x1: 查找区域的左上角 x 坐标
        :param y1: 查找区域的左上角 y 坐标
        :param x2: 查找区域的右下角 x 坐标
        :param y2: 查找区域的右下角 y 坐标
        :param font_name: 字体名称
        :param color: 文本颜色，可以使用 RGB 值或颜色名称（如 red、blue 等）
        :param sim: 相似度阈值，默认为 1.0
        :return: 返回文本在屏幕上的位置，如果找不到则返回空字符串
        """
        result = self.dm.FindStrFast(x1, y1, x2, y2, text, font_name, color, sim)
        return result

    def ocr(self, x1, y1, x2, y2, color_format, sim=1.0):
        """
        在屏幕区域内查找指定文本
        :param text: 要查找的文本"准备状态"
        :param x1: 查找区域的左上角 x 坐标
        :param y1: 查找区域的左上角 y 坐标
        :param x2: 查找区域的右下角 x 坐标
        :param y2: 查找区域的右下角 y 坐标
        :param color_format: 颜色格式串
        :param sim: 相似度阈值，默认为 1.0
        :return: 返回识别到的字符串
        """
        result = self.dm.Ocr(x1, y1, x2, y2, color_format, sim)
        return result  

    def left_click(self):
        """
        模拟鼠标左键单击
        """
        self.dm.LeftClick()

    def right_click(self):
        """
        模拟鼠标右键单击
        """
        self.dm.RightClick()

    def key_press(self, key):
        """
        模拟键盘按键
        :param key: 要按下的键，可以使用键盘码或键盘上的字符（如 'a'、'b' 等）
        """
        self.dm.KeyPress(key)

    def Left_DoubleClick(self):
        """
        模拟鼠标左键双击
        """
        self.dm.LeftDoubleClick()
        
    def Key_PressChar(self, key):
        """
        模拟键盘按键
        :param key: 要按下的键，可以使用键盘码或键盘上的字符（如 'a'、'b' 等）
        """
        self.dm.KeyPressChar(key)

    def Key_DownChar(self, key):
        """
        模拟键盘长按
        """
        self.dm.KeyDownChar(key)
        
    def Key_UpChar(self,key):
        """
        模拟键盘松开
        """
        self.dm.KeyUpChar(key)
        
    def Move_To(self, x, y):
        """
        把鼠标移动到目的点(x,y)
        """
        self.dm.MoveTo(x, y)

    def left_click(self):
        """
        模拟鼠标左键单击
        """
        self.dm.LeftClick()
        
    def Get_Clipboard(self):
        """
        获取剪贴板数据
        """
        result =self.dm.GetClipboard()
        return result  

    def Set_Clipboard(self,data):
        """
        数据传送到剪贴板
        """
        self.dm.SetClipboard(data)        

    def Find_Pic(self, x1, y1, x2, y2, pic_name, delta_color,sim, dir):

        """
        查找多个图片,只返回第一个找到的X Y坐标
        x1:区域的左上X坐标
        y1:区域的左上Y坐标
        x2:区域的右下X坐标
        y2:区域的右下Y坐标
        pic_name:图片名,可以是多个图片,比如"test.bmp|test2.bmp|test3.bmp"
        delta_color:颜色色偏 比如"203040" 表示RGB的色偏分别是20 30 40 (这里是16进制表示)
        sim:相似度,取值范围0.1-1.0
        dir:查找方向 0: 从左到右,从上到下 1: 从左到右,从下到上 2: 从右到左,从上到下 3: 从右到左, 从下到上
        intX:返回图片左上角的X坐标
        intY:返回图片左上角的Y坐标
        """
        _, intX, intY,  = self.dm.FindPic(x1, y1, x2, y2, pic_name, delta_color, sim, dir)
        return intX, intY