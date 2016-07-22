namespace CoreSys
{
    public class ServerMessage
    {
        public int messageType;
        public string credentials;
        public CoreSaveType coreSave;
        public CoreSettingsType coreSettings;

        public ServerMessage() { }

        /// <summary>
        /// Type 1 used for successful save
        /// Type 8 used for disconnects
        /// </summary>
        /// <param name="type"></param>
        public ServerMessage(int type)//Type 6: Cancel/Disconnect
        {
            messageType = type;
        }

        /// <summary>
        /// If type 2 is used, this is a request.
        /// If type 3 is used, its a save command.
        /// If type 4 is used, there is no save file(server to client only)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="save"></param>
        public ServerMessage(string cred, int type, CoreSaveType save)
        {
            messageType = type;
            coreSave = save;
        }

        /// <summary>
        /// If type 5 is used, this is a request.
        /// If type 6 is used, its a save command.
        /// If type 7 is used, there is no save file(server to client only)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="save"></param>
        public ServerMessage(string cred, int type, CoreSettingsType save)
        {
            messageType = type;
            coreSettings = save;
        }
    }
}