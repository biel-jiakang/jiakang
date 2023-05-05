from dm.task import *
from kubernetes.tcp import BaceTcpClient
from utils.structure import data_unpack, StateMessage, ResponseMessage
from utils.processing import read_config


class State:
    def __init__(self, Dm, Mc):
        self.Dm = Dm
        self.Mc = Mc
        self.PlcAddress = None
        self.state = None
    
    def update(self, state):
        self.state = state

        
    def get_cnc_state(self):
        # Processtatus [Ready, Pause, Running]
        Processtatus, *Processinginfo = read_cncstatus(self.Dm)
        return Processtatus
    def get_plc_state(self):
        # Plcstatus  4165 2:运行，3:暂停，6:暂停, 4: 故障
        self.PlcAddress = self.Mc.read_holding_registers()
        Plcstatus = self.PlcAddress[0]
        Plccompensation = self.PlcAddress[5]
        return  Plcstatus,Plccompensation
    
    def state_judge(self):
        # 1 待用 2 运行 3 维修 4故障 5报废/转移 6 暂停
        # PLC: {4165:{2:运行，3:暂停，6:暂停, 4: 故障}
        state = 2 # Default state 
        Processtatus= self.get_cnc_state()
        Plcstatus ,Plccompensation= self.get_plc_state()
        if Processtatus=='Ready' and Plcstatus != 4:
            state = 1
        if Processtatus=='Running' and Plcstatus == 2:
            state = 2
        if Plcstatus == 4 or (Plcstatus in [3, 6] and Plccompensation in [0,1]):
            state = 4
        if (Processtatus == 'Pause'  and Plcstatus != 4) or (Plcstatus in [3, 6] and Plccompensation in [2, 3,4]):
            state = 6
        return state
    
    def _ischanged(self):
        current_state = self.state_judge()
        if current_state != self.state:
            self.update(current_state)
            print("机台状态变化:",current_state)
            return True
        else:
            return False

    
class MsgProcess(BaceTcpClient):
    def __init__(self, cfg_file):
        super().__init__()
        self.msgtype = None
        self.cnchost = None
        self.plchost = None
        self.cncstate = None
        self.plcstate = None
        self.msgvalue = None
        self.config = read_config(cfg_file)
        

    def _update(self, msgtype, plchost, cncstate, plcstate):
        self.msgtype = msgtype
        self.cnchost = self.config['CNC']['Host']
        self.plchost = plchost
        self.cncstate = cncstate
        self.plcstate = plcstate
        self.msgvalue = self.message_pack()
        
        
    def message_pack(self):
        msgvalue = StateMessage(self.msgtype, 
                                self.cnchost, 
                                self.plchost, 
                                self.cncstate, 
                                self.plcstate)
        return msgvalue
    
    def sendresponsemessage(self, msgtype, hashvalue, statevalue):
        # state: 2   1：失败；  2：完成
        responsemessage = ResponseMessage(msgtype, hashvalue, statevalue)
        self.connect()
        self.send(responsemessage._pack())
        self.close()
        return 0
        
    def getcompensation(self):
        data = self.request(self.msgvalue._pack())
        hashvalue, cptvalue  = data_unpack(data)
        return hashvalue, cptvalue
        
    def request(self, rbs):
        self.connect()
        self.send(rbs)
        print("data sended ok")
        data = self.receive()
        print("data recived")
        self.close()
        return data 
    
    
def cnc_simulation_operation(dm, cptvalue):
    # extract
    valuedata = cptvalue['data']
    # dict merge muliti mold 
    value_merge = {k:v for d in valuedata.values() for k,v in d.items()}
    main_page_value = {k:v for k,v in value_merge.items() if 'D' in k}
    other_page_value = {k:v for k,v in value_merge.items() if 'D' not in k}
    
    main_page_result =  main_page_operation(dm, main_page_value)
    if not main_page_result:
        return False
    else:
        other_page_result = other_page_operation(dm, other_page_value)
    
    return other_page_result
    
    
def main_page_operation(dm, main_page_value):
    # 模拟调机 D6 D7
    D6=main_page_value.get('D6', 0)
    D7=main_page_value.get('D7', 0)
    if sum([D6, D7]) == 0:
        return True
    else:
        intX, intY = back_main(dm)
        if intX >= 0 and intY >= 0:
            size_compensation(dm, D6, D7)
            return True
        else:
            return False

def other_page_operation(dm, other_page_value):
    # 模拟调机 Rx1 R
    Rx1 = other_page_value.get('Rx1', 0)
    Ry1 = other_page_value.get('Ry1', 0)
    Rx2 = other_page_value.get('Rx2', 0)
    Ry2 = other_page_value.get('Ry2', 0)
    Cy1 = other_page_value.get('Cy1', 0)
    Cy2 = other_page_value.get('Cy2', 0)
    Cx1 = other_page_value.get('Cx1', 0)
    Cx2 = other_page_value.get('Cx2', 0)
    if sum([Rx1, Ry1, Rx2, Ry2, Cy1, Cy2, Cx1, Cx2]) == 0:
        return True
    else:
        intX, intY = back_main(dm)
        if intX >= 0 and intY >= 0:
            quit_main(dm)
            if Rx1 != 0 or Ry1 !=0:
                SizeRx(dm, Rx1, Ry1)
                
            if Rx2 != 0 or Ry2 !=0:
                SizeRy(dm, Rx2, Ry2)
                
            if Cx1 != 0 or Cy1 !=0:
                SizeCx(dm, Cx1, Cy1)
                
            if Cx2 != 0 or Cy2 !=0:
                SizeCy(dm, Cx2, Cy2)
            enter_main(dm)
            return True
        else:
            return False
            
            
