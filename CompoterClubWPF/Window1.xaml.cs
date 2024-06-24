using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MonitoringWPF
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.Topmost = false;
            this.ShowInTaskbar = false;
        }

            
    }
}
