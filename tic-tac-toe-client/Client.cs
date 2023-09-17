using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using System.Net.Http;

enum ErrorCode
{
    NO_ERROR = 0,
    INVALID_MOVE = -1,
    CLIENT_DISCONNECTION = -2,
    ERROR = -3


}
namespace tictactoe_interface
{

    public class Client
    {
        private TcpClient tcpClient { get; set; }
        private NetworkStream stream { get; set; }

        public Client(string ipAddress, int port)
        {
            tcpClient = new TcpClient(ipAddress, port);
            
        }

        public void Start()
        {
            stream = tcpClient.GetStream();
        }


        public async Task<MessageSerializable> ReceiveMessage()
        {
            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            String json = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            return MessageSerializable.ReadJSONSerialize(json);
            
            
        }

        public void SendMessage(MessageSerializable message)
        {
            try
            {
                byte[] responseBytes = EncodeMessage(message.CreateJSONSerialize());
                stream.Write(responseBytes);
                Console.WriteLine("Sent Message");
            }
            catch

            (Exception e)
            {
                Console.WriteLine("Error in SendMessage: " + e.StackTrace);
            }
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