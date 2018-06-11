using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Model
{
    public class Ingredient : AbstractEntity
    {
        public String Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
