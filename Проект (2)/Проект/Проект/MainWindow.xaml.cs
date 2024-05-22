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

namespace Проект
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn = new SqlConnection(@"");
        public MainWindow()
        {
            InitializeComponent();
            conn.Open();
            var cmd = new SqlCommand($"select Код_тура from Тур",conn);
            var r = cmd.ExecuteReader();
            while(r.Read())
            {
                Tour.Items.Add(r.GetValue(0).ToString());
            }
            r.Close();

            cmd = new SqlCommand($"select Номер_билета from Билет", conn);
            r = cmd.ExecuteReader();
            while (r.Read())
            {
                Tickets.Items.Add(r.GetValue(0).ToString());
            }
            r.Close();  


            Para.ItemsSource = new List<string> { "Роли не играет","Стоимость","Длительность","Активность" };
            Para.SelectedIndex = 0;
        }

        private void Tours_Click(object sender, RoutedEventArgs e)
        {
            Result.Children.Clear();
            var cmd = new SqlCommand($"exec CountParameters '{Date.Text}',{Para.SelectedIndex}",conn);
            var r = cmd.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    var t = new TextBlock() { TextWrapping=TextWrapping.Wrap, FontSize = 12,
                        Text=$"Код: {r.GetInt32(0)} \nНазвание: {r.GetValue(1)} \nДлительность: {r.GetValue(2)} дней \nАктивность: {r.GetValue(3)} " +
                        $"\nБазовая стоимость: {r.GetValue(4)}\n" +
                        $"Минимальная стоимость проживания: {r.GetValue(5)} Максимальная стоимость проживания: {r.GetValue(6)} \n" +
                        $"Перелёт: {r.GetValue(7)} \nСтоимость доп. услуг: {r.GetValue(8)} \n" +
                        $"Итоговый минимум: {r.GetValue(9)} \n" +
                        $"Итоговый максимум: {r.GetValue(10)}\n\n" };
                    Result.Children.Add(t);
                }
            }
            catch
            {
                MessageBox.Show("Туры не найдены");
            }
        }

        private void Price_Click(object sender, RoutedEventArgs e)
        {
            if (Tickets.SelectedIndex == -1)
                return;
            var cmd = new SqlCommand($"select * from CountPrice({Tickets.SelectedItem})", conn);
            var r = cmd.ExecuteReader();
            r.Read();
            Res1.Text = "Итоговая стоимость билета: " + r.GetValue(0).ToString();
            r.Close();
        }
    }
}
