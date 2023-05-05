from configparser import ConfigParser



def read_config(file):
    config = ConfigParser()
    config.read(file)
    return config


