using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EntityFrameworkTest1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddOrderClick(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).AddOrder();
        }

        private void PayForOrderClick(object sender, RoutedEventArgs e)
        {
            ((ViewModel.ViewModel)DataContext).PayOrder();
        }

        private void HtmlExportClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    ((ViewModel.ViewModel)DataContext).ExportLastMonthOrders(saveFileDialog.FileName);
                } catch (Exception ex)
                {
                    MessageBox.Show("Error while exporting data: "+ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
