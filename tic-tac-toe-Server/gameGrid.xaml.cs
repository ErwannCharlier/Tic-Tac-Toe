using System;
using System.IO.Compression;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;


namespace tictactoe_interface
{

    public partial class gameGrid : Page
    {
        Game Game { get; set; }
        Image X { get; } = new Image();
        Image O { get; } = new Image(); 
        Server Server { get; }
        

        public gameGrid()
        {
            InitializeComponent();

            X.Source = new BitmapImage(new Uri("/elements/X.png", UriKind.Relative));
            O.Source = new BitmapImage(new Uri("/elements/o.png", UriKind.Relative));

            Game = new Game();
            Server = SingletonServer.Server;
            if (!SingletonServer.IsConnected)
            {
                ShowWaitingMessage();
                Task.Run(() =>
                {
                    Server.Start();
                    // Marquez la connexion comme établie
                    SingletonServer.IsConnected = true;
                    Dispatcher.Invoke(() =>
                    {
                        HideWaitingMessage();
                        ReloadGrid();
                    });
                });
            }
            
            else
            {
                // La connexion est déjà établie, vous pouvez effectuer d'autres opérations ici si nécessaire.
                HideWaitingMessage();
                ReloadGrid();
            }

        }

        private void ShowWaitingMessage()
        {
            // Afficher le message d'attente
            waitingMessageTextBlock.Visibility = Visibility.Visible;
        }

        private void HideWaitingMessage()
        {
            // Masquer le message d'attente
            waitingMessageTextBlock.Visibility = Visibility.Collapsed;

        }


        private async void Play_Click(object sender, RoutedEventArgs e)
        {
            if (!SingletonServer.IsConnected)
            {
                MessageBox.Show("Joueur non connecté");
                return;
            } 
            if (Game.CurentPlayer != CellValue.CIRCLE)
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
            try
            {
                Server.SendMessage(messageMove);

            }
            catch {
                HandleDisconnection();
                return;
            }
            if(HandleGameFinished())
                return;
            HandleResponse(row,col);
            

        }

        private async void HandleResponse(int row, int col)
        {
            try
            {

                MessageSerializable moveReceived = await Server.ReceiveMessage();
            
                // voir si le coup a été flag
                if (moveReceived.HasError)
                {
                    Game.RemoveCoup(row, col);
                    ReloadGrid();
                    MessageBox.Show("Coup précédent invalide, rejoue");
                    return;
                }

                if (!Game.IsMoveValid(moveReceived.Row, moveReceived.Col))
                {
                    MessageSerializable messageMoveError = new MessageSerializable(0, 0, true);
                    Server.SendMessage(messageMoveError);
                    moveReceived = await Server.ReceiveMessage();

                    while (!Game.IsMoveValid(moveReceived.Row, moveReceived.Col))
                    {
                        // Continue à recevoir et vérifier les coups jusqu'à ce qu'un coup valide soit reçu
                        moveReceived = await Server.ReceiveMessage();
                    }
                }

                Game.MakeMove(moveReceived.Row, moveReceived.Col);
                ReloadGrid();
                HandleGameFinished();
            }
            catch
            {
                HandleDisconnection();

            }
        }
        
        private void HandleDisconnection()
        {
            MessageBox.Show("Le client a déconnecté");
            SingletonServer.IsConnected = false;
            myGrid.Visibility = Visibility.Collapsed;

            gameFrame.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }

        private bool HandleGameFinished()
        {
            if (Game.IsFinished)
            {
                myGrid.Visibility = Visibility.Collapsed;

                CellValue winner = Game.CheckWinner();

                if (winner == CellValue.CIRCLE)
                {
                    gameFrame.Navigate(new Uri("winnerPage.xaml", UriKind.Relative));

                }
                else if(winner == CellValue.CROSS)
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