using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Kalkulator.Properties;

namespace Kalkulator.SQL
{
    class DAL
    {
        //Pobranie Marek wszystkich dostępnych samochodów
        public static void getMarka()
        {
            try
            {
                string strConn = "Data Source=SUNNY;Database=Pojazdy;User Id=sa;Password=12345;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SELECT Pojazd.Marka FROM Pojazd WHERE Pojazd.Zajety = 0 GROUP BY Pojazd.Marka", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                //adapter.Fill(MainWindow.main.dt);
                /*
                 * DataTable dt = new DataTable();
                adapter.Fill(dt);

                */

                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

        //Pobranie z bazy danych wszystkich dostępnych modeli dla danej marki
        public static void getModel(string model)
        {
            try
            {
                string strConn = "Data Source=SUNNY;Database=Pojazdy;User Id=sa;Password=12345;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SELECT Pojazd.Model FROM Pojazd WHERE Pojazd.Zajety = 0 AND Pojazd.Marka='"+model+"' GROUP BY Pojazd.Model", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                /*
                 * DataTable dt = new DataTable();
                adapter.Fill(dt);

                */

                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

        //Podczas rezerwacji dany pojazdu ustawiamy jako zajęty
        public static void zajmijPojazd(int id)
        {
            try
            {
                string strConn = "Data Source=SUNNY;Database=Pojazdy;User Id=sa;Password=12345;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SET Pojazd.Zajety = 1 WHERE Pojazd.ID_Pojazd = "+id, sqlConnection);
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

        public static void Rezerwacja(int id,String dataOd, String dataDo, String Nazwisko)
        {
            try
            {
                string strConn = "Data Source=SUNNY;Database=Pojazdy;User Id=sa;Password=12345;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Rezerwacja([ID_Pojazdu],[DataOd],[DataDo],[Nazwisko]) VALUES ("+id+", '"+dataOd+"', '"+dataDo+"', '"+Nazwisko+"') ",sqlConnection);
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

        //pobranie ID  pojazdu lub sprawdzenie czy dostepne
        public static void pobierzIDPojazd(String marka, String model)
        {
            try
            {
                string strConn = "Data Source=SUNNY;Database=Pojazdy;User Id=sa;Password=12345;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SELECT Pojazd.ID_Pojazd FROM Pojazd WHERE Pojazd.Marka = '" +marka+"' AND Pojazd.Model='"+model+"' AND Pojazd.Zajety=0", sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                /*
                 * DataTable dt = new DataTable();
                adapter.Fill(dt);
                if(dt!=null){
                    return dt.Columns["ID_Pojazd"];
                }else Console.WriteLine("Nie ma wolnych samochodów danej marki obecnie");

                */
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex);
            }
        }

        public static void pobierzCenePojazd(int ID)
        {
            try
            {
                string strConn = "Data Source=SUNNY;Database=Pojazdy;User Id=sa;Password=12345;";
                SqlConnection sqlConnection = new SqlConnection(strConn);
                SqlCommand sqlCommand = new SqlCommand("SELECT Pojazd.Cena FROM Pojazd WHERE Pojazd.ID_Pojazdu="+ID, sqlConnection);
                sqlConnection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                /*
                 * DataTable dt = new DataTable();
                adapter.Fill(dt);

                */
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
