using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Model
{
    public class Order : AbstractEntity, IExportable
    {
        private readonly string[] FIELD_NAMES = new string[]
        {
            "Id", "Name", "Real Price", "Date paid"
        };

        public string Name { get; set; }

        public double RealPrice { get; set; }

        public DateTime DatePaid { get; set; }

        public string[] GetFieldNames()
        {
            return FIELD_NAMES;
        }

        public Dictionary<string, object> GetFieldValues()
        {
            return new Dictionary<string, object>()
            {
                { FIELD_NAMES[0], Id },
                { FIELD_NAMES[1], Name},
                { FIELD_NAMES[2], RealPrice },
                { FIELD_NAMES[3], DatePaid }
            };
        }

        public override string ToString()
        {
            return $"{Name}: {RealPrice:0.##} {DatePaid}";
        }
    }
}
