/*
Created by Youssef Elashry to allow two-way communication between Python3 and Unity to send and receive strings

Feel free to use this in your individual or commercial projects BUT make sure to reference me as: Two-way communication between Python 3 and Unity (C#) - Y. T. Elashry
It would be appreciated if you send me how you have used this in your projects (e.g. Machine Learning) at youssef.elashry@gmail.com

Use at your own risk
Use under the Apache License 2.0

Modified by: 
Youssef Elashry 12/2020 (replaced obsolete functions and improved further - works with Python as well)
Based on older work by Sandra Fang 2016 - Unity3D to MATLAB UDP communication - [url]http://msdn.microsoft.com/de-de/library/bb979228.aspx#ID0E3BAC[/url]
*/

using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Linq;
public class UdpSocket : MonoBehaviour
{
    [HideInInspector] public bool isTxStarted = false;

    [SerializeField] string IP = "127.0.0.1"; // local host
    [SerializeField] int rxPort = 8000; // port to receive data from Python on
    [SerializeField] int txPort = 8001; // port to send data to Python on

    //int i = 0; // DELETE THIS: Added to show sending data from Unity to Python via UDP

    // Create necessary UdpClient objects
    UdpClient client;
    IPEndPoint remoteEndPoint;
    Thread receiveThread; // Receiving Thread
    public Transform tank;
    public sbyte[] message;



    //IEnumerator SendDataCoroutine() // DELETE THIS: Added to show sending data from Unity to Python via UDP
    //{
    //    while (true)
    //    {
    //        SendData("Sent from Unity: " + i.ToString());
    //        i++;
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    //public void SendData(sbyte[] message) // Use to send data to Python
    //{
    //    try
    //    {
    //        byte[] data = new byte[message.Length * sizeof(sbyte)];
    //        Buffer.BlockCopy(message, 0, data, 0, data.Length);
            
    //        //byte[] data = Encoding.UTF8.GetBytes(message);
    //        client.Send(data, data.Length, remoteEndPoint);
    //    }
    //    catch (Exception err)
    //    {
    //        print(err.ToString());
    //    }
    //}

    void Awake()
    {
        // Create remote endpoint (to Matlab) 
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), txPort);

        // Create local client
        client = new UdpClient(rxPort);

        // local endpoint define (where messages are received)
        // Create a new thread for reception of incoming messages
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();

        // Initialize (seen in comments window)
        print("UDP Comms Initialised");

        //StartCoroutine(SendDataCoroutine()); // DELETE THIS: Added to show sending data from Unity to Python via UDP
    }

    // Receive data, update packets received
    private void ReceiveData()
    {
        while (true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);
                sbyte[] received = data.Select(x => (sbyte)x).ToArray();

                //Debug.Log("Received = " + received[0] + received[1] + received[2] + received[3]);
                //Debug.Log("Message = " + message[0] + message[1] + message[2] + message[3]);

                //Debug.Log(message.Length);

                message = received;

                

            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    private void ProcessInput(string input)
    {
        // PROCESS INPUT RECEIVED STRING HERE

        if (!isTxStarted) // First data arrived so tx started
        {
            isTxStarted = true;
        }
    }

    //Prevent crashes - close clients and threads properly!
    void OnDisable()
    {
        if (receiveThread != null)
            receiveThread.Abort();

        client.Close();
    }
    private void Update()
    {

        sbyte tankx = (sbyte)tank.position.x;
        sbyte tanky = (sbyte)tank.position.y;
        sbyte tankz = (sbyte)tank.position.z;

        sbyte[] data = {tankx,tanky,tankz};
        //SendData(data);
        
        



    }
}