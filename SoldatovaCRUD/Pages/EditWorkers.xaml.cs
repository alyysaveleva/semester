using SoldatovaCRUD.classes;
using SoldatovaCRUD.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
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
        private string newsourcepath = string.Empty;
        string path = "";
        private Worker currentWorker = new Worker();
        DateTime datetoday = DateTime.Now;

        public EditWorkers(Worker selectedWorker)
        {
            InitializeComponent();
            if (selectedWorker != null)
            {
                currentWorker = selectedWorker;
            }
            DataContext = currentWorker;

        }

        private void Save(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentWorker.name))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(currentWorker.Login))
                errors.AppendLine("Укажите логин");
            if (currentWorker.RoleID==0)
                errors.AppendLine("Укажите роль");
            if (string.IsNullOrWhiteSpace(currentWorker.Password))
                errors.AppendLine("Укажите пароль");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (currentWorker.ID == 0)
            {
                currentWorker.Date = datetoday;
                currentWorker.Time = datetoday.TimeOfDay;
                currentWorker.Entry = 1;
                SoldatovaCRUDEntities.getcontext().Workers.Add(currentWorker);
            }
            DbContextTransaction dbContextTransaction = null;

            try
            {
                if (currentWorker.ID == 0)
                {
                    SoldatovaCRUDEntities.getcontext().Workers.Add(currentWorker);
                }

                dbContextTransaction = SoldatovaCRUDEntities.getcontext().Database.BeginTransaction();

                SoldatovaCRUDEntities.getcontext().SaveChanges();

                MessageBox.Show("Информация сохранена!");
                dbContextTransaction.Commit();
                manager.MainFrame.GoBack();
            }
            catch (DbUpdateException ex)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }

                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    MessageBox.Show($"Внутреннее исключение: {innerException.Message}");
                    innerException = innerException.InnerException;
                }

                MessageBox.Show("Ошибка при сохранении изменений. Дополнительные сведения в внутреннем исключении.");
            }
            catch (Exception ex)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }

                MessageBox.Show($"Ошибка при обновлении записей. Дополнительные сведения: {ex.Message}");
            }
            finally
            {
                dbContextTransaction?.Dispose();
            }
        }

     
        private void Choose_image(object sender, RoutedEventArgs e)
        {
            string Source = Environment.CurrentDirectory;
            if (ofd.ShowDialog() == true)
            {
                string SourcePath = ofd.SafeFileName;
                newsourcepath = Source.Replace("/bin/Debug", "/Res/Сотрудники_import/") + SourcePath;
                PreviewImage.Source = new BitmapImage(new Uri(ofd.FileName));
                path = ofd.FileName;
                currentWorker.Picture = $"/Res/Сотрудники_import/{ofd.SafeFileName}";
            }
        }
    }
}
/*
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

 */
