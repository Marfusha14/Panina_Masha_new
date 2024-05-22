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
using System.Windows.Shapes;

namespace CinemaController
{
    /// <summary>
    /// Логика взаимодействия для SuperAdminWindow.xaml
    /// </summary>
    public partial class SuperAdminWindow : Window
    {
        public SuperAdminWindow()
        {
            InitializeComponent();
            Profession.ItemsSource = new List<string>() { "Менеджер","Администратор","Кассир" };
        }

        private void AddWorker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string user=GetId();
                StaticData.connection.Open();

                string command = $"delete Работник where Код_клиента = {user}";

                SqlCommand cmd = new SqlCommand(command, StaticData.connection);
                cmd.ExecuteNonQuery();

                command = $"insert into Работник values ({user},{Cinema.Text},'{Contract.Text}','{Profession.SelectedItem}')";

                cmd = new SqlCommand(command, StaticData.connection);
                cmd.ExecuteNonQuery();

                command = $"update Клиент set Роль_в_системе='Р' where Логин = {WorkerLogin.Text}";

                cmd = new SqlCommand(command, StaticData.connection);
                cmd.ExecuteNonQuery();

                StaticData.connection.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении работника");
                StaticData.connection.Close();
            }
        }
        private string GetId()
        {
            StaticData.connection.Open();
            string command = $"select Код_клиента from Клиент where Логин = {WorkerLogin.Text}";

            SqlCommand cmd = new SqlCommand(command, StaticData.connection);
            SqlDataReader r = cmd.ExecuteReader();
            r.Read();
            string id = r.GetValue(0).ToString();
            StaticData.connection.Close();
            return id;
        }
    }
}
