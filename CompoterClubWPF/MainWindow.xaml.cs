using Domain.models;
using Microsoft.Win32;
using MonitoringWPF;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;


namespace CompoterClubWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        private static extern int GetMenuItemCount(IntPtr hmenu);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        private static extern int DrawMenuBar(IntPtr hwnd);

        private const int MF_BYPOSITION = 0x0400;
        private const int MF_DISABLED = 0x0002;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                this.SourceInitialized += new EventHandler(Window1_SourceInitialized);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;

        }
        void Window1_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            IntPtr windowHandle = helper.Handle; //Get the handle of this window
            IntPtr hmenu = GetSystemMenu(windowHandle, 0);
            int cnt = GetMenuItemCount(hmenu);
            //remove the button
            RemoveMenu(hmenu, cnt - 1, MF_DISABLED | MF_BYPOSITION);
            //remove the extra menu line
            RemoveMenu(hmenu, cnt - 2, MF_DISABLED | MF_BYPOSITION);
            DrawMenuBar(windowHandle); //Redraw the menu bar
        }

        public static void Lock()
        {
            RegistryKey reg;
            string key = "1";
            string sub = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";

            reg = Registry.LocalMachine.OpenSubKey(sub, true);
            reg.SetValue("NoClose", key, RegistryValueKind.DWord);
            reg.Close();
        }

        public static void Unlock()
        {
            RegistryKey reg;
            string sub = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";

            reg = Registry.LocalMachine.OpenSubKey(sub, true);
            reg.DeleteValue("NoClose");
            reg.Close();
        }
        public bool worker;
        public string name;
        public string surname;
        public async Task workAsync(int idclient)
        {
            HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
            var peroson = await client.GetAsync($"Person/Work?IdClient={idclient}");
            worker = Convert.ToBoolean(await peroson.Content.ReadAsStringAsync());
        }
        public async Task persAsync(int idclient)
        {
            HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
            var peroson = await client.GetAsync($"Person/NamePerson?IdClient={idclient}");
            var json = await peroson.Content.ReadAsStringAsync();
            var stock = JsonConvert.DeserializeObject<Person>(json);
            name = stock.Name;
            surname = stock.Surname;

        }
        private async void Button_ClickAsync(object sender, RoutedEventArgs e)

        {
            string a = loginT.Text;
            string b = paswwordT.Text;
            if (a != "" && b != "")
            {
                var data = new { a, b };

                HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
                using HttpResponseMessage response = await client.PostAsJsonAsync($"person/Authorization?paswword={b}&login={a}", data);

                var data2 = await response.Content.ReadAsStringAsync();

                if (Convert.ToInt32(data2) > 0)
                {
                    await persAsync(Convert.ToInt32(data2));
                    await workAsync(Convert.ToInt32(data2));
                    if (worker)
                    {

                        TeacherWindow cl = new TeacherWindow(Convert.ToInt32(data2), name, surname);
                        cl.Show();
                        this.Hide();
                        clo = false;
                    }
                    else
                    {
                        EmployeeWindow em = new EmployeeWindow(Convert.ToInt32(data2), name, surname);
                        em.Show();
                        clo = false;
                        this.Hide();
                    }

                }
                else
                {
                    MessageBox.Show("Неверные данные");
                }

            }
            else
            {
                MessageBox.Show("Введите данные");
            }
        }
        private class TaskManager
        {

            public static void LockExplorer()
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "taskkill.exe",
                        Arguments = "/F /IM explorer.exe",
                        UseShellExecute = true,
                        CreateNoWindow = true
                    }
                };
                proc.Start();
            }
            public static void Lock()
            {
                RegistryKey reg;
                string key = "1";
                string sub = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";

                reg = Registry.CurrentUser.CreateSubKey(sub);
                reg.SetValue("DisableTaskMgr", key);
                reg.Close();
            }
            public static void Unlock()
            {
                RegistryKey reg;
                string sub = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";

                reg = Registry.CurrentUser.OpenSubKey(sub, true);
                reg.DeleteValue("DisableTaskMgr");
                reg.Close();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                clo = true;
                Lock();
                TaskManager.Lock();
                TaskManager.LockExplorer();
            }
            catch
            {
                MessageBox.Show("Запустите от имени Администратора", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }
        bool clo;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (clo)
                e.Cancel = true;
            else
                e.Cancel = false;

        }

        private void Label_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            clo = false;
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            proc.StartInfo.FileName = "C:\\Windows\\explorer.exe";
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
            System.Diagnostics.Process.Start("cmd", "/c shutdown -s -f -t 00");
        }
    }
}
