using DbCore.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore
{
    public class ContextInit : DropCreateDatabaseIfModelChanges<BarContext>
    {
        protected override void Seed(BarContext context)
        {
            context.Tables.AddRange(new List<Table>()
            {
                new Table() { Name = "Table 1" },
                new Table() { Name = "Table 2" },
                new Table() { Name = "Table 3" },
                new Table() { Name = "Table 4" }
            });
            context.SaveChanges();

            Ingredient rum = context.Ingredients.Add(new Ingredient() { Name = "Rum" });
            context.SaveChanges();
            Ingredient coke = context.Ingredients.Add(new Ingredient() { Name = "Coke" });
            //asdasd
            context.SaveChanges();
            Ingredient beer = context.Ingredients.Add(new Ingredient() { Name = "Beer" });
            context.SaveChanges();
            Ingredient wine = context.Ingredients.Add(new Ingredient() { Name = "Wine" });
            context.SaveChanges();
            Ingredient soda = context.Ingredients.Add(new Ingredient() { Name = "Soda" });
            context.SaveChanges();

            context.Drinks.AddRange(new List<Drink>()
            {
                new Drink() { Name = "Cuba Libre", RecommendedPrice = 20, Ingredients = new List<Ingredient>(new Ingredient[] { rum, coke }) },
                new Drink() { Name = "Aperol Spritz", RecommendedPrice = 20, Ingredients = new List<Ingredient>(new Ingredient[] { wine, soda })},
                new Drink() { Name = "Beer", RecommendedPrice = 15, Ingredients = new List<Ingredient>(new Ingredient[] { beer }) },
                new Drink() { Name = "Wine", RecommendedPrice = 10, Ingredients = new List<Ingredient>(new Ingredient[] { wine }) },
                new Drink() { Name = "Soda", RecommendedPrice = 5, Ingredients = new List<Ingredient>(new Ingredient[] { soda }) }
            });
            context.SaveChanges();
        }
    }

    public class BarContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Order> Orders { get; set; }
        
        public DbSet<PendingOrder> PendingOrders { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public BarContext()
        {
        }
    }
}
