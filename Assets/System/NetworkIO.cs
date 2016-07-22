using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

namespace CoreSys
{
    public static class NetworkIO
    {
        public static TcpClient server;
        private static string loginCredentials = "S3kIIJOq9a";

        public static void LoadFromServer()
        {
            server = new TcpClient();
            server.Connect("127.0.0.1", 8888);
            NetworkStream serverStream = default(NetworkStream);
            serverStream = server.GetStream();

            LoginMessage loginMessage = new LoginMessage(loginCredentials);
            ServerMessage sendLogin = new ServerMessage(1, loginMessage, GameManager.GetGameVersionMessage()); //FIXTHIS -- Game version needs to be checked before connection allows for messages to be sent.
            server.SendServerMessage(sendLogin, GameManager.serverSocket);
        }
    }
}