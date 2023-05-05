
from collections import OrderedDict
import struct
import json
# import zlib

class StateMessage:
    msgtype = None
    value = None
    
    def __init__(self, msgtype:int, cnc_ip:str, plc_ip:str, cnc_state:int, plc_state:int) -> None:
        self.msgtype = msgtype
        self.value = json.dumps(OrderedDict(
                                [("cnc_ip", cnc_ip),
                                ("plc_ip", plc_ip),
                                ("cnc_state", cnc_state),
                                ("plc_state", plc_state)
                                ]))
    
    def _pack(self):
        length = len(self.value)
        crc_byte = struct.pack(">II%ss"%length,
                                self.msgtype,
                                length,
                                self.value.encode("utf-8")
                                )
        Checksum = calc_crc(crc_byte)
        return struct.pack(">II%ssI"%length,
                            self.msgtype,
                            length, 
                            self.value.encode("utf-8"),
                            Checksum)
 

class ResponseMessage:
    msgtype = None 
    hashvalue = None #  master发给cnc的补偿数据生成的hash值
    state = None

    def __init__(self, msgtype:int, hashvalue:str, state:str) -> None:
        self.msgtype = msgtype
        self.hashvalue = hashvalue
        self.value = json.dumps({ 
                                "hash": hashvalue,
                                "state": state
                                })

    def _pack(self):
        length = len(self.value)
        crc_byte = struct.pack(">II%ss"%length,
                                self.msgtype,
                                length,
                                self.value.encode("utf-8")
                                )
        Checksum = calc_crc(crc_byte)
        return struct.pack(">II%ssI"%length,
                            self.msgtype,
                            length, 
                            self.value.encode("utf-8"),
                            Checksum)


# def checksum(value:str) -> int:
#     """Compute checksum
#     >II{}s解释:
#         > 表示以大端模式进行编码（也就是最高位字节放在最前面）。
#         >II{}s 中的 I 表示一个 unsigned int(4 个字节）；
#         {}s 表示一个字符串类型，{} 可以用来动态指定字符串长度。这里是 Value 部分的长度，因此在实际传递数据时需要将其替换为真实的值。
#         比如 32s 表示一个长度为 32 个字符的字符串。
#         返回的是一个4字节的无符号整数
#     """
#     return zlib.crc32()

def calc_crc(data):
    crc = 0xFFFF
    for pos in data:
        crc ^= pos 
        for _ in range(8):
            if ((crc & 1) != 0):
                crc >>= 1
                crc ^= 0xA001
            else:
                crc >>= 1
    return crc



def data_unpack(data):
    msg_type, msg_length = struct.unpack(">II", data[:8])
    value, checksum_received = struct.unpack(">%ssI"%msg_length, data[8:])
    # print(checksum_received)
    # if message.get_encryption():
    #     value = encryptor.decrypt(value)
    #     cleaned_type = msg_type & (0xFFFFFFFF >> 1)
    #     # return a decrpted message
    #     return cls(cleaned_type, value)
    valuedecode = json.loads(value.decode("utf-8"))

    return valuedecode['hash'], eval(valuedecode['value'])