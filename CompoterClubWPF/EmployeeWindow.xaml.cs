using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using IWshRuntimeLibrary;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using MonitoringWPF;
using System.Drawing;
using System.Windows.Automation.Peers;
using System.Threading.Tasks;

namespace CompoterClubWPF
{

    public partial class EmployeeWindow : Window
    {
        string name;
        string surname;
        int id;
        int chet;
        DispatcherTimer timer;
        public EmployeeWindow(int idperson, string name, string surname)
        {
            InitializeComponent();
            id = idperson;
            this.name = name;
            this.surname = surname;
            idclientL.Content = "Ваш ID: " + idperson;
            PersonL.Content = name + " " + surname;
            hi.Content = hi.Content + " " + name + " " + surname;
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            this.DataContext = this;
            clo = true;
            Swi();
            fon = new Window1();
            fon.Show();
            ImagesListBox.MouseDoubleClick += ImagesListBox_MouseDoubleClick;
    
        }
        public static Window1 fon;
        public ObservableCollection<ImageSource> ImageCollection { get; set; }
        private void ImagesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ImageSource selectedIcon = (ImageSource)ImagesListBox.SelectedItem;
            string lnkFilePath = GetLnkFilePath(selectedIcon);
            if (!string.IsNullOrEmpty(lnkFilePath))
            {
                Basd(Iarlik(lnkFilePath));
                Process.Start(Iarlik(lnkFilePath));
                CheckProcess(Iarlik(lnkFilePath));
            }
        }
        async void  Basd(string a)
        {
            string fileName = Path.GetFileName(a);
            string escription = "<" + DateTime.Now.ToLongTimeString() + ">  Запустил: " + fileName + " ;" + "\r\n";
            var data = new { id };
            HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
            using HttpResponseMessage response = await client.PostAsJsonAsync($"person/Switch?id={id}", data);
            var data2 = await response.Content.ReadAsStringAsync();
            int b = Convert.ToInt32(data2);
            HttpClient client2 = new() { BaseAddress = new Uri("https://localhost:44314") };
            var newe = await client2.GetAsync($"Computer/Rent?comp={b}&description={escription}");
        }
        private async void CheckProcess(string a)
        {
            string _processName = Path.GetFileName(a);
            await Task.Delay(1000);
            while (true)
            {
                await Task.Delay(5000); // Подождать 5 секунд перед следующей проверкой

                try
                {
                    // Проверяем, запущено ли указанное приложение
                    Process[] processes = Process.GetProcessesByName(_processName);
                    MessageBox.Show(_processName);
                    if (processes.Length == 0)
                    {
                        string escription = "<" + DateTime.Now.ToLongTimeString() + ">  Закрыт : " + _processName + " ;" + "\r\n";
                        var data = new { id };
                        HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
                        using HttpResponseMessage response = await client.PostAsJsonAsync($"person/Switch?id={id}", data);
                        var data2 = await response.Content.ReadAsStringAsync();
                        int b = Convert.ToInt32(data2);
                        HttpClient client2 = new() { BaseAddress = new Uri("https://localhost:44314") };
                        var newe = await client2.GetAsync($"Computer/Rent?comp={b}&description={escription}");
                        break; // Завершаем цикл
                    }
                }
                catch (Exception ex)
                {
                    // Обработка ошибок
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break; // Завершаем цикл
                }
            }
        }
        private string GetLnkFilePath(ImageSource icon)
        {
            for (int i = 0; i < ImageCollection.Count; i++)
            {
                if (ImageCollection[i].Equals(icon))
                {
                    string lnkFilePath = lnkFiles[i];
                    return lnkFilePath;
                }
            }
            return null;
        }

        string[] lnkFiles;
        public void Programm()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directoryInfo = Directory.GetParent(currentDirectory);
            directoryInfo = Directory.GetParent(directoryInfo.FullName); 
            directoryInfo = Directory.GetParent(directoryInfo.FullName); 
            string projectPath = directoryInfo.FullName;
            string folderPath = projectPath+@"\Resurs\programm";
            ImageCollection = new ObservableCollection<ImageSource>();
            lnkFiles = Directory.GetFiles(folderPath, "*.lnk");
            foreach (string lnkFile in lnkFiles)
            {
                ImageSource icon = DisplayIcon(Iarlik(lnkFile));
                if (icon != null)
                {
                    ImageCollection.Add(icon);
                }
            }

            ImagesListBox.ItemsSource = ImageCollection;
        }
        public string p;
        public void timer_Tick(object sender, EventArgs e)
        {
            chet++;
            label1.Content = DateTime.Now.ToLongTimeString();

        }

        private async void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            string escription = "<" + DateTime.Now.ToLongTimeString() + ">  " + surname + " " + name + " покинул систему;" + "\r\n";
            var data = new { id };
            HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
            using HttpResponseMessage response = await client.PostAsJsonAsync($"person/Switch?id={id}", data);
            var data2 = await response.Content.ReadAsStringAsync();
            int b = Convert.ToInt32(data2);
            HttpClient client2 = new() { BaseAddress = new Uri("https://localhost:44314") };
            var newe = await client2.GetAsync($"Computer/Rent?comp={b}&description={escription}");
            clo = false;
            timer.Stop();
            MainWindow a = new MainWindow();
            fon.Close();
            a.Show();
            this.Close();
        }
        bool clo = false;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (clo)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        private async void Swi()
        {
            string escription = "<" + DateTime.Now.ToLongTimeString() + ">  " + surname + " " + name + " вошел в систему;" + "\n";
            var data = new { id };
            HttpClient client = new() { BaseAddress = new Uri("https://localhost:44314") };
            using HttpResponseMessage response = await client.PostAsJsonAsync($"person/Switch?id={id}", data);
            var data2 = await response.Content.ReadAsStringAsync();
            int a = Convert.ToInt32(data2);
            HttpClient client2 = new() { BaseAddress = new Uri("https://localhost:44314") };
            var newe = await client2.GetAsync($"Computer/Rent?comp={a}&description={escription}");
        }
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern uint ExtractIconEx(string szFileName, int nIconIndex, out IntPtr phiconLarge, out IntPtr phiconSmall, uint nIcons);
        private string Iarlik(string shortcutPath)
        {
            WshShell shell = new WshShell();
            // Путь к ярлыку
            // Получаем объект ярлыка
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            // Выводим путь к исполняемому файлу, указанному в ярлыке
            return shortcut.TargetPath;
        }

            private ImageSource DisplayIcon(string executablePath)
        {
            // Извлекаем большую и маленькую иконки
            ExtractIconEx(executablePath, 0, out IntPtr largeIcon, out IntPtr smallIcon, 1);
            if (largeIcon != IntPtr.Zero)
            {
                // Конвертируем иконку в ImageSource для WPF
                ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                    largeIcon,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
                // Отображаем иконку на форме
                return imageSource;
            }
            return null;

        }
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr hIcon);
       
        public void StartApplication(string filePath)
        {
            try
            {
                // Создаем новый экземпляр процесса
                Process process = new Process();

                // Устанавливаем путь к исполняемому файлу программы
                process.StartInfo.FileName = filePath;

                // Запускаем программу
                process.Start();
            }
            catch (Exception ex)
            {
                // В случае ошибки выводим сообщение
                Console.WriteLine("Ошибка при запуске программы: " + ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Programm();
        }
    }
    public class ProcessWindow
    {
        public string WindowTitle { get; private set; }
        public Process Process { get; private set; }

        public ProcessWindow(string windowTitle, Process process)
        {
            WindowTitle = windowTitle;
            Process = process;
        }
    }

    public static class ProcessHelper
    {
        /// <summary>
        /// Возвращает массив приложений запущенных пользователем
        /// Из результата исклюлючаются текущий процесс и explorer
        /// </summary>
        public static ProcessWindow[] GetRunningApplications()
        {
            var allProccesses = Process.GetProcesses();
            var myPid = Process.GetCurrentProcess().Id;
            var explorerPids = allProccesses.Where(p => "explorer".Equals(p.ProcessName, StringComparison.OrdinalIgnoreCase)).Select(p => p.Id).ToArray();
            var windows = new List<ProcessWindow>();
            EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                var sbTitle = new StringBuilder(255);
                GetWindowText(hWnd, sbTitle, sbTitle.Capacity + 1);
                string windowTitle = sbTitle.ToString();

                if (!string.IsNullOrEmpty(windowTitle) && IsWindowVisible(hWnd))
                {
                    int pid;
                    GetWindowThreadProcessId(hWnd, out pid);
                    if (pid != myPid && !explorerPids.Contains(pid))
                    {
                        windows.Add(new ProcessWindow(windowTitle, allProccesses.FirstOrDefault(p => p.Id == pid)));
                    }
                }

                return true;
            };

            EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);
            return windows.ToArray();
        }

        delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport("user32.dll", EntryPoint = "GetWindowText",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);
    }
}
