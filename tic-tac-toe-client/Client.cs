using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

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
            byte[] responseBytes = EncodeMessage(message.CreateJSONSerialize());
            stream.Write(responseBytes);
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