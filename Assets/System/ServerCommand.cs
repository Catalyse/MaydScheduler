using System.IO;
using System.Xml.Serialization;
using System.Net.Sockets;

namespace CoreSys
{
    /// <summary>
    /// Small instancible class that can serialize/send and receive/deserialize
    /// </summary>
    public class ServerCommand //Allows instancing of common server commands
    {
        public ServerCommand() { }

        public void SendServerMessage(ServerMessage message, TcpClient client)
        {
            NetworkStream networkStream = client.GetStream();
            ServerMessage sendToClient = message;

            var serializer = new XmlSerializer(typeof(ServerMessage));
            var outStream = new StringWriter();
            serializer.Serialize(outStream, sendToClient);

            string streamToSend = outStream.ToString();
            streamToSend = streamToSend + "&?Split&?";
            byte[] sendStream = System.Text.Encoding.ASCII.GetBytes(streamToSend);
            networkStream.Write(sendStream, 0, sendStream.Length);
            networkStream.Flush();//Might be totally useless -- MSDN NOTE: The Flush method implements the Stream.Flush method; however, because NetworkStream is not buffered, it has no affect on network streams. Calling the Flush method does not throw an exception.
        }

        public ServerMessage ReceiveServerMessage(TcpClient sender, NetworkStream networkStream)
        {
            ServerMessage inStreamServerMessage = new ServerMessage();
            byte[] bytesFrom = new byte[sender.ReceiveBufferSize];
            string dataFromClient = null;
            networkStream.Read(bytesFrom, 0, sender.ReceiveBufferSize);
            dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
            dataFromClient = dataFromClient.Replace("&?Split&?", "");
            var serializer = new XmlSerializer(typeof(ServerMessage));
            using (TextReader reader = new StringReader(dataFromClient))
            {
                inStreamServerMessage = serializer.Deserialize(reader) as ServerMessage;
            }
            return inStreamServerMessage;
        }
    }
}