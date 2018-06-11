using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Model
{
    public class Drink : AbstractEntity
    {
        public string Name { get; set; }

        public double RecommendedPrice { get; set; }

        public virtual List<Ingredient> Ingredients { get; set; }

        public override string ToString()
        {
            string ingredients = String.Join(", ", Ingredients.Select(i => i.Name));
            return $"{Name} ({ingredients}";
        }
    }
}
