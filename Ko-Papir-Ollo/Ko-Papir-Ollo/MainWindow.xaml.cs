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
        }

        private void kopapirollo_pipa_Checked(object sender, RoutedEventArgs e)
        {
            if (kopapirollopipa.IsChecked == true)
            {
                kopapirollopipa.IsChecked = false;
            }
        }

        private void kopapirollopipa_Checked(object sender, RoutedEventArgs e)
        {
            if (kopapirollo_pipa.IsChecked == true)
            {
                kopapirollo_pipa.IsChecked = false;
            }
        }
    }
}
