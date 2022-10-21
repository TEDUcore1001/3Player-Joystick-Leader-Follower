from email import message
import socket
import struct
import threading
import random

localIP     = "localhost"

listenport   = 8001
sendport   = 8000




def send_data():

    udp_server = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)

        
    while (1):

        x = random.randrange(0,255)
    
        message = [x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x]

        _bytes_to_send = struct.pack("B"*len(message), *message)
        udp_server.sendto(_bytes_to_send, (localIP,sendport))



def receive_data():

    udp_client = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
    udp_client.bind((localIP,listenport))
    while True:
        msgFromServer = udp_client.recvfrom(1024)[0]
        
        mx = [int(x) for x in msgFromServer]
        print(mx)

def start_threads():

    send_thread = threading.Thread(target = send_data)
    send_thread.start()

    receive_thread = threading.Thread(target=receive_data)
    receive_thread.start()

    


if __name__ == "__main__":
    

    start_threads()