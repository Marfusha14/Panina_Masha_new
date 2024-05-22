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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            StaticData.connection.Open();

            string command = $"select case when (select count(*) from Клиент where Логин like '{LoginTB.Text}' " +
                $"and Пароль like '{PasswordTB.Password}') > 0 then 1 else 0 end";

            SqlCommand cmd = new SqlCommand(command, StaticData.connection);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                if (r.GetInt32(0) == 0)
                {
                    StaticData.connection.Close();
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }
            }
            r.Close();

            command = $"select Код_клиента, Роль_в_системе from Клиент where Логин like '{LoginTB.Text}'";

            cmd = new SqlCommand(command, StaticData.connection);
            r = cmd.ExecuteReader();
            r.Read();

            StaticData.user = r.GetInt32(0);
            StaticData.role = r.GetString(1);
            StaticData.connection.Close();
            if (StaticData.role == "П")
            {
                StaticData.CurrentMainWindow = new MainWindow(LoginTB.Text);
                StaticData.CurrentMainWindow.Show();
            }
            else if (StaticData.role == "Р")
            {
                try
                {
                    StaticData.connection.Open();
                    command = $"select * from Работник where Код_клиента = {StaticData.user}";

                    cmd = new SqlCommand(command, StaticData.connection);
                    r = cmd.ExecuteReader();
                    r.Read();
                    if (r.GetString(2) == "Менеджер" || r.GetString(2) == "Администратор")
                    {
                        new WorkerWindow().Show();
                    }
                    else
                    {
                        StaticData.CurrentMainWindow = new MainWindow(LoginTB.Text);
                        StaticData.CurrentMainWindow.Show();
                    }
                    StaticData.connection.Close();
                }
                catch
                {
                    StaticData.connection.Close();
                    MessageBox.Show("Работник не назначен на должность");
                }
            }
            else if(StaticData.role == "А")
            {
                new SuperAdminWindow().Show();
            }
            this.Close();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            Registration r = new Registration();
            r.ShowDialog();
        }
    }
}
