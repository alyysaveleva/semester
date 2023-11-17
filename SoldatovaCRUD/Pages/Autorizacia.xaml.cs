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
    /// Логика взаимодействия для Autorizacia.xaml
    /// </summary>
    public partial class Autorizacia : Page
    {
        public string ButtonVis = "invis";

        public Autorizacia()
        {
            InitializeComponent();

            classes.connect.modelbd = new Models.SoldatovaCRUDEntities();
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
                   
                    classes.manager.MainFrame.Navigate(new Pages.workers());

                    ButtonVis = "vis";
                    Application.Current.Resources["test"] = ButtonVis;
                }
                else if (userObj.RoleID == 3)
                {

                    classes.manager.MainFrame.Navigate(new Pages.workers());

                    ButtonVis = "vis";
                    Application.Current.Resources["test"] = ButtonVis;
                }

            }


        }

    }
}
