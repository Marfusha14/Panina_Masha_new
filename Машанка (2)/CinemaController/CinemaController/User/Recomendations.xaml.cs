using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CinemaController
{
    /// <summary>
    /// Логика взаимодействия для Recomendations.xaml
    /// </summary>
    public partial class Recomendations : Page
    {
        List<string> movies = new List<string>();
        public Recomendations()
        {
            InitializeComponent();
            StaticData.connection.Open();

            string command = $"select distinct top 5 cast(Код_фильма as nvarchar(20)) from(select top 5 * from Круг_интересов where Код_клиента={StaticData.user} " +
                $"order by newid())f inner join Жанры_кино on Жанры_кино.Название_жанра = f.Название_жанра";

            SqlCommand cmd = new SqlCommand(command, StaticData.connection);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                movies.Add(r.GetString(0));
            }
            StaticData.connection.Close();

            int height = 0;
            if (movies.Count == 0)
            {
                MessageBox.Show("Чтобы получить персональные рекомендации, пройдите анкету в вашем профиле");
                return;
            }
            else
            {
                for (int i = 0; i < movies.Count; i++)
                {
                    TextBlock N1 = new TextBlock() { Name = $"N{i}", FontSize = 40, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 110 + height, 0, 0), Height = 70, Width = 500 };
                    TextBlock V1 = new TextBlock() { Name = $"V{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 163 + height, 0, 0), Height = 70, Width = 500 };
                    TextBlock R1 = new TextBlock() { Name = $"R{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 225 + height, 0, 0), Height = 70, Width = 500 };
                    TextBlock A1 = new TextBlock() { Name = $"A{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 295 + height, 0, 0), Height = 70, Width = 500 };

                    Image I1 = new Image() { Name = $"I{i}", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Width = 177, Height = 223, Margin = new Thickness(182, 110 + height, 0, 0) };
                    MainGrid.Children.Add(I1);
                    MainGrid.Children.Add(N1);
                    MainGrid.Children.Add(V1);
                    MainGrid.Children.Add(R1);
                    MainGrid.Children.Add(A1);

                    StaticData.connection.Open();

                    command = $"select Название,Год_выпуска,Режиссёры,Актёры,Обложка from Фильм where Код_фильма={movies[i]}";

                    cmd = new SqlCommand(command, StaticData.connection);
                    r = cmd.ExecuteReader();
                    r.Read();

                    N1.Text = r.GetString(0);
                    V1.Text = r.GetString(1);
                    R1.Text = r.GetString(2);
                    A1.Text = r.GetString(3);

                    try
                    {
                        var c = new WebClient();
                        var bytes = c.DownloadData(r.GetString(4));
                        var ms = new MemoryStream(bytes);

                        var bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = ms;
                        bi.EndInit();

                        I1.Source = bi;
                    }
                    catch
                    {

                    }
                    StaticData.connection.Close();
                    height += 450;
                }
                MainGrid.Height = height + 200;
            }
        }
    }
}
