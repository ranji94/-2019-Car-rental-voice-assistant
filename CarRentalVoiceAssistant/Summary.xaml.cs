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
            Reservation reservation = Reservation.Instance;
            Make.Content = reservation.Make;
            Model.Content = reservation.Model;
            PersonalName.Content = reservation.PersonalName;
            Surname.Content = reservation.Surname;
            FromDateSummary.Content = reservation.FromDate;
            ToDateSummary.Content = reservation.ToDate;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void RentCar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Finish());
        }
    }
}
