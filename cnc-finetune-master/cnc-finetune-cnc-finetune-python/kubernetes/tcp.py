import socket
from utils.processing import read_config
from log import logger

class BaceTcpClient:
    def __init__(self):
        self.tcpconfig = read_config('cncset.ini')
        self.socket = None      

    def connect(self):
        self.socket = socket.socket()
        # 绑定端口号
        #self.socket.bind((self.tcpconfig['DEFAULT']['CncHost'], int(self.tcpconfig['DEFAULT']['CncPort'])))
        self.socket.connect((self.tcpconfig['TCP']['K8sHost'], int(self.tcpconfig['TCP']['K8sPort'])))
        print("tcp connect ok")
        # 设置套接字为非阻塞模式
        # self.socket.setblocking(False)
    def send(self, data):
        
        self.socket.sendall(data)

    def receive(self):
        # try:
        data = self.socket.recv(1024)
        return data
        # except:
        #     return False

    def close(self):
         self.socket.close()
    def is_socket_connected(self):
        try:
            # 获取选项值
            err = self.socket.getsockopt(self.socket.SOL_SOCKET, self.socket.SO_ERROR)
            # 如果错误值为0，则连接成功
            if err == 0:
                return True
            else:
                return False
        except Exception as e:
            logger.info('Error ', e)
            return False



        
