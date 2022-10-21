from os import O_TEMPORARY
import socket
import struct
from queue import Queue
from threading import Thread
  
localIP     = "0.0.0.0"

listenport   = 8085
sendport   = 8000

# A thread that produces data
def receive_data(out_q):
    udp_client = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
    udp_client.bind((localIP,listenport))
    while True:
        msgFromServer = udp_client.recvfrom(1024)[0]
        
        mx = [int(x) for x in msgFromServer]

        # if (mx == data):{
        #     print ('same')
        # }

        data = mx
        
        out_q.put(data)
        
          
# A thread that consumes data
def send_data(in_q):

    udp_server = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)

    while (1):

        data = in_q.get()
        #print(out_q.qsize())

        message = data
        if (in_q.qsize() > 2):
            in_q.queue.clear()

        _bytes_to_send = struct.pack("B"*len(message), *message)

        udp_server.sendto(_bytes_to_send, ("localhost",sendport))

        print(message)
        
          
# Create the shared queue and launch both threads
q = Queue()
t1 = Thread(target = send_data, args =(q, ))
t2 = Thread(target = receive_data, args =(q, ))
t1.start()
t2.start()