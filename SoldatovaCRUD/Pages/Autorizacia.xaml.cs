using BookClub;
using SoldatovaCRUD.classes;
using SoldatovaCRUD.Models;
using SoldatovaCRUD.Pages;
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
using System.Windows.Threading;
using SoldatovaCRUD.Models;

namespace SoldatovaCRUD.Pages
{

    /// <summary>
    /// Логика взаимодействия для Autorizacia.xaml
    /// </summary>
    public partial class Autorizacia : Page
    {
        public DateTime TimeLogin = DateTime.Now;
        DispatcherTimer timer = new DispatcherTimer();
        TimeSpan duration;
        Random random = new Random();
        string symbols = "";
        private int attempts = 0;
        public EntryHistory userHistory;
        public Autorizacia()
        {
            InitializeComponent();
            duration = TimeSpan.FromMinutes(1);
            StartTimer();
            classes.connect.modelbd = new Models.SoldatovaCRUDEntities();
            if(classes.currentuser.Activesession == true)
            {
                duration = TimeSpan.FromMinutes(1);

                LoginTimerTB.Visibility = Visibility.Visible;
                LoginBlock.Visibility = Visibility.Collapsed;
                BlockedTB.Text = "Время сеанса истекло!";
                vxod.IsEnabled = false;
                StartTimer();
                
            }
           


        }
       

        private void StartTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timerTick;
            timer.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (duration == TimeSpan.Zero)
            {
                timer.Stop();
                LoginTimerTB.Visibility = Visibility.Hidden;
                LoginBlock.Visibility = Visibility.Visible;
                BlockedTB.Text = "";
                vxod.IsEnabled = true;
                CaptchaTbBlock.Visibility = Visibility.Collapsed;
                attempts = 0;
                duration = TimeSpan.FromSeconds(10);
            }
            else
            {
                duration = duration.Add(TimeSpan.FromSeconds(-1));
                LoginTimerTB.Text = duration.ToString("c");
            }
        }
        private void BtnUpdateCaptcha_Click(object sender, RoutedEventArgs e)
        {
            UpdateCaptcha();
        }

        private void UpdateCaptcha()
        {
            symbols = "";
            CaptchaTB.Text = "";
            SPanelSymbols.Children.Clear();
            CanvasNoise.Children.Clear();

            GenerateSymbols(4);
            GenerateNoise(32);
        }

        private void GenerateSymbols(int count)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            for (int i = 0; i < count; i++)
            {
                string symbol = alphabet.ElementAt(random.Next(0, alphabet.Length)).ToString();
                TextBlock lbl = new TextBlock();

                lbl.Text = symbol;
                lbl.FontSize = random.Next(24, 32);
                lbl.RenderTransform = new RotateTransform(random.Next(-24, 24));
                lbl.Margin = new Thickness(16, 0, 16, 0);

                SPanelSymbols.Children.Add(lbl);

                symbols = symbols + symbol;
            }
        }

        private void GenerateNoise(int volumeNoise)
        {
            for (int i = 0; i < volumeNoise; i++)
            {
                Border border = new Border();
                border.Background = new SolidColorBrush(Color.FromArgb((byte)random.Next(32, 128), (byte)random.Next(0, 128), (byte)random.Next(0, 128), (byte)random.Next(0, 128)));
                border.Height = random.Next(4, 8);
                border.Width = random.Next(256, 512);

                border.RenderTransform = new RotateTransform(random.Next(0, 360));

                CanvasNoise.Children.Add(border);
                Canvas.SetLeft(border, random.Next(0, 200));
                Canvas.SetTop(border, random.Next(0, 75));


                Ellipse ellipse = new Ellipse();
                ellipse.Fill = new SolidColorBrush(Color.FromArgb((byte)random.Next(32, 64), (byte)random.Next(0, 128), (byte)random.Next(0, 128), (byte)random.Next(0, 128)));
                ellipse.Height = ellipse.Width = random.Next(20, 40);

                CanvasNoise.Children.Add(ellipse);
                Canvas.SetLeft(ellipse, random.Next(0, 400));
                Canvas.SetTop(ellipse, random.Next(0, 100));
            }
        }

        private void CheckAttemps()
        {
            if (attempts == 2)
            {
                
                CaptchaTbBlock.Visibility = Visibility.Visible;
                CaptchaBlock.Visibility = Visibility.Visible;
                UpdateCaptcha();
                MessageBox.Show("Пройдите капчу.", "Не удается войти!", MessageBoxButton.OK, MessageBoxImage.Warning);

                if (CaptchaTB.Text != symbols)
                {
                    vxod.Visibility = Visibility.Hidden;

                }
                else vxod.Visibility = Visibility.Visible;
            }
            else
            {
                if (attempts == 3)
                {
                    duration = TimeSpan.FromSeconds(10);

                    LoginTimerTB.Visibility = Visibility.Visible;
                    LoginBlock.Visibility = Visibility.Collapsed;
                    BlockedTB.Text = "Превышено количество попыток авторизации.\nВозможность входа заблокирована.";
                    vxod.IsEnabled = false;
                    StartTimer();
                }

            }
        }

        private void CheckCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaTB.Text == symbols)
            {
                vxod.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Неправильно введена капча", "Капча", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
                
        }
        //переход на страницу Registr при нажатии кнопки
        private void Registr(object sender, RoutedEventArgs e)
        {
            classes.manager.MainFrame.Navigate(new Registr());
        }
        private string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 3)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        //вход под разными ролями и проверка заполненности данных
        public async void Vxod(object sender, RoutedEventArgs e)
        {
            string login = Login.Text;
            string password = Password.Password;
            CheckAttemps();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                //throw new Exception("Поля логин и пароль не могут быть пустыми.");
                MessageBox.Show("Поля логин и пароль не могут быть пустыми.", "!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                attempts += 1;
            }

           

            //подключение к БД
            var userObj = classes.connect.modelbd.Workers.FirstOrDefault(
                x => x.Login == Login.Text && Password.Password == x.Password);
            //если данные пусты
            Application.Current.Resources["LoginTest"] = Login.Text;
            Application.Current.Resources["PasswordTest"] = Password.Password;
            
            if (userObj == null)
            {
                MessageBox.Show("Пользователя с такими данными не существует!", "Ошибка при авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //вход и переход на разные страницы 
            else
            {

               int UserID = userObj.ID;
                EntryHistory userhistory = new EntryHistory
                {
                    DateTime = TimeLogin,
                    UserID = UserID
                };

                if (userObj.RoleID == 1)
                {

                    classes.currentuser.AppBooted = TimeLogin;
                    classes.currentuser.ActiveUserID = UserID;
                    SoldatovaCRUDEntities.getcontext().EntryHistories.Add(userhistory);
                    SoldatovaCRUDEntities.getcontext().SaveChanges();
                    classes.manager.MainFrame.Navigate(new Pages.workers());

                    
                }
                else if (userObj.RoleID == 2)
                {
                    App.Current.Resources["test"] = "Vis";
                    classes.currentuser.AppBooted = TimeLogin;
                    classes.currentuser.ActiveUserID = UserID;
                    SoldatovaCRUDEntities.getcontext().EntryHistories.Add(userhistory);
                    SoldatovaCRUDEntities.getcontext().SaveChanges();
                    classes.manager.MainFrame.Navigate(new Pages.shop());

                }
                else if (userObj.RoleID == 3)
                {
                    classes.currentuser.AppBooted = TimeLogin;
                    classes.currentuser.ActiveUserID = UserID;
                    SoldatovaCRUDEntities.getcontext().EntryHistories.Add(userhistory);
                    SoldatovaCRUDEntities.getcontext().SaveChanges();
                    classes.manager.MainFrame.Navigate(new Pages.shop());


                }
                else if (userObj.RoleID == 4)
                {
                    classes.currentuser.AppBooted = TimeLogin;
                    classes.currentuser.ActiveUserID = UserID;
                    SoldatovaCRUDEntities.getcontext().EntryHistories.Add(userhistory);
                    SoldatovaCRUDEntities.getcontext().SaveChanges();
                    classes.manager.MainFrame.Navigate(new Pages.shopuser(UserID));
                    
                    


                }
            }

        }
    }
}
/*
   public string ButtonVis = "invis";

        public Autorizacia()
        {
            InitializeComponent();

            classes.connect.modelbd = new Models.SoldatovaCRUDEntities1();
            SoldatovaCRUD.App.Current.Resources["test"] = "invis";
        }
        //переход на страницу Registr при нажатии кнопки
        private void Registr(object sender, RoutedEventArgs e)
        {
            classes.manager.MainFrame.Navigate(new Registr());
        }

        //вход под разными ролями и проверка заполненности данных
        public void Vxod(object sender, RoutedEventArgs e)
        {

            //подключение к БД
            var userObj = classes.connect.modelbd.Workers.FirstOrDefault(
                x => x.Login == Login.Text && Password.Password == x.Password);
            //если данные пусты
            Application.Current.Resources["LoginTest"] = Login.Text;
            Application.Current.Resources["PasswordTest"] = Password.Password;
            if (userObj == null)
            {
                MessageBox.Show("Пользователя с такими данными не существует!", "Ошибка при авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //вход и переход на разные страницы 
            else
            {
                if (userObj.RoleID == 1)
                {
                    classes.manager.MainFrame.Navigate(new Pages.workers());

                    ButtonVis = "vis";
                    Application.Current.Resources["test"] = ButtonVis;
                }
                else if (userObj.RoleID == 2)
                {
                   
                    classes.manager.MainFrame.Navigate(new Pages.shop());

                    ButtonVis = "vis";
                    Application.Current.Resources["test"] = ButtonVis;
                }
                else if (userObj.RoleID == 3)
                {

                    classes.manager.MainFrame.Navigate(new Pages.shop());

                  
                }
                else if (userObj.RoleID == 4)
                {

                    classes.manager.MainFrame.Navigate(new Pages.shop());


                }
            }

  */