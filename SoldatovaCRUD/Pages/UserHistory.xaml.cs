using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UserHistory.xaml
    /// </summary>
    public partial class UserHistory : Page
    {
        private ObservableCollection<Models.EntryHistory> workerCollection;
        public UserHistory()
        {
            InitializeComponent();


            DGworkers.ItemsSource = classes.connect.modelbd.EntryHistories.ToList();

        }
       
        private void RefreshPage()
        {
            workerCollection.Clear();
            foreach (var merch in Models.SoldatovaCRUDEntities2.getcontext().EntryHistories.ToList())
            {
                workerCollection.Add(merch);
            }
        }
        private void Ref(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }
    }
}
