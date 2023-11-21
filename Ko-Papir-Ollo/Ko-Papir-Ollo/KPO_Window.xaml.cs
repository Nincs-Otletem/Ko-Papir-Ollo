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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class KPO_Window : Window
    {
        string valasztott = "";
        string gep_valasztott = "";
        Random random = new Random();
        public KPO_Window()
        {
            InitializeComponent();
            KPO_valasztas.Items.Add("Kő");
            KPO_valasztas.Items.Add("Papír");
            KPO_valasztas.Items.Add("Olló");
        }

        private void KPO_valasztas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            valasztott = Convert.ToString(KPO_valasztas.SelectedValue);
        }

        private void Lock_in_button_Click(object sender, RoutedEventArgs e)
        {
            if (valasztott == "")
            {
                eredmenyek.Text += "Hiba! Válassz egy kézmozdulatot!\n";
            }
            else
            {
                int cucc = random.Next(1, 4);
                if (cucc == 1)
                {
                    gep_valasztott = "Kő";
                }
                if (cucc == 2)
                {
                    gep_valasztott = "Papír";
                }
                if (cucc == 3)
                {
                    gep_valasztott = "Olló";
                }
                if (gep_valasztott == valasztott)
                {
                    eredmenyek.Text += Convert.ToString($"Döntetlen mind a ketten {valasztott}-t választottatok!\n");
                }
                else
                {
                    if ((gep_valasztott == "Kő" && valasztott == "Olló") || (gep_valasztott == "Papír" && valasztott == "Kő") || (gep_valasztott == "Olló" && valasztott == "Papír"))
                    {
                        eredmenyek.Text += Convert.ToString($"Vesztettél! {gep_valasztott} > {valasztott}\n");
                    }
                    if ((valasztott == "Kő" && gep_valasztott == "Olló") || (valasztott == "Papír" && gep_valasztott == "Kő") || (valasztott == "Olló" && gep_valasztott == "Papír"))
                    {
                        eredmenyek.Text += Convert.ToString($"Nyertél! {valasztott} > {gep_valasztott}\n");
                    }
                }
            }
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
