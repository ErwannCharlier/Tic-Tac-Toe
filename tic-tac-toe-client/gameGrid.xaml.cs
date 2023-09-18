using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace tictactoe_interface
{

    public partial class gameGrid : Page
    {
        Game Game { get; }
        Image X { get; } = new Image();
        Image O { get; } = new Image();
        Client Client { get; }

        public gameGrid()
        {
            InitializeComponent();
            
            X.Source = new BitmapImage(new Uri("/elements/X.png", UriKind.Relative));
            O.Source = new BitmapImage(new Uri("/elements/o.png", UriKind.Relative));

            Game = new Game();
            try
            {
                Client = ClientSingleton.Client;

            }
            catch {
                MessageBox.Show("Server pas en ligne");
                myGrid.Visibility = Visibility.Collapsed;
                gameFrame.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
                return;
            }

            if(!ClientSingleton.IsConnected)
            {
                Client.Start();
                ClientSingleton.IsConnected = true;
            }
            ReloadGrid();
            HandleResponse(0,0);

        }

        private async void Play_Click(object sender, RoutedEventArgs e)
        {

            if (Game.CurentPlayer != CellValue.CROSS)
            {
                MessageBox.Show("pas a ton tour de jouer");
                return;
            }
            Button cell = (Button)sender;

            int row = Grid.GetRow(cell);
            int col = Grid.GetColumn(cell);

            if (Game.IsMoveValid(row, col))
            {
                Game.MakeMove(row, col);
                ReloadGrid();
            }
            else
            {
                MessageBox.Show("Coup invalide, rejoue");
                return;
            }


            MessageSerializable messageMove = new MessageSerializable(col, row, false);
            Client.SendMessage(messageMove);

            if (HandleGameFinished())
            {
                return;
            }


            HandleResponse(row, col);
        }

        private bool HandleGameFinished()
        {
            if (Game.IsFinished)
            {
                myGrid.Visibility = Visibility.Collapsed;

                CellValue winner = Game.CheckWinner();

                if (winner == CellValue.CROSS)
                {
                    gameFrame.Navigate(new Uri("winnerPage.xaml", UriKind.Relative));

                }
                else if (winner == CellValue.CIRCLE)
                {
                    gameFrame.Navigate(new Uri("loosePage.xaml", UriKind.Relative));

                }
                else
                {
                    gameFrame.Navigate(new Uri("drawPage.xaml", UriKind.Relative));

                }
                return true;
            }
            return false;

        }
        private async void HandleResponse(int row, int col)
        {
            // Attendre le premier message du serveur
            MessageSerializable moveReceived = await Client.ReceiveMessage();

            // voir si le coup a été flag comme non valide
            if (moveReceived.HasError)
            {
                MessageBox.Show("Coup précédent invalide. Rejoue");
                Game.RemoveLastMove();
                ReloadGrid();
            }

            if (!Game.IsMoveValid(moveReceived.Row, moveReceived.Col))
            {
                MessageSerializable messageMoveError = new MessageSerializable(0, 0, true);
                Client.SendMessage(messageMoveError);
                moveReceived = await Client.ReceiveMessage();

                while (!Game.IsMoveValid(moveReceived.Row, moveReceived.Col))
                {
                    moveReceived = await Client.ReceiveMessage();
                }
            }

            Game.MakeMove(moveReceived.Row, moveReceived.Col);
            ReloadGrid();
            HandleGameFinished();
        }
        

        private void ReloadGrid()
        {
            for (int row = 0; row < Game.GRIDSIZE; row++)
            {
                for (int column = 0; column < Game.GRIDSIZE; column++)
                {
                    CellValue cell = Game.GetCell(row, column);
                    string buttonName = $"button{row}{column}";
                    Button button = FindName(buttonName) as Button;
                    Image image = new Image();
                    if (cell.Equals(CellValue.EMPTY))
                    {
                        image.Source = null;
                    }
                    else
                    {
                        image.Source = cell == CellValue.CIRCLE ? O.Source : X.Source;
                    }
                    button.Content = image;
                }
            }
        }
    }
}