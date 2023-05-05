from modbus_tk import modbus_tcp, defines
from utils.processing import read_config
from log import logger

class ModbusClient:
    def __init__(self):
        self.config = read_config('cncset.ini')['MODBUS']
        self.master = modbus_tcp.TcpMaster(host = self.config['PlcIp'], port = int(self.config['PlcPort']))

    def read_holding_registers(self) -> bool:
        """
        读取 Modbus 寄存器数据 
        :return: 返回读取到的寄存器数据，列表形式
        """
        try:
            result = self.master.execute(slave = 1, 
                                        function_code = defines.READ_HOLDING_REGISTERS, 
                                        starting_address = int(self.config['PlcReadAdress']), 
                                        quantity_of_x = int(self.config['PlcReadCount']))
            return result
        except Exception as e:
            logger.info('Error ', e)
            return (0,0,0,0,0,0,0,0,0,0)
        

    def write_single_register(self, output_value:int) -> bool:
        """
        单个写入 Modbus 寄存器数据
        :return: 返回写入结果, True 表示写入成功, False 表示写入失败
        """
        try:
            self.master.execute(slave = 1, 
                                function_code = defines.WRITE_SINGLE_REGISTER, 
                                starting_address = int(self.config['PlcWriteAdress']), 
                                output_value = output_value)
        
            return True
        except Exception as e:
            logger.info('Error ', e)
            return False



