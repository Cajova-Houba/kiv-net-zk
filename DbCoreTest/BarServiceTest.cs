using System;
using System.Collections.Generic;
using System.Linq;
using DbCore;
using DbCore.Model;
using DbCore.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DbCoreTest
{
    [TestClass]
    public class BarServiceTest
    {
        private BarContext barContext;
        private BarService barService;

        [TestInitialize]
        public void SetUp()
        {
            barContext = new BarContext();
            InitData(barContext);


            barService = new BarService(barContext);
        }

        private void InitData(BarContext bc)
        {
            bc.Tables.Add(new Table() { Name = "Table 1" });
            bc.Tables.Add(new Table() { Name = "Table 2" });
            bc.Tables.Add(new Table() { Name = "Table 3" });
            bc.Tables.Add(new Table() { Name = "Table 4" });

            Ingredient rum = bc.Ingredients.Add(new Ingredient() { Name = "Rum" });
            Ingredient coke = bc.Ingredients.Add(new Ingredient() { Name = "Coke" });
            Ingredient beer = bc.Ingredients.Add(new Ingredient() { Name = "Beer" });
            Ingredient wine = bc.Ingredients.Add(new Ingredient() { Name = "Wine" });
            Ingredient soda = bc.Ingredients.Add(new Ingredient() { Name = "Soda" });

            bc.Drinks.Add(new Drink() { Name = "Cuba Libre", RecommendedPrice = 20, Ingredients = new List<Ingredient>(new Ingredient[] { rum, coke }) });
            bc.Drinks.Add(new Drink() { Name = "Aperol Spritz", RecommendedPrice = 20, Ingredients = new List<Ingredient>(new Ingredient[] { wine, soda }) });
            bc.Drinks.Add(new Drink() { Name = "Beer", RecommendedPrice = 15, Ingredients = new List<Ingredient>(new Ingredient[] { beer }) });
            bc.Drinks.Add(new Drink() { Name = "Wine", RecommendedPrice = 10, Ingredients = new List<Ingredient>(new Ingredient[] { wine }) });
            bc.Drinks.Add(new Drink() { Name = "Soda", RecommendedPrice = 5, Ingredients = new List<Ingredient>(new Ingredient[] { soda }) });

            bc.SaveChanges();
        }

        [TestMethod]
        public void TestGetAllTables()
        {
            List<Table> tables = barService.GetAllTables();
            Assert.AreEqual(4, tables.Count);
        }

        [TestMethod]
        public void TestAddOrderToTable()
        {
            double price = 30;
            Table t = barContext.Tables.ToList().First();
            barService.AddOrderToTable(t, "Test order", price);
            t = barContext.Tables.Find(t.Id);
            Assert.AreEqual(1, t.PendingOrders.Count);
            Assert.AreEqual(price, t.TotalPrice);
        }
    }
}
