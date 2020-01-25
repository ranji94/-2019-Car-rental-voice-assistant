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


namespace CarRentalVoiceAssistant
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
            Loaded += MyWindow_Loaded;

            try
            {
                pTTS.SetOutputToDefaultAudioDevice();
                pTTS.Speak("Asystent Magierra, w czym mogę pomóc");
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
                // Currently nothing
            }
        }

        private void MyWindow_Loaded(Object sender, RoutedEventArgs e) {
            frame.NavigationService.Navigate(new welcomeScreen());
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
                    pTTS.SpeakAsync("Wynik działania to: " + suma);
                }
            }
            else
            {
                pTTS.SpeakAsync("Proszę powtórzyć");
            }
        }
    }
}
