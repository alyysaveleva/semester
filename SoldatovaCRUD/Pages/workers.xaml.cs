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
    /// Логика взаимодействия для workers.xaml
    /// </summary>
    public partial class workers : Page
    {
        
        public workers()
        {
            InitializeComponent();
            DGworkers.ItemsSource = Models.SoldatovaCRUDEntities.getcontext().Workers.ToList();
            DGworkers.IsReadOnly = false;
            DGworkers.RowEditEnding += DGworkers_RowEditEnding;

        }

        private void DGworkers_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // Get the edited row's data
            var editedItem = e.Row.Item as workers; // Replace Worker with the appropriate type of your data

            // Update the corresponding SQL table row

         
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            classes.manager.MainFrame.Navigate(new Pages.EditWorkers((sender as Button).DataContext as Models.Worker));
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            classes.manager.MainFrame.Navigate(new Pages.EditWorkers(null));
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var workerForRemoving = DGworkers.SelectedItems.Cast<Models.Worker>().ToList();
            if(MessageBox.Show($"Вы хотите удалить {workerForRemoving.Count()}?", "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    Models.SoldatovaCRUDEntities.getcontext().Workers.RemoveRange(workerForRemoving);
                    Models.SoldatovaCRUDEntities.getcontext().SaveChanges();
                    MessageBox.Show("Все удалилось");
                }
                catch
                {
                    MessageBox.Show("Не-а");
                }
            }
           
        }
    }
}
