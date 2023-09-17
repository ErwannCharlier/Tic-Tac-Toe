using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using System.Windows;

enum ErrorCode
{
    NO_ERROR = 0 ,
    INVALID_MOVE = -1,
    CLIENT_DISCONNECTION = -2,
    ERROR = -3


}
namespace tictactoe_interface
{

    public class Server
    {
        private TcpListener tcpListener { get; set; }
        private Socket clientSocket { get; set; }


        public Server(string ipAddress, int port)
        {
            IPAddress ipAd = IPAddress.Parse(ipAddress);
            tcpListener = new TcpListener(ipAd, port);
            tcpListener.Start();

        }

        public void Start()
        {

            clientSocket = tcpListener.AcceptSocket(); // wait until connection

        }


        public async Task<MessageSerializable> ReceiveMessage() 
        {
            byte[] receivedBytes = new byte[100];
            int bytesRead = await clientSocket.ReceiveAsync(receivedBytes, SocketFlags.None);
            string receivedMessage = DecodeMessage(receivedBytes, bytesRead);
            Console.WriteLine("Received: " + receivedMessage);

            return MessageSerializable.ReadJSONSerialize(receivedMessage);

        }

        public void SendMessage(MessageSerializable message)
        {

            byte[] responseBytes = EncodeMessage(message.CreateJSONSerialize());
            clientSocket.Send(responseBytes);
            Console.WriteLine("Sent Message");
            
        }

        public byte[] EncodeMessage(string message)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            return asen.GetBytes(message);
        }

        public string DecodeMessage(byte[] data, int length)
        {
            ASCIIEncoding asen = new ASCIIEncoding();
            return asen.GetString(data, 0, length);
        }
            
        


    }

}
