using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace Ko_Papir_Ollo
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            KPOButton.IsChecked = false;
            KPOGSButton.IsChecked = false;
            nevbox.Text = "";
            hiba.Content = "";
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var clickedButton = (ToggleButton)sender;

            foreach (var child in ((StackPanel)clickedButton.Parent).Children)
            {
                if (child is ToggleButton button && button != clickedButton)
                {
                    button.IsChecked = false;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (KPOButton.IsChecked == false && KPOGSButton.IsChecked == false) hiba.Content = "Válassz ki egy játékmódot!";

            if (nevbox.Text != "")
            {
                if (KPOButton.IsChecked == true)
                {
                    KPO_Page kpo_Page = new KPO_Page(nevbox.Text);
                    NavigationService.Navigate(kpo_Page);
                }
                if (KPOGSButton.IsChecked == true)
                {
                    KPOGS_Page kpogs_Page = new KPOGS_Page(nevbox.Text);
                    NavigationService.Navigate(kpogs_Page);
                }
            }
            else
            {
                hiba.Content = "Adj meg egy nevet!";
            }

        }
    }
}
