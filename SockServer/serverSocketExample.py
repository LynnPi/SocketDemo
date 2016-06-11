# !/usr/bin/env python

from socket import *
from time import ctime

tcpServerSock = socket(AF_INET, SOCK_STREAM)
tcpServerSock.bind(('localhost', 21567))
tcpServerSock.listen(5)

while True:
    print('wainting for connection...')
    tcpClientSock, addr = tcpServerSock.accept()
    print('... connected from: ', addr)

    while True:
        data = tcpClientSock.recv(1024)
        if not data:
            print('data is null')
            break
        tcpClientSock.send('[%s] %s' % (ctime(), data))
        print('send finished!', data)
        #conn.close()
#tcpServerSock.close()



