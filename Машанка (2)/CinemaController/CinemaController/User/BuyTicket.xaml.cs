using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для BuyTicket.xaml
    /// </summary>
    public partial class BuyTicket : Window
    {
        List<List<string>> Services_list = new List<List<string>>();
        List<string> session;
        List<int> Count_list = new List<int>();
        public BuyTicket(List<string> session)
        {
            InitializeComponent();
            this.session = session;
            StaticData.connection.Open();

            string command = $"select * from [Дополнительная услуга] where Номер_кинотеатра='{session[6]}'";
            SqlCommand cmd = new SqlCommand(command, StaticData.connection);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                Services_list.Add(new List<string>() { r.GetString(1), r.GetValue(2).ToString(), r.GetString(3) });
                Count_list.Add(0);
            }
            r.Close();

            command = $"select Количество_баллов from Клиент where Код_клиента = {StaticData.user}";
            cmd = new SqlCommand(command, StaticData.connection);
            r = cmd.ExecuteReader();
            r.Read();
            BonusBalance.Content = "Баланс баллов: " + r.GetValue(0).ToString();

            Services.ItemsSource = Services_list.Select(x => x[0] + " " + x[2] + "; " + x[1] + "руб.");
            Services.SelectedIndex = 0;
            StaticData.connection.Close();
            Check.Content = $"Билет на {session[1]} --- {session[3]} руб.\n";
            TicketCount.TextChanged += TicketCount_TextChanged;
        }

        private void EndThis_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StaticData.connection.Open();
                if (Convert.ToInt32(TicketCount.Text) <= 0 || Convert.ToInt32(TicketCount.Text) > 50)
                {
                    MessageBox.Show("Некорректное количество билетов");
                }
                string command = $"delete from Билет where Номер_кинотеатра={session[6]} " +
                    $"and ID_клиента={StaticData.user} " +
                    $"and Код_фильма={session[5]} " +
                    $"and Дата_проведения='{session[2]}'";
                SqlCommand cmd = new SqlCommand(command, StaticData.connection);
                cmd.ExecuteNonQuery();

                command = $"insert into Билет values({session[6]},{StaticData.user},{session[5]},'{session[2]}',{TicketCount.Text})";
                cmd = new SqlCommand(command, StaticData.connection);
                cmd.ExecuteNonQuery();
                for (int i = 0; i < Count_list.Count; i++)
                {
                    command = $"insert into Услуга_билета values({session[6]},{StaticData.user},{session[5]},'{session[2]}','{Services_list[i][0]}','{Services_list[i][1]}','{Count_list[i]}')";
                    cmd = new SqlCommand(command, StaticData.connection);
                    cmd.ExecuteNonQuery();
                }
                int price = String.Join(" ", Check.Content.ToString().Split('\n').Select(x => x.Split('-').Last().Replace("-", "").Replace(" ", "").Replace("руб.", ""))).Trim().Split().Select(x => Convert.ToInt32(x)).Sum();
                if (countbonuses)
                {
                    price -= Convert.ToInt32(Bonuses.Text);
                }

                command = $"update Клиент set Количество_баллов = Количество_баллов+{price/50-Convert.ToInt32(Bonuses.Text)} where Код_клиента = {StaticData.user}";
                cmd = new SqlCommand(command, StaticData.connection);
                cmd.ExecuteNonQuery();
                StaticData.connection.Close();
                this.Close();
                MessageBox.Show($"Билет забронирован. Общая сумма заказа: {price} " +
                    $"вам будет начислены 2% бонусов от этого заказа");
            }
            catch
            {
                MessageBox.Show("Ошибка при заказе");
                StaticData.connection.Close();
            }
        }

        private void AddToCheck_Click(object sender, RoutedEventArgs e)
        {
            Check.Content = $"Билет на {session[1]} --- {Convert.ToInt32(session[3])*Convert.ToInt32(TicketCount.Text)} руб.\n";
            Count_list[Services.SelectedIndex] += 1;
            for (int i = 0; i < Count_list.Count; i++)
            {
                if (Count_list[i] == 0)
                    continue;
                Check.Content += $"{Services_list[i][0]} {Services_list[i][2]} --- {Convert.ToInt32(Services_list[i][1]) * Count_list[i]} руб.\n";
            }
        }
        private void DeleleFromCheck_Click(object sender, RoutedEventArgs e)
        {
            Check.Content = $"Билет на {session[1]} --- {session[3]} руб.\n";
            Count_list[Services.SelectedIndex] -= 1;
            if (Count_list[Services.SelectedIndex] < 0)
                Count_list[Services.SelectedIndex] = 0;
            for (int i = 0; i < Count_list.Count; i++)
            {
                if (Count_list[i] == 0)
                    continue;
                Check.Content += $"{Services_list[i][0]} {Services_list[i][2]} --- {Convert.ToInt32(Services_list[i][1]) * Count_list[i]} руб.\n";
            }
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TicketCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Check.Content = $"Билет на {session[1]} --- {Convert.ToInt32(session[3]) * Convert.ToInt32(TicketCount.Text)} руб.\n";
                for (int i = 0; i < Count_list.Count; i++)
                {
                    if (Count_list[i] == 0)
                        continue;
                    Check.Content += $"{Services_list[i][0]} {Services_list[i][2]} --- {Convert.ToInt32(Services_list[i][1]) * Count_list[i]} руб.\n";
                }
            }
            catch { Check.Content = ""; }
        }
        bool countbonuses = false;
        private void UseBonuses_Click(object sender, RoutedEventArgs e)
        {
            countbonuses = !countbonuses;
            if(countbonuses)
            {
                if (Convert.ToInt32(BonusBalance.Content.ToString().Split().Last()) < Convert.ToInt32(Bonuses.Text) || Convert.ToInt32(Bonuses.Text) < 0)
                {
                    MessageBox.Show("Некорректное количество баллов");
                    countbonuses = false;
                }
                else
                {
                    MessageBox.Show("Включено использование баллов");
                    Bonuses.IsEnabled = false;
                }
            }
            else
                Bonuses.IsEnabled = true;
        }
    }
}
