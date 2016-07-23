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
        /// This is for requests
        /// </summary>
        /// <param name="cred"></param>
        /// <param name="type"></param>
        public ServerMessage(string cred, int type)
        {
            credentials = cred;
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
            credentials = cred;
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
            credentials = cred;
            messageType = type;
            coreSettings = save;
        }

        /// <summary>
        /// 8 request
        /// 9 save
        /// 10 missing settings
        /// 11 missing save
        /// 12 missing both
        /// </summary>
        /// <param name="cred"></param>
        /// <param name="type"></param>
        /// <param name="cSett"></param>
        /// <param name="cSave"></param>
        public ServerMessage(string cred, int type, CoreSettingsType cSett, CoreSaveType cSave)
        {
            credentials = cred;
            messageType = type;
            coreSettings = cSett;
            coreSave = cSave;
        }
    }
}