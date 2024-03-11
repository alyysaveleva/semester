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
    /// Interaction logic for shop.xaml
    /// </summary>
    public partial class shop : Page
    {
        bool vis = false;
        /// <summary>
        /// подключение ListView к таблице Merch
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public shop()
        {
           

            InitializeComponent();
            //подключение ListView к таблице Merch
            LVOrder.ItemsSource = Models.SoldatovaCRUDEntities2.getcontext().Merches.ToList();

            if (ButtonVis)
            {
                delButton.Visibility = Visibility.Visible;
                addButton.Visibility = Visibility.Visible;
                
            }
            else
            {
                delButton.Visibility = Visibility.Hidden;
                addButton.Visibility = Visibility.Hidden;
            }
        }
        public bool ButtonVis
        {
            get
            {
               

                    object ButtonVis = Application.Current.Resources["test"];

                    if (ButtonVis == null)
                    {
                        return false;

                    }
                    else
                    {

                        return true;

                    }
               
            }
        }
        private void DGworkers_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // Get the edited row's data
            var editedItem = e.Row.Item as workers; // Replace Worker with the appropriate type of your data

            // Update the corresponding SQL table row


        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            classes.manager.MainFrame.Navigate(new Pages.shopedit((sender as Button).DataContext as Models.Merch));
          
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            classes.manager.MainFrame.Navigate(new Pages.shopedit(null));
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var workerForRemoving = LVOrder.SelectedItems.Cast<Models.Merch>().ToList();
            if (MessageBox.Show($"Вы хотите удалить {workerForRemoving.Count()}?", "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    Models.SoldatovaCRUDEntities2.getcontext().Merches.RemoveRange(workerForRemoving);
                    Models.SoldatovaCRUDEntities2.getcontext().SaveChanges();
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
