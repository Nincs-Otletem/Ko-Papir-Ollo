using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Ko_Papir_Ollo
{
    public partial class KPOGS_Page : Page
    {
        string valasztott = "";
        string gep_valasztott = "";
        Random random = new Random();
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
        public KPOGS_Page(string jatekos)
        {
            InitializeComponent();
            JatekosokBetoltese();
            JatekosBelepese(jatekos);
            KPOGS_valasztas.Items.Add("Kő");
            KPOGS_valasztas.Items.Add("Papír");
            KPOGS_valasztas.Items.Add("Olló");
            KPOGS_valasztas.Items.Add("Gyík");
            KPOGS_valasztas.Items.Add("Spock");
        }

        private void KPOGS_valasztas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            valasztott = Convert.ToString(KPOGS_valasztas.SelectedValue);
        }

        private void Lock_in_KPOGS_Click(object sender, RoutedEventArgs e)
        {
            if (valasztott == "" && JatszottKor < 5)
            {
                jatek.Text += "Hiba! Válassz egy kézmozdulatot!\n";
            }
            else if (JatszottKor >= 5)
            {
                jatek.Text += "Lejátszottad az 5 kört ebben a játszmában!\n";
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

                    case 4:
                        gep_valasztott = "Gyík";
                        break;

                    case 5:
                        gep_valasztott = "Spock";
                        break;
                }

                if (gep_valasztott == valasztott)
                {
                    jatek.Text += Convert.ToString($"Döntetlen mind a ketten {valasztott}-t választottatok!\n");
                }
                else
                {
                    if ((gep_valasztott == "Kő" && (valasztott == "Olló" || valasztott == "Gyík")) || (gep_valasztott == "Papír" && (valasztott == "Kő" || valasztott == "Spock")) || (gep_valasztott == "Olló" && (valasztott == "Papír" || valasztott == "Gyík")) || (gep_valasztott == "Gyík" && (valasztott == "Papír" || valasztott == "Spock")) || (gep_valasztott == "Spock" && (valasztott == "Kő" || valasztott == "Olló")))
                    {
                        KorPont--;
                        jatek.Text += Convert.ToString($"Vesztettél! {gep_valasztott} > {valasztott}\n");
                    }
                    else
                    {
                        KorPont++;
                        jatek.Text += Convert.ToString($"Nyertél! {valasztott} > {gep_valasztott}\n");
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
            if (IG == true && JatszottKor == 5)
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
                jatek.Text += Convert.ToString($"A játék véget ért, eredményed mentésre került.\n");
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
