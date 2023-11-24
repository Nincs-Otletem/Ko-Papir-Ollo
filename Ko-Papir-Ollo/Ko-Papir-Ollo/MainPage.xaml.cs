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
using System.IO;

namespace Ko_Papir_Ollo
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            KPO_pipa.IsChecked = true;
            KPOGS_pipa.IsChecked = false;
            nevbox.Text = "";
            hiba.Content = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nevbox.Text != "")
            {
                if (KPO_pipa.IsChecked == true)
                {                
                    KPO_Page kpo_Page = new KPO_Page(nevbox.Text);
                    NavigationService.Navigate(kpo_Page); 
                }
                if (KPOGS_pipa.IsChecked == true)
                {
                    KPOGS_Page kpogs_Page = new KPOGS_Page(nevbox.Text);
                    NavigationService.Navigate(kpogs_Page);
                }
            }
            else
            {
                hiba.Content = "Adj meg egy nevet!!";
            }
        }
    }
}
