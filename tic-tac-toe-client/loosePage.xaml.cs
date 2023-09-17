using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            looseFrame.Navigate(new Uri("gameGrid.xaml", UriKind.Relative));
        }
    }
}
