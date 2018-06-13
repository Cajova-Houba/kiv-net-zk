using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Model
{
    public interface IExportable
    {
        string[] GetFieldNames();

        Dictionary<String, Object> GetFieldValues();
    }
}
