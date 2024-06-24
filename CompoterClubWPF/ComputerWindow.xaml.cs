using Domain.models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace CompoterClubWPF
{
    /// <summary>
    /// Логика взаимодействия для ComputerWindow.xaml
    /// </summary>
    public partial class ComputerWindow : Window
    {
        int idComp;
        int idhall;
        int idclitnt;
        public ComputerWindow(int idcomp, int idperson)
        {
            InitializeComponent();
            idComp = idcomp;
            idclitnt = idperson;
            this.Loaded += CompWindow_LoadedAsync;
        }
        private async void CompWindow_LoadedAsync(object sender, RoutedEventArgs e)
        {
            await CompAsync(idComp);
        }
        public async Task CompAsync(int idComp)
        {
            HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
            var hall = await client.GetAsync($"Computer/Comp?Comp={idComp}");
            var json = await hall.Content.ReadAsStringAsync();
            var stock = JsonConvert.DeserializeObject<Computer>(json);
            idcompL.Content = idcompL.Content + Convert.ToString(stock.IdPC);
            string phrase = stock.Description;
            string[] words = phrase.Split('<');
            foreach (string worf in words)
            {
                if (worf.Length !=0 )
                {
                    opesaT.Text += "<";
                }
                opesaT.Text += worf;
                opesaT.Text += Environment.NewLine;
            }
            idhall = stock.IdHall;
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client2 = new() { BaseAddress = new Uri("https://localhost:44314") };
            var newe = await client2.GetAsync($"Computer/Rent?comp={idComp}&description={"удалить"}");

        }
    }
}
