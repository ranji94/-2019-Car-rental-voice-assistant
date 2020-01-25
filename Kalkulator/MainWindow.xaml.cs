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
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System.Globalization;


namespace Kalkulator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool speechOn = true;
        static SpeechSynthesizer pTTS = new SpeechSynthesizer();
        static SpeechRecognitionEngine pSRE;
        int liczba1;
        int liczba2;
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                pTTS.SetOutputToDefaultAudioDevice();
                pTTS.Speak("Witam w kalkulatorze POWIEDZ OBLICZ");
                CultureInfo ci = new CultureInfo("pl-PL");
                pSRE = new SpeechRecognitionEngine(ci);
                pSRE.SetInputToDefaultAudioDevice();
                pSRE.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(PSRE_SpeechRecognized);
                Choices stopChoice = new Choices();
                stopChoice.Add("Stop");
                stopChoice.Add("Pomoc");

                GrammarBuilder buildGrammarSystem = new GrammarBuilder();
                buildGrammarSystem.Append(stopChoice);

                Grammar grammarSystem = new Grammar(buildGrammarSystem);
                Choices chNumbers = new Choices(); //możliwy wybór słów
                Choices chOperators = new Choices();
                string[] numbers = new string[] { "0", "1", "2", "3", "4", "5", "6", "7",
                    "8", "9" };
                string[] operators = new string[] { "plus", "minus", "razy", "przez" };
                chNumbers.Add(numbers);
                chOperators.Add(operators);
                GrammarBuilder grammarProgram = new GrammarBuilder();
                grammarProgram.Append("Oblicz");
                grammarProgram.Append(chNumbers);
                grammarProgram.Append(chOperators);
                grammarProgram.Append(chNumbers);

                Grammar g_WhatIsXplusY = new Grammar(grammarProgram);
                pSRE.LoadGrammarAsync(g_WhatIsXplusY);
                pSRE.LoadGrammarAsync(grammarSystem);

                pSRE.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private void PSRE_SpeechRecognized(Object sender, SpeechRecognizedEventArgs e) {
            string txt = e.Result.Text;
            float confidence = e.Result.Confidence;
            if (confidence > 0.30)
            {
                if (txt.IndexOf("Stop") >= 0)
                {
                    speechOn = false;
                }
                else if (txt.IndexOf("Pomoc") >= 0)
                {
                    pTTS.SpeakAsync("Składnia polecenia: Oblicz liczba plus liczba. Na przykład: dwa plus trzy");
                }
                else if ((txt.IndexOf("Oblicz") >= 0) && (txt.IndexOf("plus") >= 0) &&
               (speechOn == true))
                {
                    string[] words = txt.Split(' ');
                    this.liczba1 = int.Parse(words[1]);
                    this.liczba2 = int.Parse(words[3]);
                    int suma = liczba1 + liczba2;
                    Liczba1.Content = this.liczba1;
                    Liczba2.Content = this.liczba2;
                    Obliczenia.Text = this.liczba1 + "+" + this.liczba2 + "=" + suma;
                    pTTS.SpeakAsync("Wynik działania to: " + suma);

                    B1.IsEnabled = false;
                    B2.IsEnabled = false;
                    B3.IsEnabled = false;
                    B4.IsEnabled = false;
                    B5.IsEnabled = false;
                    B6.IsEnabled = false;
                    B7.IsEnabled = false;
                    B8.IsEnabled = false;
                    B9.IsEnabled = false;
                    B0.IsEnabled = false;
                    BPlus.IsEnabled = false;
                    BMinus.IsEnabled = false;
                    BDivide.IsEnabled = false;
                    BEquality.IsEnabled = false;
                    BMultiply.IsEnabled = false;
                }
                else if ((txt.IndexOf("Oblicz") >= 0) && (txt.IndexOf("minus") >= 0) &&
               (speechOn == true))
                {
                    string[] words = txt.Split(' ');
                    this.liczba1 = int.Parse(words[1]);
                    this.liczba2 = int.Parse(words[3]);
                    int roznica = liczba1 - liczba2;
                    Liczba1.Content = this.liczba1;
                    Liczba2.Content = this.liczba2;
                    Obliczenia.Text = this.liczba1 + "-" + this.liczba2 + "=" + roznica;
                    pTTS.SpeakAsync("Wynik działania to: " + roznica);

                    B1.IsEnabled = false;
                    B2.IsEnabled = false;
                    B3.IsEnabled = false;
                    B4.IsEnabled = false;
                    B5.IsEnabled = false;
                    B6.IsEnabled = false;
                    B7.IsEnabled = false;
                    B8.IsEnabled = false;
                    B9.IsEnabled = false;
                    B0.IsEnabled = false;
                    BPlus.IsEnabled = false;
                    BMinus.IsEnabled = false;
                    BDivide.IsEnabled = false;
                    BEquality.IsEnabled = false;
                    BMultiply.IsEnabled = false;
                }
                else if ((txt.IndexOf("Oblicz") >= 0) && (txt.IndexOf("razy") >= 0) &&
               (speechOn == true))
                {
                    string[] words = txt.Split(' ');
                    this.liczba1 = int.Parse(words[1]);
                    this.liczba2 = int.Parse(words[3]);
                    int iloczyn = liczba1 * liczba2;
                    Liczba1.Content = this.liczba1;
                    Liczba2.Content = this.liczba2;
                    Obliczenia.Text = this.liczba1 + "*" + this.liczba2 + "=" + iloczyn;
                    pTTS.SpeakAsync("Wynik działania to: " + iloczyn);

                    B1.IsEnabled = false;
                    B2.IsEnabled = false;
                    B3.IsEnabled = false;
                    B4.IsEnabled = false;
                    B5.IsEnabled = false;
                    B6.IsEnabled = false;
                    B7.IsEnabled = false;
                    B8.IsEnabled = false;
                    B9.IsEnabled = false;
                    B0.IsEnabled = false;
                    BPlus.IsEnabled = false;
                    BMinus.IsEnabled = false;
                    BDivide.IsEnabled = false;
                    BEquality.IsEnabled = false;
                    BMultiply.IsEnabled = false;
                }
                else if ((txt.IndexOf("Oblicz") >= 0) && (txt.IndexOf("przez") >= 0) &&
               (speechOn == true))
                {
                    string[] words = txt.Split(' ');
                    this.liczba1 = int.Parse(words[1]);
                    this.liczba2 = int.Parse(words[3]);
                    if (liczba2 != 0)
                    {
                        int iloraz = liczba1 / liczba2;
                        Liczba1.Content = this.liczba1;
                        Liczba2.Content = this.liczba2;
                        Obliczenia.Text = this.liczba1 + "/" + this.liczba2 + "=" + iloraz;
                        pTTS.SpeakAsync("Wynik działania to: " + iloraz);

                        B1.IsEnabled = false;
                        B2.IsEnabled = false;
                        B3.IsEnabled = false;
                        B4.IsEnabled = false;
                        B5.IsEnabled = false;
                        B6.IsEnabled = false;
                        B7.IsEnabled = false;
                        B8.IsEnabled = false;
                        B9.IsEnabled = false;
                        B0.IsEnabled = false;
                        BPlus.IsEnabled = false;
                        BMinus.IsEnabled = false;
                        BDivide.IsEnabled = false;
                        BEquality.IsEnabled = false;
                        BMultiply.IsEnabled = false;
                    }
                    else
                    {
                        pTTS.SpeakAsync("Cholero, nie dziel przez zero");
                    }
                }
            }
            else
            {
                pTTS.SpeakAsync("Proszę powtórzyć");
            }
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '1';
            if (Obliczenia.Text.Length > 1)
            {
                this.liczba2 = 1;
                Liczba2.Content = 1;
            }
            else {
                this.liczba1 = 1;
                Liczba1.Content = 1;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '2';
            if (Obliczenia.Text.Length > 1)
            {
                this.liczba2 = 2;
                Liczba2.Content = 2;
            }
            else
            {
                this.liczba1 = 2;
                Liczba1.Content = 2;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '3';
            if (Obliczenia.Text.Length > 1)
            {
                this.liczba2 = 3;
                Liczba2.Content = 3;
            }
            else
            {
                this.liczba1 = 3;
                Liczba1.Content = 3;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '4';
            if (Obliczenia.Text.Length > 1)
            {
                this.liczba2 = 4;
                Liczba2.Content = 4;
            }
            else
            {
                this.liczba1 = 4;
                Liczba1.Content = 4;
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '5';
            if (Obliczenia.Text.Length > 1)
            {
                this.liczba2 = 5;
                Liczba2.Content = 5;
            }
            else
            {
                this.liczba1 = 5;
                Liczba1.Content = 5;
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '6';
            if (Obliczenia.Text.Length > 1)
            {
                this.liczba2 = 6;
                Liczba2.Content = 6;
            }
            else
            {
                this.liczba1 = 6;
                Liczba1.Content = 6;
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '7';
            if (Obliczenia.Text.Length > 1)
            {
                this.liczba2 = 7;
                Liczba2.Content = 7;
            }
            else
            {
                this.liczba1 = 7;
                Liczba1.Content = 7;
            }
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '8';
            if (Obliczenia.Text.Length > 1)
            {
                this.liczba2 = 8;
                Liczba2.Content = 8;
            }
            else
            {
                this.liczba1 = 8;
                Liczba1.Content = 8;
            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '9';
            if (Obliczenia.Text.Length > 1)
            {
                this.liczba2 = 9;
                Liczba2.Content = 9;
            }
            else
            {
                this.liczba1 = 9;
                Liczba1.Content = 9;
            }
        }

        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '0';
            if (Obliczenia.Text.Length > 1)
            {
                this.liczba2 = 0;
                Liczba2.Content = 0;
            }
            else
            {
                this.liczba1 = 0;
                Liczba1.Content = 0;
            }
        }

        private void Button_Click_Plus(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '+';
            BPlus.IsEnabled = false;
            BMinus.IsEnabled = false;
            BDivide.IsEnabled = false;
            BMultiply.IsEnabled = false;
        }

        private void Button_Click_Multiply(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '*';
            BPlus.IsEnabled = false;
            BMinus.IsEnabled = false;
            BDivide.IsEnabled = false;
            BMultiply.IsEnabled = false;
        }

        private void Button_Click_Minus(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '-';
            BPlus.IsEnabled = false;
            BMinus.IsEnabled = false;
            BDivide.IsEnabled = false;
            BMultiply.IsEnabled = false;
        }

        private void Button_Click_Equality(object sender, RoutedEventArgs e)
        {

            if (Obliczenia.Text.Contains("+")) {
                Obliczenia.Text = this.liczba1 + "+" + this.liczba2 + "=" + (this.liczba1 + this.liczba2).ToString();
            }

            if (Obliczenia.Text.Contains("-"))
            {
                Obliczenia.Text = this.liczba1 + "-" + this.liczba2 + "=" + (this.liczba1 - this.liczba2).ToString();
            }

            if (Obliczenia.Text.Contains("*"))
            {
                Obliczenia.Text = this.liczba1 + "*" + this.liczba2 + "=" + (this.liczba1 * this.liczba2).ToString();
            }

            if (Obliczenia.Text.Contains("/"))
            {
                if (liczba2 != 0)
                    Obliczenia.Text = this.liczba1 +"/"+this.liczba2 +"="+(this.liczba1 / this.liczba2).ToString();
                else
                    Obliczenia.Text = "ERROR: Dzielenie przez zero";
            }

            B1.IsEnabled = false;
            B2.IsEnabled = false;
            B3.IsEnabled = false;
            B4.IsEnabled = false;
            B5.IsEnabled = false;
            B6.IsEnabled = false;
            B7.IsEnabled = false;
            B8.IsEnabled = false;
            B9.IsEnabled = false;
            B0.IsEnabled = false;
            BPlus.IsEnabled = false;
            BMinus.IsEnabled = false;
            BDivide.IsEnabled = false;
            BMultiply.IsEnabled = false;
            BEquality.IsEnabled = false;
            Liczba1.Content = "0";
            Liczba2.Content = "0";
        }

        private void Button_Click_Divide(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text += '/';
            BPlus.IsEnabled = false;
            BMinus.IsEnabled = false;
            BDivide.IsEnabled = false;
            BMultiply.IsEnabled = false;
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            Obliczenia.Text = "";
            B1.IsEnabled = true;
            B2.IsEnabled = true;
            B3.IsEnabled = true;
            B4.IsEnabled = true;
            B5.IsEnabled = true;
            B6.IsEnabled = true;
            B7.IsEnabled = true;
            B8.IsEnabled = true;
            B9.IsEnabled = true;
            B0.IsEnabled = true;
            BPlus.IsEnabled = true;
            BMinus.IsEnabled = true;
            BDivide.IsEnabled = true;
            BEquality.IsEnabled = true;
            BMultiply.IsEnabled = true;
        }
    }
}
