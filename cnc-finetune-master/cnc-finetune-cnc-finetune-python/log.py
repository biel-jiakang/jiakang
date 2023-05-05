import os
import logging
from logging.handlers import RotatingFileHandler

ROOT_PATH = os.getcwd()
LOG_NAME = 'INFO.log'

# create file handler which logs even INFO messages
logger = logging.getLogger(LOG_NAME)
logger.setLevel(logging.INFO)

# create formatter and add it to the handlers
handler = RotatingFileHandler(filename=os.path.join(ROOT_PATH, 'logs', LOG_NAME), maxBytes=1024*1024*5, backupCount=5)
handler.setFormatter(logging.Formatter('%(asctime)s | %(message)s', datefmt='%Y-%m-%d %H:%M:%S'))

# add the handlers to the logger
logger.addHandler(handler)