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
using System.Data.SqlClient;
using System.Globalization;
using System.Data;

namespace CarRentalVoiceAssistant
{
    /// <summary>
    /// Logika interakcji dla klasy CarChoose.xaml
    /// </summary>
    public partial class CarChoose : Page
    {
        public CarChoose()
        {
            InitializeComponent();
            Assistant.LoadChooseCarRecognition();
            FillMakeBox();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int carid = DAL.GetCarID(Make.Text, Model.Text);
            if (carid == 0)
            {
                MessageBox.Show(" Przykor nam. Wybrany samochód nie znajduje się w ofercie lub wszystkie są obecnie zajęte.");
            }
            else {
                RentDates newPage = new RentDates();
                Current current = Current.Instance;
                this.NavigationService.Navigate(newPage);
                current.Page = newPage;

                Reservation reservation = Reservation.Instance;
                reservation.Make = Make.Text;
                reservation.Model = Model.Text;
                reservation.ID = carid;
            }
        }

        private void Make_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.Visibility = Visibility.Visible;
            String whatever = Make.SelectedItem.ToString();
            String[] paczka = DAL.GetModel(whatever);
            Model.Items.Clear();
            foreach (String str in paczka)
            {
                Model.Items.Add(str);
            }
        }

        private void Model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void FillMakeBox()
        {
            String[] paczka = DAL.GetMake();
            foreach(String str in paczka)
            {
                Make.Items.Add(str);
            }
        }

    }
}
