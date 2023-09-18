using System;
using System.Windows;
using System.Windows.Controls;
namespace tictactoe_interface
{
    /// <summary>
    /// Logique d'interaction pour winnerPage.xaml
    /// </summary>
    public partial class winnerPage : Page
    {
        public winnerPage()
        {
            InitializeComponent();
        }
        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGameButton.Visibility = Visibility.Collapsed;
            winnerFrame.Navigate(new Uri("gameGrid.xaml", UriKind.Relative));
        }
    }
}
