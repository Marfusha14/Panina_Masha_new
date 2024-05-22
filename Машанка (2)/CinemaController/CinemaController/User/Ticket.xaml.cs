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
    /// Логика взаимодействия для Ticket.xaml
    /// </summary>
    public partial class Ticket : Window
    {
        List<CheckBox> checkBoxes = new List<CheckBox>();
        public Ticket()
        {
            InitializeComponent();

            checkBoxes = new List<CheckBox>() { CB1, CB2, CB3, CB4, CB5, CB6, CB7, CB8, CB9, CB10, CB11, CB12, CB13, CB14, CB15, CB16, CB17, CB18 };
            List<string> genres = new List<string>();

            StaticData.connection.Open();

            string command = $"select top 18 Название_жанра from Жанр order by newid()";

            SqlCommand cmd = new SqlCommand(command, StaticData.connection);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                genres.Add(r.GetString(0));
            }
            StaticData.connection.Close();

            for (int i = 0; i < 18;i++)
            {
                checkBoxes[i].Content = genres[i];
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StaticData.connection.Open();
            string command = $"delete from Круг_интересов where Код_клиента = {StaticData.user}";

            SqlCommand cmd = new SqlCommand(command, StaticData.connection);
            cmd.ExecuteNonQuery();

            foreach (CheckBox cb in checkBoxes)
            {
                if (cb.IsChecked == true)
                {
                    command = $"insert into Круг_интересов values('{StaticData.user}','{cb.Content}')";

                    cmd = new SqlCommand(command, StaticData.connection);
                    cmd.ExecuteNonQuery();
                }
            }
            StaticData.connection.Close();
            MessageBox.Show("Ваши предпочтения будут учтены!");
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
