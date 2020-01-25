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
    /// Logika interakcji dla klasy carChoose.xaml
    /// </summary>
    public partial class carChoose : Page
    {
        public carChoose()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new rentDates());
            Reservation reservation = Reservation.Instance;
            reservation.Make = Make.Text;
            reservation.Model = Model.Text;
        }

        private void Make_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
