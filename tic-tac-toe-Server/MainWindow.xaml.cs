using System;
using System.Windows;

namespace tictactoe_interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }

    }
    

}
