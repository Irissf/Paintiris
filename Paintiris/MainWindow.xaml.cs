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

namespace Paintiris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //*********** Variables **********************
        private int anchoLienzo = 500;
        private int altoLienzo = 500;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Canvas lienzo = new Canvas();
            lienzo.Background = Brushes.White;
            lienzo.Height = altoLienzo;
            lienzo.Width = anchoLienzo;
            folio.Children.Add(lienzo);
        }
    }
}
