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
using System.Collections.ObjectModel;
using SoldatovaCRUD.classes;
using SoldatovaCRUD.Models;
using System.ComponentModel;

namespace SoldatovaCRUD.Pages
{
    /// <summary>
    /// Логика взаимодействия для workers.xaml
    /// </summary>
    public partial class workers : Page
    {
        public ObservableCollection<Worker> workerCollection;
        private ICollectionView workersCollectionView;
        public string filterType;
        public ICollectionView WorkersCollection { get; set; }
        public workers()
        {
            InitializeComponent();
            //DGworkers.ItemsSource = SoldatovaCRUDEntities1.getcontext().Workers.ToList();
            //DGworkers.IsReadOnly = false;
            //DGworkers.RowEditEnding += DGworkers_RowEditEnding;
            workerCollection = new ObservableCollection<Worker>(SoldatovaCRUDEntities.getcontext().Workers.ToList());
            DGworkers.ItemsSource = workerCollection;
            //DGworkers.ItemsSource = SoldatovaCRUDEntities1.getcontext().Workers.ToList();
            Sortby.ItemsSource = new string[] { "name", "password", "login", "date", "time", "role"};
            SortDir.ItemsSource = Enum.GetNames(typeof(ListSortDirection));
            FilterBy.ItemsSource = new string[] { "name", "password", "login" };

            Sortby.SelectionChanged += SelectionChanged;
            SortDir.SelectionChanged += SelectionChanged;
            FilterBy.SelectionChanged += FilterSelectionChanged;

            DGworkers.Items.SortDescriptions.Add(new SortDescription("name", ListSortDirection.Ascending));

            workersCollectionView = CollectionViewSource.GetDefaultView(workerCollection);
            DataContext = this;

            // ... (Other initialization code)

            // Handle the TextChanged event of a TextBox used for filtering
            FilterTextBox.TextChanged += FilterTextBox_TextChanged;

        }


        private void FilterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var SortProperty = FilterBy.SelectedItem.ToString();
            if (SortProperty.ToString() == "name")
            {
                filterType = "name";
            }
            if (SortProperty.ToString() == "password")
            {
                filterType = "password";
            }
            if (SortProperty.ToString() == "login")
            {
                filterType = "login";
            }

            else
            {
                filterType = "name";
            }

        }
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            // Apply the filter based on the text entered in the TextBox
             if (workersCollectionView != null)
             {
                 workersCollectionView.Filter = item =>
                 {
                     if (item is Worker worker )
                     {
                         if(filterType == "name")
                         {
                             return worker.name.ToLower().Contains(FilterTextBox.Text.ToLower());
                         }
                         if (filterType == "password")
                         {
                             return worker.Password.ToLower().Contains(FilterTextBox.Text.ToLower());
                         }
                         if (filterType == "login")
                         {
                             return worker.Login.ToLower().Contains(FilterTextBox.Text.ToLower());
                         }
                         else
                         {
                             return worker.name.ToLower().Contains(FilterTextBox.Text.ToLower());
                         }
                         // Replace "PropertyName" with the actual property name you want to filter on
                         
                     }

                     return false;
                 };
             }
        }
        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SortList();

        }

        public void SortList()
        {
            var SortProperty = Sortby.SelectedItem.ToString();
            var SortDirection
                = SortDir.SelectedItem.ToString()
                == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending;
            DGworkers.Items.SortDescriptions[0] = new SortDescription(SortProperty, SortDirection);
        }

       

        private void DGworkers_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // Get the edited row's data
            var editedItem = e.Row.Item as workers; // Replace Worker with the appropriate type of your data

            // Update the corresponding SQL table row

         
        }
        private void RefreshPage()
        {
           workerCollection.Clear();
            foreach (var merch in SoldatovaCRUDEntities.getcontext().Workers.ToList())
            {
                workerCollection.Add(merch);
              }
        }

          private void Ref(object sender, RoutedEventArgs e)
          {
              RefreshPage();
          }
        private void Edit(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new Pages.EditWorkers((sender as Button).DataContext as Models.Worker));
        }

        private void Add(object sender, RoutedEventArgs e)
        {
           manager.MainFrame.Navigate(new Pages.EditWorkers(null));
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
     
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            manager.MainFrame.Navigate(new UserHistory());
        }

      
    }
}
