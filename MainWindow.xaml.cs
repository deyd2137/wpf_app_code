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
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

//string logintmp;
//string haslotmp;
//string serwertmp;
//string databasetmp;

namespace WpfApp1 { 
   


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        void logowanie_baza(string serwertmp, string databasetmp, string logintmp, string haslotmp)
        {
            SqlConnection polaczenie = new SqlConnection();
            polaczenie.ConnectionString = @"Server=" + serwertmp + "; Database=" + databasetmp + "; User Id=" + logintmp + "; Password=" + haslotmp + ";";
            //polaczenie.ConnectionString = @"Server=DESKTOP-OEF53FV\PRAKTYKA;Database=eksport;User Id=sa;Password=Zbud123;";
            try
            {
                polaczenie.Open();
                Debug.WriteLine("\nPOŁACZENIE NAWIAZANE\n");


            }
            catch (System.Data.SqlClient.SqlException se)
            {
                Debug.WriteLine("\nBŁĄD\n" + se.StackTrace);
            }
            finally
            {
                if (polaczenie.State == ConnectionState.Open)
                {
                    Debug.WriteLine("\nZamykam polaczenie\n");
                    polaczenie.Close();
                }
            }
        }

        public ObservableCollection<Tabela> Records;
        string logintmp;
        string haslotmp;
        string serwertmp;
        string databasetmp;

        public MainWindow()
        {
            InitializeComponent();
        }

        public class Tabela
        {
            public string ReturnCode { get; set; }
            public string LatestCommTime { get; set; }
            public string Address { get; set; }
            public string MacAddress { get; set; }
            public string ModelName { get; set; }
            public string DeviceName { get; set; }
            public string Comment { get; set; }
            public string TotalCounter { get; set; }
        }

        private void tekstbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public void baton_Click_1(object sender, RoutedEventArgs e)
        {
            logowanie_baza(serwertmp,databasetmp,logintmp,haslotmp);
           /* SqlConnection polaczenie = new SqlConnection();
            polaczenie.ConnectionString = @"Server=" + serwertmp + "; Database="+databasetmp+"; User Id="+logintmp+"; Password="+haslotmp +";";
            //polaczenie.ConnectionString = @"Server=DESKTOP-OEF53FV\PRAKTYKA;Database=eksport;User Id=sa;Password=Zbud123;";
            try
            {
                polaczenie.Open();
                Debug.WriteLine("\nPOŁACZENIE NAWIAZANE\n");


            }
            catch (System.Data.SqlClient.SqlException se)
            {
                Debug.WriteLine("\nBŁĄD\n" + se.StackTrace);
            }
            finally
            {
                if (polaczenie.State == ConnectionState.Open)
                {
                    Debug.WriteLine("\nZamykam polaczenie\n");
                    polaczenie.Close();
                }
            }
            //MessageBox.Show("Dane zostaly eksportowane.");*/
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Records = new ObservableCollection<Tabela>();

            List<List<string>> records = new List<List<string>>();
            string s;
            string path = tekstbox.Text;
            //Debug.WriteLine(System.IO.Path.GetExtension(path));
            if (File.Exists(path) && System.IO.Path.GetExtension(path) == ".CSV")
                using (StreamReader sr = File.OpenText(path))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        if ((s.Substring(0, 1).Equals("#")) || (s.Substring(0, 1).Equals("<"))) continue;
                        //tekstbox.Text += s;
                        records.Add(s.Split(',').ToList<string>());
                    }
                }
            else
            {
                MessageBox.Show("Taki plik nie istnieje,\n" +
                    "Sprawdź czy ścieżka jest poprwana.");
            }
            foreach (List<string> record in records)
            {
                Tabela plik = new Tabela();
                plik.ReturnCode = record[0];
                plik.LatestCommTime = record[1];
                plik.Address = record[2];
                plik.MacAddress = record[3];
                plik.ModelName = record[4];
                plik.DeviceName = record[5];
                plik.Comment = record[6];
                plik.TotalCounter = record[7];
                //string ssw = Grid.Items.Add(plik).ToString();
                Records.Add(plik);
            }
            Grid.ItemsSource = Records;
        }

        public void Server_TextChanged(object sender, TextChangedEventArgs e)
        {
            serwertmp = Server.Text;
        }

        public void Database_TextChanged(object sender, TextChangedEventArgs e)
        {
            databasetmp = Database.Text;
        }

        public void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            logintmp = Login.Text;
        }

        public void Haslo_TextChanged(object sender, TextChangedEventArgs e)
        {
            haslotmp = Haslo.Text;
        }
    }
}