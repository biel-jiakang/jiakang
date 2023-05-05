import time
from modbus.modbus import ModbusClient
from dm.dm import DMClient
from core import State, MsgProcess, cnc_simulation_operation
from log import logger

# Common class and Constant
# Mc = ModbusClient()
# Dm = DMClient()

# Constants
STATE_MSG = 3  # State message 状态消息
RESPONSE_MSG = 5  # Response message 响应消息
PLC_STOP_SIGNAL = 4  # PLC stop signal 停止信号
PLC_STATE_SIGNAL = 6 # PLC state address 状态地址
PLC_DONE_SIGNAL = 3  # PLC done signal 完成信号

def send_stop_signal(mc):
    mc.write_single_register(PLC_STOP_SIGNAL)
def send_done_signal(mc):
    mc.write_single_register(PLC_DONE_SIGNAL)

def wait_for_plc_stop(state_obj):
    while state_obj.PlcAddress[PLC_STATE_SIGNAL] % 2 != 1:
        state_obj.get_plc_state()
        print('waiting plc to stop')
        time.sleep(3)

def send_response(msg_obj, hashvalue, res):
    cpt_state = {False: 1, True: 2}
    msg_obj.sendresponsemessage(RESPONSE_MSG, hashvalue, cpt_state[res])


def process_state(state_obj, msg_obj, mc, dm):
    msg_obj._update(STATE_MSG, mc.config['PlcIp'], state_obj.state, state_obj.state)
    try:
        hashvalue, cptvalue = msg_obj.getcompensation()
        print(cptvalue)
        if cptvalue['compensation_type'] in [2,3,4,5]:
            #try:
            send_stop_signal(mc)
            wait_for_plc_stop(state_obj)
            res = cnc_simulation_operation(dm, cptvalue)
            if cptvalue['compensation_type'] in [2, 4]:
                send_done_signal(mc)
            #except:
            #res = False
            print('CNC operation result: ', res)
            send_response(msg_obj, hashvalue, res)
            msg_obj.close()
        
    except Exception as e:
        logger.info('Error ', e)
        print('tcp stop')
        pass

def main():
    mc = ModbusClient()
    dm = DMClient()
    state_obj = State(dm, mc)
    msg_obj = MsgProcess('cncset.ini')
    while True:
        if state_obj.get_cnc_state()=="running":
            state=1
            continue
        else:
            if state_obj._ischanged() or int(msg_obj.config['CNC']['Test_Switch']) == 1 or state==1:
                process_state(state_obj, msg_obj, mc, dm)
                state=0
        time.sleep(2)

# def send_response(msg_obj, hashvalue, res):
#     cpt_state = {False: 1, True: 2}
#     msg_obj.sendresponsemessage(RESPONSE_MSG, hashvalue, cpt_state[res])

# def main():
#     state_obj = State(Dm, Mc)
#     msg_obj = MsgProcess('cnc.ini')
#     cpt_state = {True:2, False:1}
   
#     while True:
#         if state_obj._ischanged() or msg_obj.config['DEFAULT']['Test_Switch'] == 1:
#             # Update data 
#             msg_obj._update(STATEMSG, Mc.config['PlcIp'], state_obj.state, state_obj.state)
#             # Recive hash value cptvalue
#             hashvalue, cptvalue = msg_obj.getcompensation()
#             if cptvalue['compensation_type'] in [2, 4]:
#                 # Send STOPSIGNAL to plc
#                 Mc.write_single_register(PLCSTOPSIGNAL)
#                 # Waiting plc status to stop
#                 while state_obj.PlcAddress[PLCSTATESIGNAL] %2 != 1:
#                     state_obj.get_total_state()
#                     print('waiting plc to stop')
#                     time.sleep(3)
#                 res = cnc_simulation_operation(Dm, cptvalue)
#                 print('CNC operation result: ', res)
#                 msg_obj.sendresponsemessage(RESPONSEMSG, hashvalue, cpt_state[res])
#                 msg_obj.close()
#         time.sleep(5)

                    

if __name__ == "__main__":
    main()