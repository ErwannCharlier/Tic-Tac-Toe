namespace tictactoe_interface
{
    public static class SingletonServer
    {
        private static Server server = null;
        public static bool IsConnected { get;  set; } = false;

        public static Server Server
        {
            get
            {
                if (server == null)
                {
                    server = new Server("127.0.0.1", 7771);
                }

                return server;
            }
        }

    }
}
