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

namespace SoldatovaCRUD.Pages
{

    /// <summary>
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : Page
    {
        int mistakes = 0;
        int mistakesCaptcha = 0;
        DateTime datetoday = DateTime.Now;
        DateTime timeNow = DateTime.Now;
        bool right = false;

        public Registr()
        {
           
            InitializeComponent();
            //подключение к БД
            classes.connect.modelbd = new Models.SoldatovaCRUDEntities();
            captcha.Visibility = Visibility.Collapsed;
            captchaPicture.Visibility = Visibility.Collapsed;
            captchatext.Visibility = Visibility.Collapsed;
            CaptchaCheck.Visibility = Visibility.Collapsed;
            timeout.Visibility = Visibility.Collapsed;
        }


        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        private void test()
        {
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 5, 0); // 5 minutes
            dispatcherTimer.Start();
            timeout.Text = $"{dispatcherTimer.Interval}";

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Code to display a message box
            MessageBox.Show("Timer ticked!");

            registr.Visibility = Visibility.Visible;
        }

  

        public bool Captcha()
        {
            captcha.Visibility = Visibility.Visible;
            captchaPicture.Visibility = Visibility.Visible;
            captchatext.Visibility = Visibility.Visible;
            CaptchaCheck.Visibility = Visibility.Visible;

          
            String allowchar = " ";

            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";

            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";

            allowchar += "1,2,3,4,5,6,7,8,9,0";

            char[] a = { ',' };

            String[] ar = allowchar.Split(a);

            String pwd = " ";

            string temp = " ";

            Random r = new Random();



            for (int i = 0; i < 6; i++)

            {
                temp = ar[(r.Next(0, ar.Length))];

                pwd += temp;

            }

            captchaPicture.Text = pwd;
            captchatext.Text = pwd;
            if(captcha.Text == pwd)
            {
                MessageBox.Show("!!!!", $"{mistakes}",
                  MessageBoxButton.OK, MessageBoxImage.Error);
                right = true;
                return right;
            }
            else
            {
                MessageBox.Show("Капча введена неверно", "0_0",
                 MessageBoxButton.OK, MessageBoxImage.Error);
                right = false;
                return right;
            }
        }
        private void Registraciya(object sender, RoutedEventArgs e)
        {
            // Получаем данные из полей ввода
            string login = login_.Text;
            string password = password_.Password;
            string confirmPassword = ConfirmPassword.Password;
            DateTime datetoday = DateTime.Now;

            // Проверяем, совпадают ли пароль и подтверждение пароля
            if (password != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка при регистрации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверяем, существует ли пользователь с таким логином
            var existingUser = classes.connect.modelbd.Workers.FirstOrDefault(u => u.Login == login);
            if (existingUser != null)
            {
                if(mistakes < 3)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", $"{mistakes}",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                    mistakes++;

                    return;
                }
                else
                {
                    MessageBox.Show("????", $"{mistakes}",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                    Captcha();
                    return;
                }
               
            }
           

            // Создаем нового пользователя
            var newUser = new Models.Worker
            {
                name = "user",
                Login = login,
                Password = password,
                RoleID = 4, // 3 соответствует роли "Client"
                Date = datetoday,
                Time = datetoday.TimeOfDay,
               Entry = 1,
               
                Picture = "/Res/Сотрудники_import/Иванов.jpeg"


            };

            // Добавляем пользователя в таблицу users
            classes.connect.modelbd.Workers.Add(newUser);

            try
            {
                // Сохраняем изменения в базе данных
                classes.connect.modelbd.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно!", "Успешная регистрация",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Очищаем поля ввода
                login_.Clear();
                password_.Clear();
                ConfirmPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка при регистрации",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                if (mistakes < 3)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", $"{mistakes}",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                    mistakes++;

                    return;
                }
                else
                {
                    MessageBox.Show("????", $"{mistakes}",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                    Captcha();
                    return;
                    
                }
            }
        }

        private void CaptchaCheck_Click(object sender, RoutedEventArgs e)
        {
            if (right)
            {
                registr.Visibility = Visibility.Visible;

                MessageBox.Show("incorrect", $"{mistakes}",
                   MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
               
                MessageBox.Show("correct", $"{mistakes}",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                mistakesCaptcha++;
                if(mistakesCaptcha > 2)
                {
                    MessageBox.Show("Бан", $"{mistakesCaptcha}",
                  MessageBoxButton.OK, MessageBoxImage.Error);
                    test();

                    timeout.Visibility = Visibility.Visible;
                     registr.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
