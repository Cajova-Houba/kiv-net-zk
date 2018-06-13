using DbCore;
using DbCore.Model;
using DbCore.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTest1.ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private BarContext barContext;
        private BarService barService;
      

        public ObservableCollection<Table> Tables { get; set; }
        
        public Table AddOrderSelectedTable { get; set; }

        public Drink AddOrderSelectedDrink { get; set; }

        public ObservableCollection<Drink> Drinks { get; set; }

        private Table selectedTableDetail;
        public Table SelectedTableDetail
        {
            get { return selectedTableDetail; }
            set
            {
                selectedTableDetail = value;
                OnPropertyChanged("SelectedTableDetail");
            }
        }

        private PendingOrder selectedPendingOrder;
        public PendingOrder SelectedPendingOrder
        {
            get { return selectedPendingOrder; }
            set
            {
                selectedPendingOrder = value;
                OnPropertyChanged("SelectedPendingOrder");
                OnPropertyChanged("IsPayForOrderButtonEnabled");
            }
        }

        public bool IsPayForOrderButtonEnabled
        {
            get
            {
                return selectedPendingOrder != null;
            }
        }

        private ObservableCollection<Order> lastMonthOrders;
        public ObservableCollection<Order> LastMonthOrders
        {
            get
            {
                return new ObservableCollection<Order>(lastMonthOrders);
            }

            set
            {
                lastMonthOrders = value;
                OnPropertyChanged("LastMonthOrders");
            }
        }

        public ViewModel()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                Tables = new ObservableCollection<Table>
                {
                    new Table() { Id = 1, Name = "Table 1" },
                    new Table() { Id = 2, Name = "Table 2" }
                };
                AddOrderSelectedTable = Tables[0];

                Drinks = new ObservableCollection<Drink>
                {
                    new Drink() { Id = 1, Name = "Cuba Libre", RecommendedPrice = 20 },
                    new Drink() { Id = 2, Name = "Beer", RecommendedPrice = 10 },
                    new Drink() { Id = 3, Name = "Wine", RecommendedPrice = 10 }
                };
                AddOrderSelectedDrink = new Drink() { Id = 1, Name = "Cuba Libre", RecommendedPrice = 20 };
                LastMonthOrders = new ObservableCollection<Order>();
            }
            else
            {
                Database.SetInitializer(new ContextInit());
                this.barContext = new BarContext();
                this.barService = new BarService(this.barContext);
                Refresh();
            }

        }

        private void Refresh()
        {
            AddOrderSelectedTable = null;
            AddOrderSelectedDrink = null;
            Tables = new ObservableCollection<Table>(barService.GetAllTables());
            if (Tables.Count > 0)
            {
                AddOrderSelectedTable = Tables[0];
            }
            Drinks = new ObservableCollection<Drink>(barService.GetDrinkMenu());
            if (Drinks.Count > 0)
            {
                AddOrderSelectedDrink = Drinks[0];
            }
            LastMonthOrders = new ObservableCollection<Order>(barService.OrdersInCurrentMonth());
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public void AddOrder()
        {
            barService.AddOrderToTable(AddOrderSelectedTable, AddOrderSelectedDrink.Name, AddOrderSelectedDrink.RecommendedPrice);
            Refresh();
        }

        public void PayOrder()
        {
            barService.PayForOrder(SelectedPendingOrder);
            Refresh();
            SelectedPendingOrder = null;
        }

        public void ExportLastMonthOrders(String fileName)
        {
            LastMonthOrders = new ObservableCollection<Order>(barService.OrdersInCurrentMonth());
            string htmlExport = HtmlExporter.ExportToHtml(new Dictionary<string, ICollection<IExportable>>()
            {
                { "Orders in last month", new List<IExportable>(LastMonthOrders) }
            },
            "Orders in last month");
            File.WriteAllText(fileName, htmlExport);
        }
    }
}
