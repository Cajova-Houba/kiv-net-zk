using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Model
{
    public class Order : AbstractEntity
    {

        public string Name { get; set; }

        public double RealPrice { get; set; }

        public DateTime DatePaid { get; set; }

        public override string ToString()
        {
            return $"{Name}: {RealPrice:0.##} {DatePaid}";
        }
    }
}
