using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Ko_Papir_Ollo
{
    public partial class KPO_Page : Page
    {
        private string valasztott = "";
        private string gep_valasztott = "";
        private Random random = new Random();
        private int KorPont;
        private int JatszottKor;
        private bool IG = true;
        private string nyertes;

        public class Jatekos
        {
            public string Nev { get; set; }
            public int NyertJatek { get; set; }
            public int VesztettJatek { get; set; }
            public int DontetlenJatek { get; set; }

            // Konstruktor a név alapján
            public Jatekos(string nev)
            {
                Nev = nev;
                NyertJatek = 0;
                VesztettJatek = 0;
                DontetlenJatek = 0;
            }

            // Konstruktor a sor alapján
            public Jatekos(string sor, bool torol)
            {
                string[] adatok = sor.Split(';');
                Nev = adatok[0];
                NyertJatek = torol ? 0 : Convert.ToInt32(adatok[1]);
                VesztettJatek = torol ? 0 : Convert.ToInt32(adatok[2]);
                DontetlenJatek = torol ? 0 : Convert.ToInt32(adatok[3]);
            }

            public string Sorra()
            {
                return $"{Nev};{NyertJatek};{VesztettJatek};{DontetlenJatek}";
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
            hiba.Content = "";
            valasztott = Convert.ToString(KPO_valasztas.SelectedValue);
        }

        void Ko_Onclick(object sender, RoutedEventArgs e)
        {
            valasztott = "Kő";
            hiba.Content = "";
            Lock_in_button_Click();
        }

        void Papir_Onclick(object sender, RoutedEventArgs e)
        {
            valasztott = "Papír";
            hiba.Content = "";
            Lock_in_button_Click();
        }

        void Ollo_Onclick(object sender, RoutedEventArgs e)
        {
            valasztott = "Olló";
            hiba.Content = "";
            Lock_in_button_Click();

        }

        private void Lock_in_button_Click()
        {
            if (valasztott == "" && JatszottKor < 3)
            {
                hiba.Content = "Hiba! Válassz egy kézmozdulatot!\n";
            }
            else if (JatszottKor >= 3)
            {          
                hiba.Content = "Lejátszottad  a 3 kört ebben a játszmában!\n";
            }
            else
            {
                JatszottKor++;
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
                    //jatek.Text += Convert.ToString($"Döntetlen mind a ketten {valasztott}-t választottatok!\n");
                }
                else
                {
                    if ((gep_valasztott == "Kő" && valasztott == "Olló") || (gep_valasztott == "Papír" && valasztott == "Kő") || (gep_valasztott == "Olló" && valasztott == "Papír"))
                    {
                        KorPont--;
                        //jatek.Text += Convert.ToString($"Vesztettél! {gep_valasztott} > {valasztott}\n");
                    }
                    if ((valasztott == "Kő" && gep_valasztott == "Olló") || (valasztott == "Papír" && gep_valasztott == "Kő") || (valasztott == "Olló" && gep_valasztott == "Papír"))
                    {
                        KorPont++;
                        //jatek.Text += Convert.ToString($"Nyertél! {valasztott} > {gep_valasztott}\n");
                    }
                }
                JatekErtekelese();
            }
        }

        private void JatekosokBetoltese()
        {
            foreach (string sor in File.ReadAllLines("jatekosok.txt"))
            {
                Jatekosok.Add(new Jatekos(sor, false));
                //MessageBox.Show(sor);
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
                    Jatekosok.Add(Jatekoss);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Hibás formátumú adatok a játékos létrehozásánál.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba történt a Jatekos konstruktorban: {ex.Message}");
                }
            }
        }

        private void JatekErtekelese()
        {
            if (IG == true && JatszottKor == 3)
            {
                IG = false;
                if (KorPont > 0)
                {
                    Jatekoss.NyertJatek++;
                    nyertes = Jatekoss.Nev;
                }
                else if (KorPont < 0)
                {
                    Jatekoss.VesztettJatek++;
                    nyertes = "LaciBot2000";
                }
                else 
                {
                    Jatekoss.DontetlenJatek++;
                    nyertes = "nincs";
                }
                MessageBox.Show($"A játék abszolút győztese: {nyertes}!");
                //jatek.Text += Convert.ToString($"A játék véget ért, eredményed mentésre került.\n");
                JatekosokMentese();
            }

        }

        private void JatekosokMentese()
        {
            List<string> sorok = new List<string>();
            foreach (Jatekos jatekos in Jatekosok) sorok.Add(jatekos.Sorra());
            File.WriteAllLines("jatekosok.txt", sorok);
        }
    }
}
