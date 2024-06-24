using CompoterClubWPF;
using Domain.models;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MonitoringWPF
{
    /// <summary>
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        int id;
        public TeacherWindow(int idperson, string name, string surname)
        {
            InitializeComponent();
            id = idperson;
            idclientL.Content = "Ваш ID: " + idperson;
            PersonL.Content = name + " " + surname;
            this.Loaded += ClientWindow_LoadedAsync;

            RegistryKey reg;
            string sub = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
            reg = Registry.CurrentUser.OpenSubKey(sub, true);
            reg.DeleteValue("DisableTaskMgr");
            reg.Close();

            RegistryKey reg2;
            string sub2 = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";
            reg2 = Registry.LocalMachine.OpenSubKey(sub2, true);
            reg2.DeleteValue("NoClose");
            reg2.Close();

            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            proc.StartInfo.FileName = "C:\\Windows\\explorer.exe";
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
        private async void ClientWindow_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await HallsAsync();
        }

        public async Task HallsAsync()
        {
            HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
            var hall = await client.GetAsync($"Room/Halls");
            var json = await hall.Content.ReadAsStringAsync();
            var stock = JsonConvert.DeserializeObject<List<Room>>(json);
            hallBox.ItemsSource = stock.Select(e => e.IdHall);
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            MainWindow a = new MainWindow();
            a.Show();
            this.Close();
        }

        private async void DataB_ClickAsync(object sender, RoutedEventArgs e)
        {
            await HallsAsync();
        }
        List<Button> btnlist = new List<Button>();
        public async Task Computers(int hall)
        {
            HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
            var comp = await client.GetAsync($"Computer/Computers?hall={hall}");
            var json = await comp.Content.ReadAsStringAsync();
            var stock = JsonConvert.DeserializeObject<List<Computer>>(json);
            foreach (var i in stock)
            {

                Button btn = new Button();
                btn.Content = i.IdPC;
                if (i.ComputerStatus == "не работает")
                    btn.Background = Brushes.Red;
                if (i.ComputerStatus == "работает")
                    btn.Background = Brushes.Green;
                btn.Width = 40;
                btn.Height = 40;
                btn.Margin = new Thickness(5, 5, 0, 0);
                btn.Click += Btn_Click;

                btnlist.Add(btn);
            }
            foreach (var item in btnlist)
            {
                stackpan.Children.Add(item);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            int a = (int)((Button)sender).Content;
            ComputerWindow Comp = new ComputerWindow(a, id);
            Comp.Show();
        }

        private async void hallBox_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            btnlist.Clear();
            stackpan.Children.Clear();
            int idhall = (int)hallBox.SelectedItem;
            await Computers(idhall);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Regist a = new Regist();
            a.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateComp Comp = new CreateComp();
            Comp.Show();
        }
    }
}
