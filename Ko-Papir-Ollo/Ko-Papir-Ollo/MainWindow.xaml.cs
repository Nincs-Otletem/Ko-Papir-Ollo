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

namespace Ko_Papir_Ollo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            kopapirollopipa.IsChecked = true ;
            kopapirollo_pipa.IsChecked = false;
            nevbox.Text = "";
            hiba.Content = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nevbox.Text != "")
            {
                Window1 window1 = new Window1();
                this.Close();
                window1.Show();
            }
            else
            {
                hiba.Content = "Adj meg egy nevet!!";
            }
        }
    }
}
