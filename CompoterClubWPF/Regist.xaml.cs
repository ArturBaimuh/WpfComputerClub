using System;
using System.Net.Http;
using System.Windows;

namespace CompoterClubWPF
{
    /// <summary>
    /// Логика взаимодействия для Regist.xaml
    /// </summary>
    public partial class Regist : Window
    {
        public Regist()
        {
            InitializeComponent();
        }

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            string name = Namet.Text;
            string fname = FamiT.Text;
            string oname = OtchT.Text;
            string login = loginT.Text;
            string comp = Comp.Text;
            string paswword = PaswwT.Text;
            if (name != "" && fname != "" && oname != "" && login != "" && paswword != "" && comp != "")
            {
                HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
                var newperoson = await client.GetAsync($"Person/Create?name={name}&surname={fname}&patronymic={oname}&login={login}&password={paswword}&comp={Convert.ToInt32(comp)}&work={work.IsChecked}");
                var data2 = await newperoson.Content.ReadAsStringAsync();
                string data = Convert.ToString(data2);
                if (data == "true")
                    MessageBox.Show("Регистрация Завершена");
                else
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                this.Close();
            }
            else MessageBox.Show("Введите данные");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
