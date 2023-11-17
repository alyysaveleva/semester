using Microsoft.Win32;
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
    /// Логика взаимодействия для EditWorkers.xaml
    /// </summary>
    public partial class EditWorkers : Page
    {
        public OpenFileDialog ofd = new OpenFileDialog();
        private Models.Worker currentWorker = new Models.Worker();
        private string newsourcepath = string.Empty;
        string path = "";
        private bool flag = false;
        public EditWorkers(Models.Worker selectedWorker)
        {
            InitializeComponent();
            if (selectedWorker != null) currentWorker = selectedWorker;
            DataContext = currentWorker;
        }
        DateTime datetoday = DateTime.Now;
        private void Save(object sender, RoutedEventArgs e)

        {
            if(currentWorker.ID == 0)
            {
                currentWorker.Date = datetoday;
                currentWorker.Time = datetoday.TimeOfDay;
                //currentWorker.Picture = "/Сотрудники_import/Иванов.jpeg";
                currentWorker.Entry = 1;
                Models.SoldatovaCRUDEntities.getcontext().Workers.Add(currentWorker);

                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(currentWorker.name))
                {
                    errors.AppendLine("укажите имя");
                }
                if (string.IsNullOrWhiteSpace(currentWorker.Login))
                {
                    errors.AppendLine("укажите логин");
                }
                if (string.IsNullOrWhiteSpace(currentWorker.Password))
                {
                    errors.AppendLine("укажите пароль");
                    if(currentWorker.Password.Length > 20)
                    {
                        errors.AppendLine("Пароль болше 20 символов");
                    }
                }
                if (string.IsNullOrWhiteSpace(currentWorker.Picture))
                {
                    errors.AppendLine("укажите картинку");
                }
             
                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "Ошибка",  MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {

                        MessageBox.Show("added", "done");
                        Models.SoldatovaCRUDEntities.getcontext().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}", "Что-то не так");
                    }
                }
            }
           
        }

        private void Choose_image(object sender, RoutedEventArgs e)
        {
            string Source = Environment.CurrentDirectory;
            if (ofd.ShowDialog() == true)
            {
                flag = true;
                string SourcePath = ofd.SafeFileName;
                newsourcepath = Source.Replace("/bin/Debug", "/Сотрудники_import/") + SourcePath;
                PreviewImage.Source = new BitmapImage(new Uri(ofd.FileName));
                path = ofd.FileName;
                currentWorker.Picture = $"/Сотрудники_import/{ofd.SafeFileName}";
            }
        }
    }
}
