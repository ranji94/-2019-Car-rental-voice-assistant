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
    /// Logika interakcji dla klasy rentDates.xaml
    /// </summary>
    public partial class rentDates : Page
    {
        public rentDates()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void ToDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime temp1, temp2;
            if (FromDate.SelectedDate != null) {
                if (DateTime.TryParse(FromDate.Text, out temp1) && DateTime.TryParse(ToDate.Text, out temp2))
                {
                    Next.IsEnabled = true;
                }
            }
        }

        private void FromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime temp1, temp2;
            if (ToDate.SelectedDate != null) {
                if(DateTime.TryParse(FromDate.Text, out temp1) && DateTime.TryParse(ToDate.Text, out temp2))
                {
                    Next.IsEnabled = true;
                }
            }
        }

        public static bool IsDateTime(string txtDate)
        {
            DateTime tempDate;
            return DateTime.TryParse(txtDate, out tempDate);
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Reservation reservation = Reservation.Instance;
            reservation.FromDate = FromDate.Text;
            reservation.ToDate = ToDate.Text;
            this.NavigationService.Navigate(new personalData());
        }
    }
}
