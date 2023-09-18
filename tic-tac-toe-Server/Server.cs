using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

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
            return MessageSerializable.ReadJSONSerialize(receivedMessage);

        }

        public void SendMessage(MessageSerializable message)
        {
            byte[] responseBytes = EncodeMessage(message.CreateJSONSerialize());
            clientSocket.Send(responseBytes);            
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
