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
using static Ko_Papir_Ollo.KPO_Page;

namespace Ko_Papir_Ollo
{
    public partial class KPO_Page : Page
    {
        private string valasztott = "";
        private string gep_valasztott = "";
        private Random random = new Random();

        // Példa egy Jatekos osztályra, ahogy a kódból sejthető
        public class Jatekos
        {
            public string Nev { get; set; }
            public int NyertJatek { get; set; }
            public int VesztettJatek { get; set; }
            public int DontetlenJatek { get; set; }
            public int NyertKor { get; set; }
            public int VesztettKor { get; set; }
            public int DontetlenKor { get; set; }


            public Jatekos(string sor)
            {
                string[] adatok = sor.Split(';');
                Nev = adatok[0];
                NyertJatek = Convert.ToInt32(adatok[1]);
                VesztettJatek = Convert.ToInt32(adatok[2]);
                DontetlenJatek = Convert.ToInt32(adatok[3]);
                NyertKor = 0;
                VesztettKor = 0;
                DontetlenKor = 0;
            }

            public string Sorra()
            {
                return $"{Nev};{NyertJatek};{VesztettJatek};{DontetlenJatek}\n";
            }
        }
        private Jatekos Jatekoss { get; set; }
        private List<Jatekos> Jatekosok = new List<Jatekos>();
        public KPO_Page(string jatekos)
        {
            InitializeComponent();
            JatekosokBetoltese();
            JatekosBelepese(jatekos);
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
                jatek.Text += "Hiba! Válassz egy kézmozdulatot!\n";
            }
            else
            {
                int cucc = random.Next(1, 4);
                switch (cucc)
                {
                    case 1:
                        gep_valasztott = "Kő";
                        break;

                    case 2:
                        gep_valasztott = "Papír";
                        break;

                    case 3:
                        gep_valasztott = "Olló";
                        break;
                }

                if (gep_valasztott == valasztott)
                {
                    jatek.Text += Convert.ToString($"Döntetlen mind a ketten {valasztott}-t választottatok!\n");
                }
                else
                {
                    if ((gep_valasztott == "Kő" && valasztott == "Olló") || (gep_valasztott == "Papír" && valasztott == "Kő") || (gep_valasztott == "Olló" && valasztott == "Papír"))
                    {
                        jatek.Text += Convert.ToString($"Vesztettél! {gep_valasztott} > {valasztott}\n");
                    }
                    if ((valasztott == "Kő" && gep_valasztott == "Olló") || (valasztott == "Papír" && gep_valasztott == "Kő") || (valasztott == "Olló" && gep_valasztott == "Papír"))
                    {
                        jatek.Text += Convert.ToString($"Nyertél! {valasztott} > {gep_valasztott}\n");
                    }
                }
                JatekosokMentese();
            }
        }

        private void JatekosokBetoltese()
        {
            foreach (string sor in File.ReadAllLines("jatekosok.txt"))
            {
                Jatekosok.Add(new Jatekos(sor));
            }
        }
        private void JatekosBelepese(string jatekos)
        {
            UserStats.Text = jatekos + UserStats.Text;

            Jatekoss = Jatekosok.Find(x => x.Nev == jatekos);
            if (Jatekoss == null)
            {
                try
                {
                    Jatekoss = new Jatekos(jatekos);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt a Jatekos konstruktorban: {ex.Message}");
                }
                Jatekosok.Add(Jatekoss);

            }
        }

        private void JatekosokMentese()
        {
            List<string> sorok = new List<string>();
            foreach (Jatekos jatekos in Jatekosok) sorok.Add(jatekos.Sorra());
            File.WriteAllLines("jatekosok.txt", sorok);
            MessageBox.Show("Az adatok sikeresen mentve lettek.");

        }
    }
}
