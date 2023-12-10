using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using SoldatovaCRUD.Models;

namespace SoldatovaCRUD.Pages
{
    /// <summary>
    /// Interaction logic for shopuser.xaml
    /// </summary>
    public partial class shopuser : Page
    {
        
        public int UsersID;
        public Order Item;
        public shopuser(int userID)
        {
            InitializeComponent();
          
            LVOrder.ItemsSource = Models.SoldatovaCRUDEntities.getcontext().Merches.ToList();

            
            UsersID = userID;

            var UserData = classes.connect.modelbd.Workers.FirstOrDefault(row => row.ID == UsersID);
            var userOrder = classes.connect.modelbd.Orders.FirstOrDefault(row => row.UserID == UsersID );
           
            if (userOrder != null )
            {
                Orderview.Visibility = Visibility.Visible;
                Item = userOrder;
            }
            else
            {
                Orderview.Visibility = Visibility.Hidden;
            }
        }

       





    public void orderButton_Click(object sender, RoutedEventArgs e)
        {

            Random rnd = new Random();
            int num = rnd.Next(1000, 9999);
          

         
            if (LVOrder.SelectedItem != null)
            {
                
        Models.Merch selectedMerch = LVOrder.SelectedItem as Models.Merch;

                if (!Application.Current.Properties.Contains("user"))
                {
                    // Обработка ситуации, когда значение не было установлено
                }
                else
                {
                 
                }
               
                Models.Order Item = new Models.Order
                {
                    Cost = selectedMerch.cost,
                    Amount = 1,
                    Sale = selectedMerch.sale,
                    Arrived = false,
                    UserID = UsersID,
                    Code = num,
                    DateOrder = DateTime.Today,
                    DateArrive = DateTime.Today.AddDays(7),
                    Place = 1,
                    MerchID = selectedMerch.ID
                };

                SoldatovaCRUDEntities.getcontext().Orders.Add(Item);
                SoldatovaCRUDEntities.getcontext().SaveChanges();
                classes.manager.MainFrame.Navigate(new orderMake(Item, UsersID));
            }
        }
      
        private void MyListView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                LVOrder.ContextMenu.IsEnabled = true;
                LVOrder.ContextMenu.PlacementTarget = LVOrder;
                LVOrder.ContextMenu.IsOpen = true;
            }
        }

        private void orderview(object sender, RoutedEventArgs e)
        {
            
            classes.manager.MainFrame.Navigate(new orderMake(Item, UsersID));
        }
    }
}
