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
            // FillMakeBox();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RentDates newPage = new RentDates();
            Current current = Current.Instance;
            this.NavigationService.Navigate(newPage);
            current.Page = newPage;

            Reservation reservation = Reservation.Instance;
            reservation.Make = Make.Text;
            reservation.Model = Model.Text;
        }

        private void Make_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /* Model.Visibility = Visibility.Visible;
            try
            {
                string strConn = "Data Source=SUNNY;Database=Pojazdy;User Id=sa;Password=12345;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                string makeName = Make.SelectedValue.ToString();
                SqlCommand sqlCommand = new SqlCommand("SELECT Pojazd.Model FROM Pojazd WHERE Pojazd.Zajety = 0 AND Pojazd.Marka='" + makeName + "' GROUP BY Pojazd.Model", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();

                adapter.Fill(dt);
                Model.ItemsSource = dt.DefaultView;
                Model.DisplayMemberPath = dt.Columns["Model"].ToString();
                Model.SelectedValuePath = dt.Columns["Model"].ToString(); ;

                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            } */
        }

        private void Model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void FillMakeBox()
        {
            try
            {
                string strConn = "Data Source=SUNNY;Database=Pojazdy;User Id=sa;Password=12345;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SELECT Pojazd.Marka FROM Pojazd WHERE Pojazd.Zajety = 0 GROUP BY Pojazd.Marka", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                DataTable dt = new DataTable();

                adapter.Fill(dt);
                Make.ItemsSource = dt.DefaultView;
                Make.DisplayMemberPath = dt.Columns["Marka"].ToString();
                Make.SelectedValuePath = dt.Columns["Marka"].ToString();

                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("A handled exception just occurred: " + ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
