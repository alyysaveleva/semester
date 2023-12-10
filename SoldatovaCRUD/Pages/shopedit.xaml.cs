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
using Microsoft.Win32;

namespace SoldatovaCRUD.Pages
{
    /// <summary>
    /// Interaction logic for shopedit.xaml
    /// </summary>
    public partial class shopedit : Page
    {
        public OpenFileDialog ofd = new OpenFileDialog();
        private Models.Merch currentProduct = new Models.Merch();
        private string newsourcepath = string.Empty;
        string path = "";
        public shopedit(Models.Merch selectedProduct)
        {
            InitializeComponent();
            if (selectedProduct != null) currentProduct = selectedProduct;
            DataContext = currentProduct;
        }
        DateTime datetoday = DateTime.Now;
        private void Save(object sender, RoutedEventArgs e)

        {
            if (currentProduct.ID == 0)
            {
                
                Models.SoldatovaCRUDEntities.getcontext().Merches.Add(currentProduct);

                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrWhiteSpace(currentProduct.name))
                {
                    errors.AppendLine("укажите имя");
                }
                if (currentProduct.cost == 0)
                {
                    errors.AppendLine("укажите цену");
                }
               
                if (string.IsNullOrWhiteSpace(currentProduct.picture))
                {
                    errors.AppendLine("укажите картинку");
                }

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void Choose_image(object sender, RoutedEventArgs e)
        {
            string Source = Environment.CurrentDirectory;
            if (ofd.ShowDialog() == true)
            {
                string SourcePath = ofd.SafeFileName;
                newsourcepath = Source.Replace("/bin/Debug", "/Res/stuff/") + SourcePath;
                PreviewImage.Source = new BitmapImage(new Uri(ofd.FileName));
                path = ofd.FileName;
                currentProduct.picture = $"/Res/stuff/{ofd.SafeFileName}";
            }
        }
    }
}
