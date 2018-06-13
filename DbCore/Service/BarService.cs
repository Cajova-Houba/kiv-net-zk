using DbCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Service
{
    public class BarService
    {
        private BarContext barContext;

        public BarService(BarContext barContext)
        {
            this.barContext = barContext;
        }

        public BarService() : this (new BarContext())
        {

        }

        public List<Table> GetAllTables()
        {
            return barContext.Tables.ToList();
        }

        public List<Drink> GetDrinkMenu()
        {
            return barContext.Drinks.ToList();
        }

        public Table GetTableById(int id)
        {
            return barContext.Tables.Find(id);
        }

        public void AddOrderToTable(Table table, string name, double price)
        {
            PendingOrder po = new PendingOrder() { Table = table, Name = name, RealPrice = price };
            barContext.PendingOrders.Add(po);
            barContext.SaveChanges();
        }

        public void PayForOrder(PendingOrder po)
        {
            Order o = new Order() { Name = po.Name, RealPrice = po.RealPrice, DatePaid = DateTime.Now };
            barContext.Orders.Add(o);
            barContext.PendingOrders.Remove(po);
            barContext.SaveChanges();
        }

        public List<Order> OrdersInCurrentMonth()
        {
            DateTime now = DateTime.Now;
            DateTime dateFrom = new DateTime(now.Year, now.Month, 1);
            DateTime dateTo = dateFrom.AddMonths(1).AddDays(-1);

            return OrdersInDateRange(dateFrom, dateTo);
        }

        public List<Order> OrdersInDateRange(DateTime dateFrom, DateTime dateTo)
        {
            var query = from ordr in barContext.Orders
                        where
                            ordr.DatePaid >= dateFrom
                            && ordr.DatePaid <= dateTo
                        select ordr;

            return query.ToList();
        }
    }
}
