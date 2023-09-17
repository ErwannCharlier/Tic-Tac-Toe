using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.SetOut(new ConsoleTextWriter(textBlockConsoleOutput));
            mainFrame.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }

    }
    public class ConsoleTextWriter : System.IO.TextWriter
    {
        private TextBlock _output;

        public ConsoleTextWriter(TextBlock output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            _output.Dispatcher.Invoke(() => _output.Text += value);
        }

        public override void Write(string value)
        {
            _output.Dispatcher.Invoke(() => _output.Text += value);
        }

        public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;
    }






}
