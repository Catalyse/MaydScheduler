using System.Net.Sockets;
using System.Threading;

namespace CoreSys
{
    public static class NetworkIO
    {
        private static TcpClient serverConnection;
        private static NetworkStream serverStream = default(NetworkStream);
        private static string loginCredentials = "S3kIIJOq9a";
        private static ServerCommand server = new ServerCommand();

        public static void ConnectionSwitch(int choice)
        {
            switch(choice)
            {
                case 1:
                    Thread connectionProccess = new Thread(new ThreadStart(NetworkIO.SendCoreSave));
                    connectionProccess.Start();
                    break;
                case 2:
                    Thread connectionProccess1 = new Thread(new ThreadStart(NetworkIO.SendCoreSettings));
                    connectionProccess1.Start();
                    break;
                case 3:
                    Thread connectionProccess2 = new Thread(new ThreadStart(NetworkIO.SendBoth));
                    connectionProccess2.Start();
                    break;
                case 4:
                    Thread connectionProccess3 = new Thread(new ThreadStart(NetworkIO.RequestCoreSave));
                    connectionProccess3.Start();
                    break;
                case 5:
                    Thread connectionProccess4 = new Thread(new ThreadStart(NetworkIO.RequestCoreSettings));
                    connectionProccess4.Start();
                    break;
                case 6:
                    Thread connectionProccess5 = new Thread(new ThreadStart(NetworkIO.RequestBoth));
                    connectionProccess5.Start();
                    break;
            }
        }

        private static void ConnectToServer()
        {
            serverConnection = new TcpClient();
            serverConnection.Connect("127.0.0.1", 8888);
            serverStream = serverConnection.GetStream();
            CoreSystem.networkLoading = true;
            CoreSystem.networkFailure = false;
        }

        private static void DisconnectFromServer()
        {
            serverConnection.Close();
            serverStream.Close();
            CoreSystem.networkLoading = false;
        }

        public static void SendCoreSave()
        {
            try
            {
                ConnectToServer();
                ServerMessage request = new ServerMessage(loginCredentials, 3, CoreSystem.coreSave);
                server.SendServerMessage(request, serverConnection);
                ServerMessage response = server.ReceiveServerMessage(serverConnection, serverStream);
                if (response.messageType == 1)
                    DisconnectFromServer();
                else
                {
                    CoreSystem.networkFailure = true;
                    DisconnectFromServer();
                }
            }
            catch
            {
                CoreSystem.networkFailure = true;
            }
        }

        public static void SendCoreSettings()
        {
            try
            {
                ConnectToServer();
                ServerMessage request = new ServerMessage(loginCredentials, 6, CoreSystem.coreSettings);
                server.SendServerMessage(request, serverConnection);
                ServerMessage response = server.ReceiveServerMessage(serverConnection, serverStream);
                if (response.messageType == 1)
                    DisconnectFromServer();
                else
                {
                    CoreSystem.networkFailure = true;
                    DisconnectFromServer();
                }
            }
            catch
            {
                CoreSystem.networkFailure = true;
            }
        }

        public static void SendBoth()
        {
            try
            {
                ConnectToServer();
                ServerMessage request = new ServerMessage(loginCredentials, 9, CoreSystem.coreSettings, CoreSystem.coreSave);
                server.SendServerMessage(request, serverConnection);
                ServerMessage response = server.ReceiveServerMessage(serverConnection, serverStream);
                if (response.messageType == 1)
                    DisconnectFromServer();
                else
                {
                    CoreSystem.networkFailure = true;
                    DisconnectFromServer();
                }
            }
            catch
            {
                CoreSystem.networkFailure = true;
            }
        }

        public static void RequestCoreSave()
        {
            try
            {
                ConnectToServer();
                ServerMessage request = new ServerMessage(loginCredentials, 2);
                server.SendServerMessage(request, serverConnection);
                ServerMessage response = server.ReceiveServerMessage(serverConnection, serverStream);
                if (response.messageType == 3)
                {
                    CoreSystem.CoreSaveLoaded(response.coreSave);
                }
                else if (response.messageType == 4)
                {
                    //Means that there were no settings saved
                }
                else
                {
                    //something else happened, this is bad mkay
                }
                DisconnectFromServer();
            }
            catch
            {
                CoreSystem.networkFailure = true;
            }
        }

        public static void RequestCoreSettings()
        {
            try
            {
                ConnectToServer();
                ServerMessage request = new ServerMessage(loginCredentials, 5);
                server.SendServerMessage(request, serverConnection);
                ServerMessage response = server.ReceiveServerMessage(serverConnection, serverStream);
                if (response.messageType == 6)
                {
                    CoreSystem.CoreSettingsLoaded(response.coreSettings);
                }
                else if (response.messageType == 7)
                {
                    //No settings saved
                }
                else
                {
                    //something else failed
                }
                DisconnectFromServer();
            }
            catch
            {
                CoreSystem.networkFailure = true;
            }
        }

        public static void RequestBoth()
        {
            try
            {
                ConnectToServer();
                ServerMessage request = new ServerMessage(loginCredentials, 8);
                server.SendServerMessage(request, serverConnection);
                ServerMessage response = server.ReceiveServerMessage(serverConnection, serverStream);
                if (response.messageType == 9)
                {
                    CoreSystem.CoreSaveLoaded(response.coreSave);
                    CoreSystem.CoreSettingsLoaded(response.coreSettings);
                }
                else if (response.messageType == 10)//This just means we've never saved anything before.
                {
                    CoreSystem.CoreSaveLoaded(response.coreSave);
                }
                else if (response.messageType == 11)
                {
                    CoreSystem.CoreSettingsLoaded(response.coreSettings);
                }
                else//total failure
                {
                    //bad mkay
                }
                DisconnectFromServer();
            }
            catch
            {
                CoreSystem.networkFailure = true;
            }
        }
    }
}