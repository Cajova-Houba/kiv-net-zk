using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Model
{
    public abstract class AbstractEntity : IEntity
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            var entity = obj as AbstractEntity;
            return entity != null &&
                   Id == entity.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }
    }
}
