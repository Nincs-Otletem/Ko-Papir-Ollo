using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Ko_Papir_Ollo
{
    public partial class KPOGS_Page : Page
    {
        private string valasztott = "";
        private string gep_valasztott = "";
        private Random random = new Random();
        private bool IG = true;
        private string nyertes;
        private int GyoztesKor;
        private int VesztesKor;
        private int DontetlenKor;
        private int JatszottKor;
        private string playah;


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
            playah = jatekos;
        }

        void Ko_Onclick(object sender, RoutedEventArgs e)
        {
            valasztott = "Kő";
            playerImage.Source = new BitmapImage(new Uri("/image/ko.png", UriKind.Relative));
            Lock_in_KPOGS_Click();
        }

        void Papir_Onclick(object sender, RoutedEventArgs e)
        {
            valasztott = "Papír";
            playerImage.Source = new BitmapImage(new Uri("/image/papir.png", UriKind.Relative));
            Lock_in_KPOGS_Click();
        }

        void Ollo_Onclick(object sender, RoutedEventArgs e)
        {
            valasztott = "Olló";
            playerImage.Source = new BitmapImage(new Uri("/image/ollo.png", UriKind.Relative));
            Lock_in_KPOGS_Click();

        }

        void Gyik_Onclick(object sender, RoutedEventArgs e)
        {
            valasztott = "Gyík";
            playerImage.Source = new BitmapImage(new Uri("/image/gyik.png", UriKind.Relative));
            Lock_in_KPOGS_Click();

        }

        void Spock_Onclick(object sender, RoutedEventArgs e)
        {
            valasztott = "Spock";
            playerImage.Source = new BitmapImage(new Uri("/image/spock.png", UriKind.Relative));
            Lock_in_KPOGS_Click();

        }

        private void Lock_in_KPOGS_Click()
        {
            if (JatszottKor >= 3)
            {
                Console.Text = "Lejátszottad  a 3 kört ebben a játszmában!\nNyomj az újra gombra, vagy lépj vissza a menübe.";
            }
            else
            {
                if (CurrentGame.Visibility == Visibility.Hidden) CurrentGame.Visibility = Visibility.Visible;
                if (playerImage.Opacity != 1) playerImage.Opacity = 1;
                if (robotImage.Opacity != 1) robotImage.Opacity = 1;
                JatszottKor++;
                int cucc = random.Next(1, 6);
                switch (cucc)
                {
                    case 1:
                        gep_valasztott = "Kő";
                        robotImage.Source = new BitmapImage(new Uri("/image/ko.png", UriKind.Relative));
                        break;

                    case 2:
                        gep_valasztott = "Papír";
                        robotImage.Source = new BitmapImage(new Uri("/image/papir.png", UriKind.Relative));
                        break;

                    case 3:
                        gep_valasztott = "Olló";
                        robotImage.Source = new BitmapImage(new Uri("/image/ollo.png", UriKind.Relative));
                        break;

                    case 4:
                        gep_valasztott = "Gyík";
                        robotImage.Source = new BitmapImage(new Uri("/image/gyik.png", UriKind.Relative));
                        break;

                    case 5:
                        gep_valasztott = "Spock";
                        robotImage.Source = new BitmapImage(new Uri("/image/spock.png", UriKind.Relative));
                        break;
                }

                if (gep_valasztott == valasztott)
                {
                    DontetlenKor++;
                    Console.Text = Convert.ToString($"Döntetlen mind a ketten {valasztott}-t \n választottatok!\n");
                }
                else
                {
                    if ((gep_valasztott == "Kő" && (valasztott == "Olló" || valasztott == "Gyík")) || (gep_valasztott == "Papír" && (valasztott == "Kő" || valasztott == "Spock")) || (gep_valasztott == "Olló" && (valasztott == "Papír" || valasztott == "Gyík")) || (gep_valasztott == "Gyík" && (valasztott == "Papír" || valasztott == "Spock")) || (gep_valasztott == "Spock" && (valasztott == "Kő" || valasztott == "Olló")))
                    {
                        VesztesKor++;
                        Console.Text = Convert.ToString($"Vesztettél! {gep_valasztott} > {valasztott}\n");
                    }
                    else
                    {
                        GyoztesKor++;
                        Console.Text = Convert.ToString($"Nyertél! {valasztott} > {gep_valasztott}\n");
                    }
                }
                RoundVictory.Text = "Nyert kör: " + Convert.ToString(GyoztesKor);
                RoundDraw.Text = "Döntetlen kör: " + Convert.ToString(DontetlenKor);
                RoundDefeat.Text = "Vesztett kör: " + Convert.ToString(VesztesKor);
                JatekErtekelese();
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            NavigationService.Navigate(mainPage);
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            KPOGS_Page kpogs_Page = new KPOGS_Page(playah);
            NavigationService.Navigate(kpogs_Page);
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
            UserChoice.Text = jatekos + UserChoice.Text;
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
            TotalVictory.Text = TotalVictory.Text + Jatekoss.NyertJatek;
            TotalDraw.Text = TotalDraw.Text + Jatekoss.DontetlenJatek;
            TotalDefeat.Text = TotalDefeat.Text + Jatekoss.VesztettJatek;

        }
        private void JatekErtekelese()
        {
            if (IG == true && JatszottKor == 3)
            {
                IG = false;
                if (GyoztesKor > VesztesKor && GyoztesKor > DontetlenKor)
                {
                    Jatekoss.NyertJatek++;
                    nyertes = Jatekoss.Nev;
                }
                else if (VesztesKor > GyoztesKor && VesztesKor > DontetlenKor)
                {
                    Jatekoss.VesztettJatek++;
                    nyertes = "LaciBot2000";
                }
                else
                {
                    Jatekoss.DontetlenJatek++;
                    nyertes = "nincs";
                }
                CurrentGame.Visibility = Visibility.Hidden;
                Console.Margin = new Thickness(0, 0, 0, 0);
                Console.Text = Convert.ToString($"A játék abszolút győztese: {nyertes}!\nA játék véget ért, eredményed mentésre került.");
                TotalVictory.Text = "Nyert játék: " + Jatekoss.NyertJatek;
                TotalDraw.Text = "Döntetlen játék: " + Jatekoss.DontetlenJatek;
                TotalDefeat.Text = "Vesztett játék: " + Jatekoss.VesztettJatek;
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