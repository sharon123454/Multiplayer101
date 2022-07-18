using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour
{
    private string LocalIPAddress = "127.0.0.1";
    [SerializeField] int _ListeningPort = 40000;
    [SerializeField] int _SendingPort = 40001;

    [SerializeField] Text ListeningPort;
    [SerializeField] Text SendingPort;
    [SerializeField] GameObject connectButton;
    [SerializeField] GameObject ToggleButton;
    [SerializeField] GameObject InputField1;
    [SerializeField] GameObject InputField2;

    Thread listener;
    static Queue pQueue = Queue.Synchronized(new Queue()); //this is the message queue, it is thread safe
    static UdpClient udp;
    private IPEndPoint endPoint;

    public GameManager gameManager; //drag this on the inspector

    private void Update()
    {
        //in the main thread, read the message and update the game manager
        lock (pQueue.SyncRoot)
        {
            if (pQueue.Count > 0)
            {
                object obj = pQueue.Dequeue(); //Take the olders message out of the queue
                gameManager.GotNetworkMessage((string)obj); //Send it to the game manager
            }
        }
    }

    private void OnDestroy()
    {
        EndUDP();
    }

    public void StartUDP()
    {
        _ListeningPort = int.Parse(ListeningPort.text);
        _SendingPort = int.Parse(SendingPort.text);

        InputField1.SetActive(false);
        InputField2.SetActive(false);
        connectButton.SetActive(false);
        ToggleButton.SetActive(false);

        endPoint = new IPEndPoint(IPAddress.Any, _ListeningPort); //this line will listen to all IP addresses in the network
        //endPoint = new IPEndPoint(IPAddress.Parse(LocalIPAddress), ListeningPort); //this line will listen to a specific IP address
        udp = new UdpClient(endPoint);
        print("Listening for Data...");
        listener = new Thread(new ThreadStart(MessageHandler));
        listener.IsBackground = true;
        listener.Start();
    }

    void MessageHandler()
    {
        Byte[] data = new byte[0];
        while (true)
        {
            try
            {
                //Did we get a new message?
                data = udp.Receive(ref endPoint);
            }
            catch (Exception err)
            {
                //If there's a problem
                print("Communication error, recieve data error " + err);
                udp.Close();
                return;
            }
            //Treat the new message
            string msg = Encoding.ASCII.GetString(data);
            pQueue.Enqueue(msg);
        }
    }

    private void EndUDP()
    {
        if (udp != null)
        {
            udp.Close();
        }
        if (listener != null)
        {
            listener.Abort();
        }
    }

    public void ForwardMessage(string message)
    {
        UdpClient send_client = new UdpClient();
        IPEndPoint send_endPoint = new IPEndPoint(IPAddress.Parse(LocalIPAddress), _SendingPort);
        byte[] bytes = Encoding.ASCII.GetBytes(message);
        send_client.Send(bytes, bytes.Length, send_endPoint);
        send_client.Close();
        print("Sent message: " + message);
    }

}