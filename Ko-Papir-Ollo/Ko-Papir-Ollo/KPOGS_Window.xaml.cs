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
using System.Windows.Shapes;

namespace Ko_Papir_Ollo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class KPOGS_Window : Window
    {
        string valasztott_KPOGS = "";
        string gep_valasztott_KPOGS = "";
        Random random_KPOGS = new Random();
        public KPOGS_Window()
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
                eredmeny_KPOGS.Text += "Hiba! Válassz egy kézmozdulatot!\n";
            }
            else
            {
                int cucc = random_KPOGS.Next(1, 6);
                if (cucc == 1)
                {
                    gep_valasztott_KPOGS = "Kő";
                }
                if (cucc == 2)
                {
                    gep_valasztott_KPOGS = "Papír";
                }
                if (cucc == 3)
                {
                    gep_valasztott_KPOGS = "Olló";
                }
                if (cucc == 4)
                {
                    gep_valasztott_KPOGS = "Gyík";
                }
                if (cucc == 5)
                {
                    gep_valasztott_KPOGS = "Spock";
                }
                if (gep_valasztott_KPOGS == valasztott_KPOGS)
                {
                    eredmeny_KPOGS.Text += Convert.ToString($"Döntetlen mind a ketten {valasztott_KPOGS}-t választottatok!\n");
                }
                else
                {
                    if ((gep_valasztott_KPOGS == "Kő" && (valasztott_KPOGS == "Olló" || valasztott_KPOGS == "Gyík")) || (gep_valasztott_KPOGS == "Papír" && (valasztott_KPOGS == "Kő" || valasztott_KPOGS == "Spock")) || (gep_valasztott_KPOGS == "Olló" && (valasztott_KPOGS == "Papír" || valasztott_KPOGS == "Gyík")) || (gep_valasztott_KPOGS == "Gyík" && (valasztott_KPOGS == "Papír" || valasztott_KPOGS == "Spock")) || (gep_valasztott_KPOGS == "Spock" && (valasztott_KPOGS == "Kő" || valasztott_KPOGS == "Olló")))
                    {
                        eredmeny_KPOGS.Text += Convert.ToString($"Vesztettél! {gep_valasztott_KPOGS} > {valasztott_KPOGS}\n");
                    }
                    if ((valasztott_KPOGS == "Kő" && (gep_valasztott_KPOGS == "Olló" || gep_valasztott_KPOGS == "Gyík")) || (valasztott_KPOGS == "Papír" && (gep_valasztott_KPOGS == "Kő" || gep_valasztott_KPOGS == "Spock")) || (valasztott_KPOGS == "Olló" && (gep_valasztott_KPOGS == "Papír" || gep_valasztott_KPOGS == "Gyík")) || (valasztott_KPOGS == "Gyík" && (gep_valasztott_KPOGS == "Papír" || gep_valasztott_KPOGS == "Spock")) || (valasztott_KPOGS == "Spock" && (gep_valasztott_KPOGS == "Kő" || gep_valasztott_KPOGS == "Olló")))
                    {
                        eredmeny_KPOGS.Text += Convert.ToString($"Nyertél! {valasztott_KPOGS} > {gep_valasztott_KPOGS}\n");
                    }
                }
            }
        }
    }
}
