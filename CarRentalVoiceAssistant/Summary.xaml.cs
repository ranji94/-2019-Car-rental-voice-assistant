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
using CarRentalVoiceAssistant.SQL;

namespace CarRentalVoiceAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy Summary.xaml
    /// </summary>
    public partial class Summary : Page
    {
        public Summary()
        {
            InitializeComponent();
            Assistant.LoadSummaryRecognition();
            Reservation reservation = Reservation.Instance;
            Make.Content = reservation.Make;
            Model.Content = reservation.Model;
            PersonalName.Content = reservation.PersonalName;
            Surname.Content = reservation.Surname;
            FromDateSummary.Content = reservation.FromDate;
            ToDateSummary.Content = reservation.ToDate;
            String date = reservation.FromDate;
            DateTime fromDate = DateTime.Parse(date);
            date = reservation.ToDate;
            DateTime toDate = DateTime.Parse(date);
            TimeSpan expected = toDate - fromDate;
            decimal price = reservation.Price;
            decimal days = expected.Days;
            decimal paym = price * days;
            Price.Content = "Koszt za dobę: " + price + " zł";
            Payment.Content ="Koszt: "+ paym+" zł";
            reservation.Payment = paym;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void RentCar_Click(object sender, RoutedEventArgs e)
        {
            Reservation reservation = Reservation.Instance;
            DAL.RentCar(reservation.ID);
            String name = reservation.PersonalName + " " + reservation.Surname;
            DAL.CreateReservation(reservation.ID, reservation.FromDate, reservation.ToDate, name);
            Finish newPage = new Finish();
            this.NavigationService.Navigate(newPage);
            Current current = Current.Instance;
            current.Page = newPage;
        }
    }
}
