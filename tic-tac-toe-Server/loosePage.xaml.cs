using System;
using System.Windows;
using System.Windows.Controls;

namespace tictactoe_interface
{
    /// <summary>
    /// Logique d'interaction pour loosePage.xaml
    /// </summary>
    public partial class loosePage : Page
    {
        public loosePage()
        {
            InitializeComponent();
        }

        private void RestartGameButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGameButton.Visibility = Visibility.Collapsed;
            
            looseFrame.NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new Uri("gameGrid.xaml", UriKind.Relative));
        }
    }
}
