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
    /// Логика взаимодействия для Afisha.xaml
    /// </summary>
    public partial class Afisha : Page
    {

        List<List<string>> Sessions = new List<List<string>>();

        List<Button> Buttons = new List<Button>();
        List<List<TextBlock>> TextBlocks = new List<List<TextBlock>>();


        public Afisha()
        {
            InitializeComponent();
        }
        public Afisha(string city)
        {
            InitializeComponent();
            MainGrid.Children.Clear();
            GenFilters();

            if (city != "")
            {
                StaticData.connection.Open();

                string command = $"select distinct Кинотеатр.Название, Фильм.Название, Дата_проведения, Базовая_стоимость, Фильм.Обложка, Фильм.Код_фильма, Кинотеатр.Номер_кинотеатра " +
                    $"from Сеанс inner join Фильм on Сеанс.Код_фильма = Фильм.Код_фильма inner join Кинотеатр on Сеанс.Номер_кинотеатра = Кинотеатр.Номер_кинотеатра where Город like '{city}' and Дата_проведения>=cast(getdate() as date)";
                SqlCommand cmd = new SqlCommand(command, StaticData.connection);

                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Sessions.Add(new List<string>()
                {
                    r.GetString(0),
                    r.GetString(1),
                    r.GetValue(2).ToString(),
                    r.GetDouble(3).ToString(),
                    r.GetString(4),
                    r.GetInt32(5).ToString(),
                    r.GetInt32(6).ToString()
                });
                }
                StaticData.connection.Close();
            }
            else
            {
                StaticData.connection.Open();

                string command = $"select distinct Кинотеатр.Название, Фильм.Название, Дата_проведения, Базовая_стоимость, Фильм.Обложка, Фильм.Код_фильма, Кинотеатр.Номер_кинотеатра " +
                    $"from Сеанс inner join Фильм on Сеанс.Код_фильма = Фильм.Код_фильма inner join Кинотеатр on Сеанс.Номер_кинотеатра = Кинотеатр.Номер_кинотеатра where Дата_проведения>=cast(getdate() as date)";
                SqlCommand cmd = new SqlCommand(command, StaticData.connection);

                SqlDataReader r = cmd.ExecuteReader();
                while (r.Read())
                {
                    Sessions.Add(new List<string>()
                {
                    r.GetString(0),
                    r.GetString(1),
                    r.GetValue(2).ToString(),
                    r.GetDouble(3).ToString(),
                    r.GetString(4),
                    r.GetInt32(5).ToString(),
                    r.GetInt32(6).ToString()
                });
                }
                StaticData.connection.Close();
            }

            int height = 100;
            for(int i = 0; i < Sessions.Count; i++)
            {
                TextBlock N1 = new TextBlock() { Name = $"N{i}", FontSize=40, Foreground =Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 110+height, 0, 0),Height=70,Width= 500 };
                TextBlock V1 = new TextBlock() { Name = $"V{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 163+height, 0, 0), Height = 70, Width = 500 };
                TextBlock R1 = new TextBlock() { Name = $"R{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 225 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock A1 = new TextBlock() { Name = $"A{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 295 + height, 0, 0), Height = 70, Width = 500 };

                TextBlock K1 = new TextBlock() { Name = $"K{i}", FontSize = 35, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 110 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock AD1 = new TextBlock() { Name = $"AD{i}", FontSize = 35, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 185 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock DA1 = new TextBlock() { Name = $"DA{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 255 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock P1 = new TextBlock() { Name = $"P{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 300 + height, 0, 0), Height = 70, Width = 500 };

                Button B1 = new Button() { Name=$"B{i}", Content="Оформить заказ", Style=FindResource("DefaultButton") as Style, Foreground = Brushes.White, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 336 + height, 0, 0) };
                Image I1 = new Image() {Name=$"I{i}",HorizontalAlignment=HorizontalAlignment.Left,VerticalAlignment=VerticalAlignment.Top,Width=177,Height=223, Margin=new Thickness(182,110+height,0,0) };
                MainGrid.Children.Add(I1);
                N1.Text = Sessions[i][1];
                var info = Movie_Adress(Sessions[i][5], Sessions[i][6]);
                
                V1.Text = info[1];
                R1.Text = "Режиссёр: " + info[2];
                A1.Text = "Актёры: " + info[3];
                K1.Text = Sessions[i][0];
                DA1.Text = Sessions[i][2];
                AD1.Text = info[0];
                P1.Text = Sessions[i][3]+" руб.";
                TextBlocks.Add(new List<TextBlock> { N1,V1,R1,A1,DA1,K1,AD1,P1 });
                B1.Click += ProcessTicket;
                Buttons.Add(B1);
                try
                {
                    var c = new WebClient();
                    var bytes = c.DownloadData(info[4]);
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
                height += 450;
            }
            foreach(var r in TextBlocks)
            {
                foreach(var r1 in r)
                {
                    MainGrid.Children.Add(r1);
                }
            }
            foreach (var r in Buttons)
            {
                MainGrid.Children.Add(r);
            }
            MainGrid.Height = height + 300;

        }
        private List<string> Movie_Adress(string movie, string cinema)
        {
            List<string> infos = new List<string>();

            StaticData.connection.Open();

            string command = $"select concat(Улица,' ',Номер_дома,'-',Строение) from Кинотеатр where Номер_кинотеатра = {cinema}";
            SqlCommand cmd = new SqlCommand(command, StaticData.connection);

            SqlDataReader r = cmd.ExecuteReader();
            r.Read();
            infos.Add(r.GetString(0));
            r.Close();

            command = $"select Год_выпуска,Режиссёры,Актёры,Обложка from фильм where Код_фильма = {movie}";
            cmd = new SqlCommand(command, StaticData.connection);
            r = cmd.ExecuteReader();
            r.Read();
            infos.Add(r.GetString(0));
            infos.Add(r.GetString(1));
            infos.Add(r.GetString(2));
            infos.Add(r.GetString(3));

            StaticData.connection.Close();

            return infos;
        }
        private void ProcessTicket(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32(((Button)sender).Name.Last().ToString());

            new BuyTicket(Sessions[index]).ShowDialog();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            List<Image> imgs = new List<Image>();
            Buttons.Clear();
            TextBlocks.Clear();
            int height = 100;
            for (int i = 0; i < Sessions.Count; i++)
            {
                TextBlock N1 = new TextBlock() { Name = $"N{i}", FontSize = 40, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 110 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock V1 = new TextBlock() { Name = $"V{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 163 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock R1 = new TextBlock() { Name = $"R{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 225 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock A1 = new TextBlock() { Name = $"A{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 295 + height, 0, 0), Height = 70, Width = 500 };

                TextBlock K1 = new TextBlock() { Name = $"K{i}", FontSize = 35, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 110 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock AD1 = new TextBlock() { Name = $"AD{i}", FontSize = 35, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 185 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock DA1 = new TextBlock() { Name = $"DA{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 255 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock P1 = new TextBlock() { Name = $"P{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 300 + height, 0, 0), Height = 70, Width = 500 };

                Button B1 = new Button() { Name = $"B{i}", Content = "Оформить заказ", Style = FindResource("DefaultButton") as Style, Foreground = Brushes.White, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 336 + height, 0, 0) };
                Image I1 = new Image() { Name = $"I{i}", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Width = 177, Height = 223, Margin = new Thickness(182, 110 + height, 0, 0) };
                N1.Text = Sessions[i][1];
                var info = Movie_Adress(Sessions[i][5], Sessions[i][6]);
                if (!info[1].Contains(filt_year.Trim()) & filt_year.Trim() != "")
                {
                    continue;
                }
                if (!Sessions[i][1].ToLower().Contains(filt_title.ToLower().Trim()) & filt_title.Trim() != "")
                {
                    continue;
                }
                V1.Text = info[1];
                R1.Text = "Режиссёр: " + info[2];
                A1.Text = "Актёры: " + info[3];
                K1.Text = Sessions[i][0];
                DA1.Text = Sessions[i][2];
                AD1.Text = info[0];
                P1.Text = Sessions[i][3] + " руб.";
                TextBlocks.Add(new List<TextBlock> { N1, V1, R1, A1, DA1, K1, AD1, P1 });
                B1.Click += ProcessTicket;
                Buttons.Add(B1);
                imgs.Add(I1);
                try
                {
                    var c = new WebClient();
                    var bytes = c.DownloadData(info[4]);
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
                height += 450;
            }
            MainGrid.Children.Clear();
            GenFilters();
            foreach (var r in TextBlocks)
            {
                foreach (var r1 in r)
                {
                    MainGrid.Children.Add(r1);
                }
            }
            foreach (var r in Buttons)
            {
                MainGrid.Children.Add(r);
            }
            foreach (var r in imgs)
            {
                MainGrid.Children.Add(r);
            }
            MainGrid.Height = height + 1000;
        }

        private void Drop_Click(object sender, RoutedEventArgs e)
        {
            Buttons.Clear();
            TextBlocks.Clear();
            MainGrid.Children.Clear(); 
            GenFilters();
            int height = 100;
            for (int i = 0; i < Sessions.Count; i++)
            {
                TextBlock N1 = new TextBlock() { Name = $"N{i}", FontSize = 40, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 110 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock V1 = new TextBlock() { Name = $"V{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 163 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock R1 = new TextBlock() { Name = $"R{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 225 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock A1 = new TextBlock() { Name = $"A{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(370, 295 + height, 0, 0), Height = 70, Width = 500 };

                TextBlock K1 = new TextBlock() { Name = $"K{i}", FontSize = 35, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 110 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock AD1 = new TextBlock() { Name = $"AD{i}", FontSize = 35, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 185 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock DA1 = new TextBlock() { Name = $"DA{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 255 + height, 0, 0), Height = 70, Width = 500 };
                TextBlock P1 = new TextBlock() { Name = $"P{i}", FontSize = 22, Foreground = Brushes.Yellow, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 300 + height, 0, 0), Height = 70, Width = 500 };

                Button B1 = new Button() { Name = $"B{i}", Content = "Оформить заказ", Style = FindResource("DefaultButton") as Style, Foreground = Brushes.White, VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(885, 336 + height, 0, 0) };
                Image I1 = new Image() { Name = $"I{i}", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Width = 177, Height = 223, Margin = new Thickness(182, 110 + height, 0, 0) };
                MainGrid.Children.Add(I1);
                N1.Text = Sessions[i][1];
                var info = Movie_Adress(Sessions[i][5], Sessions[i][6]);
                V1.Text = info[1];
                R1.Text = "Режиссёр: " + info[2];
                A1.Text = "Актёры: " + info[3];
                K1.Text = Sessions[i][0];
                DA1.Text = Sessions[i][2];
                AD1.Text = info[0];
                P1.Text = Sessions[i][3] + " руб.";
                TextBlocks.Add(new List<TextBlock> { N1, V1, R1, A1, DA1, K1, AD1, P1 });
                B1.Click += ProcessTicket;
                Buttons.Add(B1);
                try
                {
                    var c = new WebClient();
                    var bytes = c.DownloadData(info[4]);
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
                height += 450;
            }
            foreach (var r in TextBlocks)
            {
                foreach (var r1 in r)
                {
                    MainGrid.Children.Add(r1);
                }
            }
            foreach (var r in Buttons)
            {
                MainGrid.Children.Add(r);
            }
            MainGrid.Height = height + 1000;
        }
        string filt_title="";
        string filt_year="";
        private void GenFilters()
        {
            TextBlock Name = new TextBlock() { Name="_1", Text="Фильтры", Height=35, Width=104, FontSize=24, Foreground=Brushes.Yellow, Margin=new Thickness(10,10,0,0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            TextBlock Year = new TextBlock() { Name = "_2", Text = "Год выпуска", Height = 28, Width = 124, FontSize = 20, Foreground = Brushes.Yellow, Margin = new Thickness(124, 17, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            TextBlock Title = new TextBlock() { Name = "_3", Text = "Название", Height = 28, Width = 100, FontSize = 20, Foreground = Brushes.Yellow, Margin = new Thickness(385, 17, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            TextBox Titletb = new TextBox() { Width = 120, FontSize = 20, Name="title", Margin = new Thickness(486, 20, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            TextBox Yeartb = new TextBox() { Width = 120, FontSize = 20, Name = "year", Margin = new Thickness(250, 20, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };

            Titletb.TextChanged += Title_TextChanged;
            Yeartb.TextChanged += Year_TextChanged;

            Button Filt = new Button() { Style = FindResource("DefaultButton") as Style, Margin = new Thickness(627, 17, 0, 0), Content = "Применить", HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
            Filt.Click += Filter_Click;
            Button Drop = new Button() { Style=FindResource("DefaultButton") as Style, Margin = new Thickness(627, 64, 0, 0), Content="Сбросить",HorizontalAlignment=HorizontalAlignment.Left,VerticalAlignment=VerticalAlignment.Top};
            Drop.Click += Drop_Click;

            MainGrid.Children.Add(Name);
            MainGrid.Children.Add(Year);
            MainGrid.Children.Add(Title);

            MainGrid.Children.Add(Titletb);
            MainGrid.Children.Add(Yeartb);

            MainGrid.Children.Add(Filt);
            MainGrid.Children.Add(Drop);
        }

        private void Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            filt_title = ((TextBox)sender).Text;
        }
        private void Year_TextChanged(object sender, TextChangedEventArgs e)
        {
            filt_year = ((TextBox)sender).Text;
        }

    }
}
