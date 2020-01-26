using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using CarRentalVoiceAssistant.SQL;

namespace CarRentalVoiceAssistant
{
    class Assistant
    {
        static bool speechOn = true;
        static SpeechSynthesizer pTTS = new SpeechSynthesizer();
        static SpeechRecognitionEngine pSRE;

        private static void RentCarGrammar(Grammar grammarSystem) {
            pTTS.SpeakAsync("Witaj w asystencie");
            Choices chRent = new Choices();
            Choices chCar = new Choices();
            string[] rent = new string[] { "Wypożycz", "Rezerwuj", "Zarezerwuj" };
            string[] car = new string[] { "Pojazd", "Auto", "Samochód" };
            chRent.Add(rent);
            chCar.Add(car);
            GrammarBuilder grammarProgram = new GrammarBuilder();
            grammarProgram.Append(chRent);
            grammarProgram.Append(chCar);

            Grammar g_rentCar = new Grammar(grammarProgram);
            pSRE.LoadGrammarAsync(g_rentCar);
            pSRE.LoadGrammarAsync(grammarSystem);
        }

        private static void RentDatesGrammar(Grammar grammarSystem) {
            pTTS.SpeakAsync("W jakim terminie dokonać rezerwacji?");
            Choices chDay = new Choices();
            Choices chMonth = new Choices();
            Choices chFrom = new Choices();
            Choices chTo = new Choices();
            string[] days = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
            "11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
            "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"};
            string[] months = new string[] { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec",
            "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"};
            chDay.Add(days);
            chMonth.Add(months);
            chFrom.Add("Od");
            chTo.Add("Do");

            GrammarBuilder grammarFrom = new GrammarBuilder();
            grammarFrom.Append(chFrom);
            grammarFrom.Append(chDay);
            grammarFrom.Append(chMonth);

            GrammarBuilder grammarTo = new GrammarBuilder();
            grammarTo.Append(chTo);
            grammarTo.Append(chDay);
            grammarTo.Append(chMonth);

            Grammar g_rentDatesFrom = new Grammar(grammarFrom);
            Grammar g_rentDatesTo = new Grammar(grammarTo);
            pSRE.LoadGrammarAsync(g_rentDatesFrom);
            pSRE.LoadGrammarAsync(g_rentDatesTo);
            pSRE.LoadGrammarAsync(grammarSystem);
        }

        private static void ChooseCarGrammar(Grammar grammarSystem) {
            Choices chChoose = new Choices();
            Choices chCar = new Choices();
            Choices chMake = new Choices();
            Choices chModel = new Choices();
            string[] make = DAL.GetAllMake();
            string[] model = DAL.GetAllModel();
            string[] car = new string[] { "Pojazd", "Auto", "Samochód" };
            chChoose.Add("Wybierz");
            chCar.Add(car);
            chMake.Add(make);
            chModel.Add(model);
            GrammarBuilder grammarProgram = new GrammarBuilder();
            grammarProgram.Append(chChoose);
            grammarProgram.Append(chCar);
            grammarProgram.Append(chMake);
            grammarProgram.Append(chModel);

            Grammar g_chooseCar = new Grammar(grammarProgram);
            pSRE.LoadGrammarAsync(g_chooseCar);
            pSRE.LoadGrammarAsync(grammarSystem);
        }

        public static void LoadRentACarRecognition() {
            try
            {
                Grammar grammarSystem = InitializeAssistant();
                RentCarGrammar(grammarSystem);
                pSRE.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public static void LoadChooseCarRecognition() {
            try
            {
                Grammar grammarSystem = InitializeAssistant();
                ChooseCarGrammar(grammarSystem);
                pSRE.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public static void LoadRentDatesRecognition() {
            try {
                Grammar gramarSystem = InitializeAssistant();
                RentDatesGrammar(gramarSystem);
                pSRE.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex) {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private static void AssistantChooseCarResponse(String txt) {
            string[] words = txt.Split(' ');
            if (txt.IndexOf("Wybierz") >= 0 &&
                (txt.IndexOf("Pojazd") >= 0 || txt.IndexOf("Auto") >= 0 || txt.IndexOf("Samochód") >= 0) &&
                (speechOn == true)) {
                pTTS.SpeakAsync("Wybrano pojazd " + words[2] + words[3]);
                Reservation reservation = Reservation.Instance;

                reservation.Make = words[2];
                reservation.Model = words[3];

                Page newPage = new RentDates();
                Current current = Current.Instance;
                current.Page.NavigationService.Navigate(newPage);
                current.Page = newPage;
            }
        }

        private static void AssistantRentDatesResponse(String txt) {
            string[] words = txt.Split(' ');
            RentDates page = (RentDates)Current.Instance.Page;

            if ((txt.IndexOf("Od") >= 0) && (speechOn == true)) {
                pTTS.SpeakAsync("Wybrano daty Od");
                page.FromDate.Text = words[1] + words[2];
            }
            else if ((txt.IndexOf("Do") >= 0) && (speechOn == true))
            {
                pTTS.SpeakAsync("Wybrano daty Do");
                page.ToDate.Text = words[1] + words[2];
            }
        }

        private static void AssistantRentCarResponse(String txt) {
           if (((txt.IndexOf("Wypożycz") >= 0) || (txt.IndexOf("Rezerwuj") >= 0) || (txt.IndexOf("Zarezerwuj") >= 0)) && 
           ((txt.IndexOf("Pojazd") >= 0) || (txt.IndexOf("Auto") >= 0) || (txt.IndexOf("Samochód") >= 0)) &&
           (speechOn == true))
            {
                string[] words = txt.Split(' ');
                Current current = Current.Instance;
                CarChoose newPage = new CarChoose();
                current.Page.NavigationService.Navigate(newPage);
                current.Page = newPage;
                //pTTS.SpeakAsync("Wynik działania to: " + int.Parse(words[1]) + int.Parse(words[3]));
            }
        }

        private static void PSRE_SpeechRecognized(Object sender, SpeechRecognizedEventArgs e)
        {
            string txt = e.Result.Text;
            float confidence = e.Result.Confidence;
            if (confidence > 0.60)
            {
                AssistantServiceResponses(txt);
                AssistantRentCarResponse(txt);
                AssistantChooseCarResponse(txt);
                if (Current.Instance.Page is RentDates) {
                AssistantRentDatesResponse(txt);
                }

            }
            else
            {
                pTTS.SpeakAsync("Proszę powtórzyć");
            }
        }

        private static void AssistantServiceResponses(String txt)
        {
            Page currentPage = Current.Instance.Page;
            if (txt.IndexOf("Pomoc") >= 0)
            {
                if (currentPage is CarChoose)
                {
                    pTTS.SpeakAsync("Składnia polecenia: Wybierz pojazd marka model");
                }
                else if (currentPage is RentDates) {
                    pTTS.SpeakAsync("Składnia: Od/Do dzień miesiąc");
                }
            }
        }

        private static Grammar InitializeAssistant()
        {
            pTTS.SetOutputToDefaultAudioDevice();
            CultureInfo ci = new CultureInfo("pl-PL");
            pSRE = new SpeechRecognitionEngine(ci);
            pSRE.SetInputToDefaultAudioDevice();
            pSRE.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(PSRE_SpeechRecognized);
            Choices stopChoice = new Choices();
            stopChoice.Add("Pomoc");

            GrammarBuilder buildGrammarSystem = new GrammarBuilder();
            buildGrammarSystem.Append(stopChoice);
            Grammar grammarSystem = new Grammar(buildGrammarSystem);
            return grammarSystem;
        }
    }
}
