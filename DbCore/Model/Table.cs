using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Model
{
    public class Table : AbstractEntity
    {
        public string Name { get; set; }

        public virtual List<PendingOrder> PendingOrders { get; set; }

        [NotMappedAttribute]
        public double TotalPrice
        {
            get
            {
                return PendingOrders.Sum(po => po.RealPrice);
            }
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
