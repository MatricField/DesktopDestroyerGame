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

namespace DesktopDestroyer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly RoutedCommand CmdStartGame = new RoutedCommand();

        private GameState gameState;

        public MainWindow()
        {
            gameState = GameState.WelcomeScreen;
            InitializeComponent();
        }

        private async void StartGame(object sender, ExecutedRoutedEventArgs e)
        {
            Hide();
            await Task.Delay(200);
            var nextPage = new PlayfieldPage();
            Content = nextPage;
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            Show();
            gameState = GameState.Started;
        }

        private void CanStartGame(object sender, CanExecuteRoutedEventArgs e)
        {
            switch(gameState)
            {
                case GameState.WelcomeScreen:
                    e.CanExecute = true;
                    break;
                default:
                    e.CanExecute = false;
                    break;
            }
        }

        private enum GameState
        {
            WelcomeScreen,
            Started
        }
    }
}
