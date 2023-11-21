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
            MainPage mainPage = new MainPage();
            MainFrame.Navigate(mainPage);
            
        }

<<<<<<< Updated upstream
=======
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nevbox.Text != "")
            {
                if (KPO_pipa.IsChecked == true)
                {
                    KPO_Window KPOWindow = new KPO_Window();
                    this.Close();
                    //KPOWindow.Show();
                    var Page_1 = new Page1();
                    Page_1.Show();
                }
                if (KPOGS_pipa.IsChecked == true)
                {
                    KPOGS_Window KPOGSWindow = new KPOGS_Window();
                    this.Close();
                    KPOGSWindow.Show();
                }
            }
            else
            {
                hiba.Content = "Adj meg egy nevet!!";
            }
        }
>>>>>>> Stashed changes
    }
}
