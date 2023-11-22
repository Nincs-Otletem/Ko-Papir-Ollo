using System;
using System.Windows;
using System.Windows.Controls;

namespace Ko_Papir_Ollo
{
    /// <summary>
    /// Interaction logic for KPOGS_Page.xaml
    /// </summary>
    public partial class KPOGS_Page : Page
    {
        string valasztott_KPOGS = "";
        string gep_valasztott_KPOGS = "";
        Random random_KPOGS = new Random();

        public KPOGS_Page()
        {
            InitializeComponent();
            KPOGS_valasztas.Items.Add("Kő");
            KPOGS_valasztas.Items.Add("Papír");
            KPOGS_valasztas.Items.Add("Olló");
            KPOGS_valasztas.Items.Add("Gyík");
            KPOGS_valasztas.Items.Add("Spock");
        }

        private void KPOGS_valasztas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            valasztott_KPOGS = Convert.ToString(KPOGS_valasztas.SelectedValue);
        }

        private void Lock_in_KPOGS_Click(object sender, RoutedEventArgs e)
        {
            if (valasztott_KPOGS == "")
            {
                jatek.Text += "Hiba! Válassz egy kézmozdulatot!\n";
            }
            else
            {
                int cucc = random_KPOGS.Next(1, 6);
                switch (cucc)
                {
                    case 1:
                        gep_valasztott_KPOGS = "Kő";
                        break;

                    case 2:
                        gep_valasztott_KPOGS = "Papír";
                        break;

                    case 3:
                        gep_valasztott_KPOGS = "Olló";
                        break;

                    case 4:
                        gep_valasztott_KPOGS = "Gyík";
                        break;

                    case 5:
                        gep_valasztott_KPOGS = "Spock";
                        break;
                }

                if (gep_valasztott_KPOGS == valasztott_KPOGS)
                {
                    jatek.Text += Convert.ToString($"Döntetlen mind a ketten {valasztott_KPOGS}-t választottatok!\n");
                }
                else
                {
                    if ((gep_valasztott_KPOGS == "Kő" && (valasztott_KPOGS == "Olló" || valasztott_KPOGS == "Gyík")) || (gep_valasztott_KPOGS == "Papír" && (valasztott_KPOGS == "Kő" || valasztott_KPOGS == "Spock")) || (gep_valasztott_KPOGS == "Olló" && (valasztott_KPOGS == "Papír" || valasztott_KPOGS == "Gyík")) || (gep_valasztott_KPOGS == "Gyík" && (valasztott_KPOGS == "Papír" || valasztott_KPOGS == "Spock")) || (gep_valasztott_KPOGS == "Spock" && (valasztott_KPOGS == "Kő" || valasztott_KPOGS == "Olló")))
                    {
                        jatek.Text += Convert.ToString($"Vesztettél! {gep_valasztott_KPOGS} > {valasztott_KPOGS}\n");
                    }
                    else
                    {
                        jatek.Text += Convert.ToString($"Nyertél! {valasztott_KPOGS} > {gep_valasztott_KPOGS}\n");
                    }
                }
            }
        }
    }
}
