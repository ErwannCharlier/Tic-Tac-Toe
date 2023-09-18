using System;
using System.Windows;
using System.Windows.Controls;

namespace tictactoe_interface
{
    /// <summary>
    /// Logique d'interaction pour HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            startGameButton.Visibility = Visibility.Collapsed;
            homeFrame.Navigate(new Uri("gameGrid.xaml", UriKind.Relative));
        }
    }
}
