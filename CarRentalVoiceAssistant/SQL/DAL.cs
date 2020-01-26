using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CarRentalVoiceAssistant.Properties;

namespace CarRentalVoiceAssistant.SQL
{
    class DAL
    {
        private static String strConn = "Data Source=SUNNY;Database=Pojazdy;User Id=sa;Password=12345;";
        // Get all available car makes
        public static void GetMake()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SELECT Pojazd.Marka FROM Pojazd WHERE Pojazd.Zajety = 0 GROUP BY Pojazd.Marka", sqlConnection);
                sqlConnection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

        // Get all models available for specific make
        public static void GetModel(string make)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SELECT Pojazd.Model FROM Pojazd WHERE Pojazd.Zajety = 0 AND Pojazd.Marka='" + make + "' GROUP BY Pojazd.Model", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

        // While doing reservation we are set car as occupied
        public static void RentCar(int id)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SET Pojazd.Zajety = 1 WHERE Pojazd.ID_Pojazd = " + id, sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

        public static void CreateReservation(int id, String fromDate, String toDate, String surname)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Rezerwacja([ID_Pojazdu],[DataOd],[DataDo],[Nazwisko]) VALUES (" + id + ", '" + fromDate + "', '" + toDate + "', '" + surname + "') ", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

        // Get car ID or check if car is occupied
        public static void GetCarID(String make, String model)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SELECT Pojazd.ID_Pojazd FROM Pojazd WHERE Pojazd.Marka = '" + make + "' AND Pojazd.Model='" + model + "' AND Pojazd.Zajety=0", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

        public static void GetCarPrice(int ID)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SELECT Pojazd.Cena FROM Pojazd WHERE Pojazd.ID_Pojazdu=" + ID, sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }
    }
}