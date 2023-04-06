using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour
{
    #region variables for communication
    private Thread receiveThread;
    private UdpClient client;
    [SerializeField] private int port = 5052;
    [SerializeField] private bool startReceiving = true;
    [SerializeField] private bool printToConsole = false;
    public string data;
    #endregion

    void Start()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);

        while (startReceiving)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] dataByte = client.Receive(ref anyIP);
                data = Encoding.UTF8.GetString(dataByte);

                if (printToConsole) { print(data); }
            }
            catch(System.Exception err)
            {
                print(err.ToString());
            }
        }
    }
}
