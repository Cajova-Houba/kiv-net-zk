using DbCore;
using DbCore.Model;
using DbCore.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTest1.ViewModel
{
    public class ViewModel
    {
        private BarContext barContext;
        private BarService barService;
      

        public ObservableCollection<Table> Tables { get; set; }
        

        public ViewModel()
        {
            if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                Tables = new ObservableCollection<Table>();
                Tables.Add(new Table() { Name = "Table 1" });
                Tables.Add(new Table() { Name = "Table 2" });
            }
            else
            {
                Database.SetInitializer(new ContextInit());
                this.barContext = new BarContext();
                this.barService = new BarService(this.barContext);
                Refresh();
            }

        }

        private void Refresh()
        {
            Tables = new ObservableCollection<Table>(barService.GetAllTables());
        }
    }
}
