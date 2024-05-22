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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public Profile()
        {
            InitializeComponent();
            GenderCb.ItemsSource = new List<string>() { "М", "Ж", "Не указан" };
            List<string> Cities_list =  new List<string>() { "" };
            StaticData.connection.Open();

            string command = $"select distinct Город from Кинотеатр";
            SqlCommand cmd = new SqlCommand(command, StaticData.connection);

            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                Cities_list.Add(r.GetString(0));
            }
            r.Close();

            Cities.ItemsSource = Cities_list;

            command = $"select Фамилия, Имя, Отчество, Дата_рождения, Пол, Город from Клиент where Код_клиента = {StaticData.user}";
            cmd = new SqlCommand(command, StaticData.connection);

            r = cmd.ExecuteReader();
            r.Read();
            Fam.Text = r.GetString(0);
            Nam.Text = r.GetString(1);
            Patr.Text = r.GetString(2);
            Birthday.Text = r.GetValue(3).ToString();
            switch (r.GetString(4))
            {
                case "М":
                    GenderCb.SelectedIndex = 0;
                    break;
                case "Ж":
                    GenderCb.SelectedIndex = 1;
                    break;
                case "Не указан":
                    GenderCb.SelectedIndex = 2;
                    break;
            }

            Cities.SelectedIndex = Cities_list.IndexOf(r.GetString(5));

            StaticData.connection.Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StaticData.connection.Open();

            string command = $"update Клиент set Фамилия='{Fam.Text}', Имя='{Nam.Text}', Отчество='{Patr.Text}'" +
                $", Дата_рождения='{Birthday.Text}', Пол='{GenderCb.SelectedItem}', Город='{Cities.SelectedItem}'" +
                $" where Код_клиента = {StaticData.user}";
            SqlCommand cmd = new SqlCommand(command, StaticData.connection);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Изменения в профиле сохранены");
            StaticData.connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StaticData.Frame.GoBack();
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            StaticData.CurrentMainWindow.Close();
            StaticData.CurrentMainWindow = null;
        }

        private void ProcessTicket_Click(object sender, RoutedEventArgs e)
        {
            Ticket t = new Ticket();
            t.ShowDialog();
        }
    }
}
