using SoldatovaCRUD.Models;
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
using System.Windows.Threading;
using System.ComponentModel;

namespace SoldatovaCRUD.Pages
{
    /// <summary>
    /// Interaction logic for orderMake.xaml
    /// </summary>
    public partial class orderMake : Page
    {


        private DispatcherTimer timer;
        private Order currentItem = new Order();
        public string PlaceName;
        public string merchname;
        public int newcost;
        public int? newsale;
        public DateTime datearr;
        public DateTime dateord;
        public int Userr;
        public int count = 1;

        public ObservableCollection<OrderMerch> merchord1;
        // private Models.Merch currentMerch = new Models.Merch();
        public orderMake(Order Item, int UsersID)
        {
            InitializeComponent();
            // LVItems.ItemsSource = SoldatovaCRUDEntities1.getcontext().Orders.ToList();
            var UserData = classes.connect.modelbd.Workers.FirstOrDefault(row => row.ID == UsersID);
            var userOrder = classes.connect.modelbd.Orders.FirstOrDefault(row => row.UserID == UsersID && row.ID == Item.ID);
            var Place = classes.connect.modelbd.Places.FirstOrDefault(row => row.ID == Item.Place);
            var MerchOrder = classes.connect.modelbd.Merches.FirstOrDefault(row => row.ID == Item.MerchID);
            var MerchOrderID = classes.connect.modelbd.OrderMerches.FirstOrDefault(row => row.OrderID == Item.ID);

        

            merchname = MerchOrder.name;
            newcost = (int)(userOrder.Cost - userOrder.Cost * userOrder.Sale / 100);
            newsale = userOrder.Sale;
            dateord = userOrder.DateOrder;
           
          
           // PlaceName = Place.Place1;
                        // LVItems.ItemsSource = classes.connect.modelbd.Orders.Where(row => row.UserID == UsersID).ToList();
          /*  timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Set the interval to 1 second
            timer.Tick += Timer_Tick; // Attach the event handler
            timer.Start(); // Start the timer*/
            PlaceName = Place.Place1;
            currentItem = Item;
             Userr = UsersID;
            // count = userOrder.Amount;
            newcost = newcost * count;
            MerchName.Text = $"Название товара: {merchname}";
            MerchCost.Text = $"Цена: {newcost}";
            MerchSale.Text = $"Скидка: {newsale}%";
            Date1.Text = $"Дата заказа: {dateord}";
            //CostText.Text = items.ToList().ToString();
            AmountText.Text = $"количество: {count}";
            PlaceText.Text = $"Пункт выдачи: {PlaceName}";

            
        }
       

        private void orderview_Click(object sender, RoutedEventArgs e)
        {

        }

        /*  private void MyListView_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
          {
              if (e.RightButton == MouseButtonState.Pressed)
              {
                  LVItems.ContextMenu.IsEnabled = true;
                  LVItems.ContextMenu.PlacementTarget = LVItems;
                  LVItems.ContextMenu.IsOpen = true;
              }
          }*/
     

   
        private void order_Click(object sender, RoutedEventArgs e)
        {
           

            currentItem.Cost = newcost;
            currentItem.Amount = count;
            
                
                
            
          
             
            
           

            /*  if (LVItems.SelectedItem != null)
              {

                  Models.Merch selectedMerch =  LVItems.SelectedItem as Merch ;



                  SoldatovaCRUDEntities1.getcontext().SaveChanges();
                  classes.manager.MainFrame.Navigate(new order(currentItem, Userr));
              }*/
            classes.manager.MainFrame.Navigate(new order(currentItem, Userr));
        }

        private void additem_Click(object sender, RoutedEventArgs e)
        {
            count += 1;

            var userOrder = classes.connect.modelbd.Orders.FirstOrDefault(row => row.UserID == Userr && row.ID == currentItem.ID);
            newcost = (int)(userOrder.Cost - userOrder.Cost * userOrder.Sale / 100) * count;
            AmountText.Text = $"количество: {count}";

            MerchCost.Text = $"Цена: {newcost}";
            amounttext.Text = $"количество: {count}";
        }

        private void delitem_Click(object sender, RoutedEventArgs e)
        {
            count -= 1;

            var userOrder = classes.connect.modelbd.Orders.FirstOrDefault(row => row.UserID == Userr && row.ID == currentItem.ID);
            newcost = (int)(userOrder.Cost - userOrder.Cost * userOrder.Sale / 100) * count;
            AmountText.Text = $"количество: {count}";
            amounttext.Text = $"количество: {count}";

            MerchCost.Text = $"Цена: {newcost}";
        }

        private void Places_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)Places.SelectedItem;
            if (selectedItem.Content.ToString() == "North")
            {
                currentItem.Place = 1;

                var place = classes.connect.modelbd.Places.FirstOrDefault(row => row.ID == currentItem.Place);
                datearr = dateord.AddDays(6);
                currentItem.DateArrive = datearr;
                Date2.Text = $"Дата приезда: {datearr}";
                PlaceName = place.Place1;
                PlaceText.Text = $"Пункт выдачи: {PlaceName}";

            }
            else if (selectedItem.Content.ToString() == "West")
            {
                currentItem.Place = 2;
                var place = classes.connect.modelbd.Places.FirstOrDefault(row => row.ID == currentItem.Place);
                datearr = dateord.AddDays(3);
                currentItem.DateArrive = datearr;
                Date2.Text = $"Дата приезда: {datearr}";
                PlaceName = place.Place1;
                PlaceText.Text = $"Пункт выдачи: {PlaceName}";
            }
            else if (selectedItem.Content.ToString() == "East")
            {
                currentItem.Place = 3;
                var place = classes.connect.modelbd.Places.FirstOrDefault(row => row.ID == currentItem.Place);
                datearr = dateord.AddDays(7);
                currentItem.DateArrive = datearr;
                Date2.Text = $"Дата приезда: {datearr}";
                PlaceName = place.Place1;
                PlaceText.Text = $"Пункт выдачи: {PlaceName}";
            }
            else if (selectedItem.Content.ToString() == "South")
            {
                currentItem.Place = 4;
               var place = classes.connect.modelbd.Places.FirstOrDefault(row => row.ID == currentItem.Place);
                datearr = dateord.AddDays(1);
                currentItem.DateArrive = datearr;
                Date2.Text = $"Дата приезда: {datearr}";
                PlaceName = place.Place1;
                PlaceText.Text = $"Пункт выдачи: {PlaceName}";
            }
        }
    }
}
