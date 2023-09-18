namespace tictactoe_interface
{
    public static class ClientSingleton
    {
        private static Client client = null;
        public static bool IsConnected { get; set; } = false;

        public static Client Client
        {
            get
            {
                if (client == null)
                {
                    client = new Client("127.0.0.1", 7771);
                }

                return client;
            }
        }

    }

}
