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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            this.Height = 480;
            this.Width = 800;
            InitializeComponent();
            GenderCb.ItemsSource = new List<TextBlock>() { new TextBlock() { FontSize=20, Text="М"},
                                                           new TextBlock() { FontSize=20, Text="Ж"},
                                                           new TextBlock() { FontSize=20, Text="Не указан"} };
            GenderCb.SelectedIndex = 0;
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!CheckProfile(LoginTB.Text))
            {
                MessageBox.Show("Такой логин уже существует");
                return;
            }
            StaticData.connection.Open();

            string command = $"insert into Клиент values((select max(код_клиента) from Клиент)+1," +
                $"'{Fam.Text}','{Nam.Text}','','','{((TextBlock)GenderCb.SelectedItem).Text}','П','{LoginTB.Text}','{PasswordTB.Password}','')";

            SqlCommand cmd = new SqlCommand(command, StaticData.connection);
            cmd.ExecuteNonQuery();

            StaticData.connection.Close();
            MessageBox.Show("Профиль создан");
            LoginBtn_Click(null, null);
        }
        private bool CheckProfile(string login)
        {
            StaticData.connection.Open();

            string command = $"select case when(select count(*) from Клиент where Логин like '{login}') > 0 then 1 else 0 end";

            SqlCommand cmd = new SqlCommand(command, StaticData.connection);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                if (r.GetInt32(0) == 0)
                {
                    StaticData.connection.Close();
                    return true;
                }
            }

            StaticData.connection.Close();
            return false;
        }
    }
}
