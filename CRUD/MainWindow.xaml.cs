using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;
using System.ComponentModel;

namespace SoldatovaCRUD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int sessionTimeInMinutes = 15;
        private int remainingTimeInSeconds;
        
        private DispatcherTimer timer;
        TimeSpan timeSpan = new TimeSpan(0, 0, 0); // Initialize a TimeSpan object with zero hours, minutes, and seconds

        

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Pages.Autorizacia());
            classes.manager.MainFrame = MainFrame;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            InitializeTimer();
            classes.currentuser.Activesession = true;
        }
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            StartSessionTimer();
        }

        private void StartSessionTimer()
        {
            remainingTimeInSeconds = sessionTimeInMinutes * 60;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            remainingTimeInSeconds--;
            if (remainingTimeInSeconds <= 1)
            {
                timer.Stop();
                Application.Current.Shutdown();
                classes.currentuser.Activesession = false;

            }
            if (remainingTimeInSeconds <= 0)
            {
                timer.Stop();
                Application.Current.Shutdown();
               
            }
            else
            {
                TimerText.Text = TimeSpan.FromSeconds(remainingTimeInSeconds).ToString(@"mm\:ss");

                if (remainingTimeInSeconds == 2 * 60) // Оповещение за 2 минуты до конца
                {
                    MessageBox.Show("До конца сессии осталось 2 минуты!");
                }
                
            }
        }
       
       

       
    }
}
