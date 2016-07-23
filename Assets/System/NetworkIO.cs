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
        public static TcpClient serverConnection;
        private static string loginCredentials = "S3kIIJOq9a";
        private static ServerCommand server = new ServerCommand();

        public static void LoadFromServer()
        {
            serverConnection = new TcpClient();
            serverConnection.Connect("127.0.0.1", 8888);
            NetworkStream serverStream = default(NetworkStream);
            serverStream = serverConnection.GetStream();

            ServerMessage sendLogin = new ServerMessage(loginCredentials, 8);
            server.SendServerMessage(sendLogin, serverConnection);

            ServerMessage serverMessage = server.ReceiveServerMessage(serverConnection, serverStream);
        }

        public static void SendToServer(CoreSaveType coreSave)
        {

        }

        public static void SendToServer(CoreSettingsType coreSettings)
        {

        }

        public static void SendToServer(CoreSettingsType coreSettings, CoreSaveType coreSave)
        {

        }
    }
}