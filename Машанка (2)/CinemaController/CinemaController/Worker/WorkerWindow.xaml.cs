using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
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
    /// Логика взаимодействия для WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        public WorkerWindow()
        {
            InitializeComponent();
        }

        private void AddSession_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    if(Convert.ToInt32(Price.Text) <= 0)
                    {
                        MessageBox.Show("Некорректная цена");
                        return;
                    }    
                }
                catch
                {
                    MessageBox.Show("Некорректная цена");
                }

                string command = $"insert into Сеанс values ({Cinema.Text},{Movie.Text},'{Date.Text}','{Price.Text}')";

                SqlCommand cmd = new SqlCommand(command, StaticData.connection);
                StaticData.connection.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка при добавлении сеанса");
                StaticData.connection.Close();
            }
        }
    }
}
