using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace CarRentalVoiceAssistant
{
    class Assistant
    {
        static bool speechOn = true;
        static SpeechSynthesizer pTTS = new SpeechSynthesizer();
        static SpeechRecognitionEngine pSRE;

        private static void RentCarGrammar(Grammar grammarSystem) {
            pTTS.SpeakAsync("Asystent Magierra, czym mogę pomóc");
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

        private static void FinishGrammar(Grammar grammarSystem) {
            Choices chExit = new Choices();
            Choices chAgain = new Choices();

            string[] exit = new string[] { "Zakończ", "Koniec", "Wyłącz", "Wyjdź" };
            string[] again = new string[] { "Ponownie", "Jeszcze", "Nowy", "Kolejny" };
            chExit.Add(exit);
            chAgain.Add(again);

            GrammarBuilder grammarExit = new GrammarBuilder();
            grammarExit.Append(chExit);

            GrammarBuilder grammarAgain = new GrammarBuilder();
            grammarAgain.Append(chAgain);

            Grammar g_exit = new Grammar(grammarExit);
            Grammar g_again = new Grammar(grammarAgain);
            pSRE.LoadGrammarAsync(g_exit);
            pSRE.LoadGrammarAsync(g_again);
            pSRE.LoadGrammarAsync(grammarSystem);
        }

        private static void SummaryGrammar(Grammar grammarSystem) {
            Reservation r = Reservation.Instance;
            pTTS.SpeakAsync("Twoje podsumowanie: "+r.Make+r.Model+r.FromDate+r.ToDate+"Na nazwisko"+r.Surname);
            Choices chRent = new Choices();
            string[] rent = new string[] { "Wynajmij", "Rezerwuj", "Pożycz", "Wypożycz" };
            chRent.Add(rent);
            GrammarBuilder grammarProgram = new GrammarBuilder();
            grammarProgram.Append(chRent);

            Grammar g_summary = new Grammar(grammarProgram);
            pSRE.LoadGrammarAsync(g_summary);
            pSRE.LoadGrammarAsync(grammarSystem);
        }

        private static void PersonalDataGrammar(Grammar grammarSystem) {
            pTTS.SpeakAsync("Podaj dane");
            Choices chName = new Choices();
            Choices chSurname = new Choices();

            string[] names = new string[] { "Jędrzej", "Adrian", "Tomasz", "Adam", "Jan", "Krzysztof", "Maciej" };
            string[] surNames = new string[] { "Piasecki", "Stefaniak", "Pałys", "Małysz", "Nowak", "Kowalski","Kononowicz", "Roszkowski" };

            chName.Add(names);
            chSurname.Add(surNames);
            GrammarBuilder grammarProgram = new GrammarBuilder();
            grammarProgram.Append(chName);
            grammarProgram.Append(chSurname);

            Grammar g_personalData = new Grammar(grammarProgram);
            pSRE.LoadGrammarAsync(g_personalData);
            pSRE.LoadGrammarAsync(grammarSystem);
        }

        private static void RentDatesGrammar(Grammar grammarSystem) {
            pTTS.SpeakAsync("Podaj termin rezerwacji");
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
            pTTS.SpeakAsync("Wybierz pojazd");
            Choices chChoose = new Choices();
            Choices chCar = new Choices();
            Choices chMake = new Choices();
            Choices chModel = new Choices();
            string[] make = new string[] { "Opel", "BMW", "Audi", "Toyota" };
            string[] model = new string[] { "Astra", "Vectra", "Corsa", "Corolla" };
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
                Console.WriteLine(ex);
            }
        }

        public static void LoadFinishRecognition()
        {
            try
            {
                Grammar grammarSystem = InitializeAssistant();
                FinishGrammar(grammarSystem);
                pSRE.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void LoadSummaryRecognition() {
            try
            {
                Grammar grammarSystem = InitializeAssistant();
                SummaryGrammar(grammarSystem);
                pSRE.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void LoadPersonalDataRecognition() {
            try
            {
                Grammar grammarSystem = InitializeAssistant();
                PersonalDataGrammar(grammarSystem);
                pSRE.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
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
                Console.WriteLine(ex);
            }
        }

        public static void LoadRentDatesRecognition() {
            try {
                Grammar gramarSystem = InitializeAssistant();
                RentDatesGrammar(gramarSystem);
                pSRE.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
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

        private static void AssistantFinishResponse(String txt) {
            Current current = Current.Instance;
            string[] words = txt.Split(' ');
            if ((txt.IndexOf("Zakończ") >= 0 ||
                (txt.IndexOf("Koniec") >= 0) ||
                (txt.IndexOf("Wyłącz") >= 0) ||
                (txt.IndexOf("Wyjdź") >= 0))
                && (speechOn == true)) {
                Application.Current.Shutdown();
            }
        }

        private static void AssistantAgainResponse(String txt)
        {
            Current current = Current.Instance;
            string[] words = txt.Split(' ');
            if ((txt.IndexOf("Jeszcze") >= 0 ||
                (txt.IndexOf("Ponownie") >= 0) ||
                (txt.IndexOf("Nowy") >= 0) ||
                (txt.IndexOf("Kolejny") >= 0))
                && (speechOn == true))
            {
                current.Page.NavigationService.Navigate(new WelcomeScreen());
            }
        }

        private static void AssistantPersonalDataResponse(String txt) {
            string[] words = txt.Split(' ');
            Reservation reservation = Reservation.Instance;

            PersonalData p = (PersonalData)Current.Instance.Page;

            if (((txt.IndexOf("Jędrzej") >= 0) || 
                (txt.IndexOf("Adrian") >= 0) ||
                (txt.IndexOf("Tomasz") >= 0) ||
                (txt.IndexOf("Jan") >= 0) ||
                (txt.IndexOf("Krzysztof") >= 0) ||
                (txt.IndexOf("Adam") >= 0) ||
                (txt.IndexOf("Maciej") >= 0)) && 
                ((txt.IndexOf("Piasecki") >= 0) || 
                (txt.IndexOf("Stefaniak") >= 0) ||
                (txt.IndexOf("Pałys") >= 0) ||
                (txt.IndexOf("Nowak") >= 0) ||
                (txt.IndexOf("Kowalski") >= 0) ||
                (txt.IndexOf("Małysz") >= 0) ||
                (txt.IndexOf("Kononowicz") >= 0) ||
                (txt.IndexOf("Roszkowski") >= 0)) &&
                (speechOn=true))
            {
                p.PersonalName.Text = words[0];
                p.PersonalSurname.Text = words[1];

                reservation.PersonalName = words[0];
                reservation.Surname = words[1];
                Summary summary = new Summary();
                p.NavigationService.Navigate(summary);
                Current.Instance.Page = summary;
            }
        }

        private static void AssistantRentDatesResponse(String txt)
        {
            string[] words = txt.Split(' ');
            RentDates page = (RentDates)Current.Instance.Page;
            Reservation reservation = Reservation.Instance;
            DateTime dateTime;

            if ((txt.IndexOf("Od") >= 0) && !(txt.IndexOf("Rezerwacja") >= 0) && (speechOn == true))
            {
                pTTS.SpeakAsync("Rezerwacja od daty "+words[1] + words[2] );
                page.FromDate.Text = words[1] + words[2];
                reservation.FromDate = page.FromDate.Text;
            }
            else if (DateTime.TryParse(page.FromDate.Text, out dateTime)) { 
                if ((txt.IndexOf("Do") >= 0) && !(txt.IndexOf("Rezerwacja") >= 0) && (speechOn == true))
                {
                    pTTS.SpeakAsync("Rezerwacja do daty " + words[1] + words[2]);
                    page.ToDate.Text = words[1] + words[2];
                    reservation.ToDate = page.ToDate.Text;
                    PersonalData newPage = new PersonalData();
                    page.NavigationService.Navigate(newPage);
                    Current.Instance.Page = newPage;
                }
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
            }
        }

        private static void AssistantSummaryResponse(String txt) {
            if (((txt.IndexOf("Rezerwacja") >= 0) ||
                (txt.IndexOf("Rezerwuj") >= 0) ||
                (txt.IndexOf("Pożycz") >= 0) ||
                (txt.IndexOf("Wypożycz") >= 0) ||
                (txt.IndexOf("Wynajmij") >= 0)) && 
                (speechOn == true)) 
            {
                Current current = Current.Instance;
                Finish finish = new Finish();
                current.Page.NavigationService.Navigate(finish);
                current.Page = finish;
            }
        }

        private static void PSRE_SpeechRecognized(Object sender, SpeechRecognizedEventArgs e)
        {
            string txt = e.Result.Text;
            float confidence = e.Result.Confidence;
            Current current = Current.Instance;
            Page p = current.Page;
            if (confidence > 0.40)
            {
                AssistantServiceResponses(txt);
                if (p is WelcomeScreen) { 
                    AssistantRentCarResponse(txt);
                }

                if (p is Summary) {
                    AssistantSummaryResponse(txt);
                }

                if (p is CarChoose) { 
                    AssistantChooseCarResponse(txt);
                }

                if (p is RentDates) {
                    AssistantRentDatesResponse(txt);
                }
                if (p is PersonalData) {
                    AssistantPersonalDataResponse(txt);
                }
                if (p is Finish) {
                    AssistantFinishResponse(txt);
                    AssistantAgainResponse(txt);
                }
            }
            else
            {
                if (p is WelcomeScreen)
                {
                    ((WelcomeScreen)p).Message.Content = "Proszę powtórzyć";
                    ((WelcomeScreen)p).Message.Visibility = Visibility.Visible;
                }
                if (p is CarChoose)
                {
                    ((CarChoose)p).Message.Content = "Proszę powtórzyć";
                    ((CarChoose)p).Message.Visibility = Visibility.Visible;
                }
                if (p is RentDates)
                {
                    ((RentDates)p).Message.Content = "Proszę powtórzyć";
                    ((RentDates)p).Message.Visibility = Visibility.Visible;
                }
                if (p is PersonalData)
                {
                    ((PersonalData)p).Message.Content = "Proszę powtórzyć";
                    ((PersonalData)p).Message.Visibility = Visibility.Visible;
                }
                if (p is Summary)
                {
                    ((Summary)p).Message.Content = "Proszę powtórzyć";
                    ((Summary)p).Message.Visibility = Visibility.Visible;
                }
                if (p is Finish)
                {
                    ((Finish)p).Message.Content = "Proszę powtórzyć";
                    ((Finish)p).Message.Visibility = Visibility.Visible;
                }
            }
        }

        private static void AssistantServiceResponses(String txt)
        {
            Page p = Current.Instance.Page;
            if (txt.IndexOf("Pomoc") >= 0)
            {
                if (p is WelcomeScreen)
                {
                    ((WelcomeScreen)p).Message.Content = "Powiedz na przykład 'Wypożycz pojazd'";
                    ((WelcomeScreen)p).Message.Visibility = Visibility.Visible;
                }
                if (p is CarChoose)
                {
                    ((CarChoose)p).Message.Content = "Przykładowa składnia: 'Wybierz pojazd Opel Astra'";
                    ((CarChoose)p).Message.Visibility = Visibility.Visible;
                }
                if (p is RentDates)
                {
                    ((RentDates)p).Message.Content = "Przykładowe składnie: 'Od czternasty luty', 'Do piąty marzec'";
                    ((RentDates)p).Message.Visibility = Visibility.Visible;
                }
                if (p is PersonalData)
                {
                    ((PersonalData)p).Message.Content = "Przykładowa składnia: 'Nazywam się Jan Nowak'";
                    ((PersonalData)p).Message.Visibility = Visibility.Visible;
                }
                if (p is Summary)
                {
                    ((Summary)p).Message.Content = "Przykładowa składnia: 'Wynajmij pojazd' ";
                    ((Summary)p).Message.Visibility = Visibility.Visible;
                }
                if (p is Finish)
                {
                    ((Finish)p).Message.Content = "Przykładowa składnia: 'Zakończ', 'Wynajmij kolejny'";
                    ((Finish)p).Message.Visibility = Visibility.Visible;
                }
            }
        }

        private static Grammar InitializeAssistant()
        {
            pTTS = new SpeechSynthesizer();
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
