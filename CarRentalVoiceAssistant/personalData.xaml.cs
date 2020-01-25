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
    /// Logika interakcji dla klasy Page1.xaml
    /// </summary>
    public partial class personalData : Page
    {
        public personalData()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Reservation reservation = Reservation.Instance;
            reservation.PersonalName = PersonalName.Text;
            reservation.Surname = PersonalSurname.Text;
            this.NavigationService.Navigate(new Summary());
        }

        private void PersonalName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonalSurname.Text != "") {
                Next.IsEnabled = true;
            }
        }

        private void PersonalSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonalName.Text != "")
            {
                Next.IsEnabled = true;
            }
        }
    }
}
