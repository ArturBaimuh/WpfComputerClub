using System;
using System.Net.Http;
using System.Windows;

namespace MonitoringWPF
{
    /// <summary>
    /// Логика взаимодействия для CreateComp.xaml
    /// </summary>
    public partial class CreateComp : Window
    {
        public CreateComp()
        {
            InitializeComponent();

        }

        private async void payB_ClickAsynck(object sender, RoutedEventArgs e)
        {
            HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
            var newperoson = await client.GetAsync($"Computer/Create?hall={1}&status={"не работает"}&description={opesaT.Text}");
            this.Close();
        }
    }
}