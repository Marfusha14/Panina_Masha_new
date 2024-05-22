using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace CinemaController
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string city = "";
        public MainWindow()
        { 
            MessageBox.Show("ОШИБКА, НЕТ ПОЛЬЗОВАТЕЛЯ");
            Application.Current.Shutdown();
        }
        public MainWindow(string login)
        {
            InitializeComponent();

            StaticData.Frame = Frame;
            StaticData.login = login;
            StaticData.connection.Open();

            string command = $"select Город from Клиент where Код_клиента = {StaticData.user}";
            SqlCommand cmd = new SqlCommand(command, StaticData.connection);

            SqlDataReader r = cmd.ExecuteReader();
            r.Read();
            city = r.GetString(0);
            StaticData.connection.Close();
        }

        private void AfishaBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Afisha(city));
        }

        private void RecsBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Recomendations());
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Profile());
        }
    }
}
