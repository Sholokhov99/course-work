using System;
using System.Collections.Generic;
using System.IO.Packaging;
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

namespace VideoPlay
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string wayVideoFile)
        {
            InitializeComponent();
            this.Topmost = true;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            MessageBox.Show(wayVideoFile);
            mediaContent.Source = new Uri(wayVideoFile);
            mediaContent.Play();
        }


        private void PauseMedia_Click(object sender, RoutedEventArgs e)
        {
            mediaContent.Pause();
        }

        private void PlayMedia_Click(object sender, RoutedEventArgs e)
        {
            mediaContent.Play();
        }
    }
}
