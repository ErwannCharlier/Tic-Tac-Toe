using System;
using System.Windows;
using System.Windows.Controls;

namespace tictactoe_interface
{
    /// <summary>
    /// Logique d'interaction pour drawPage.xaml
    /// </summary>
    public partial class drawPage : Page
    {
        public drawPage()
        {
            InitializeComponent();
        }

        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGameButton.Visibility = Visibility.Collapsed;
            drawFrame.NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new Uri("gameGrid.xaml", UriKind.Relative));
            
        }
    }
}
