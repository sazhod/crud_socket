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

namespace CRUD_socket
{
    /// <summary>
    /// Логика взаимодействия для MyUserControl.xaml
    /// </summary>
    public partial class MyUserControl : UserControl
    {
        private Product product;
        private MainWindow mainWindow;

        public MyUserControl(Product product, MainWindow mainWindow)
        {
            InitializeComponent();
            //this.product = product;
            DataContext = product;
            this.mainWindow = mainWindow;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить продукт ?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {   
                CustomDataContext.Instance.RemoveProduct((Product)DataContext);
                CustomDataContext.Instance.SaveChanges();
                mainWindow.LoadItems();
            }
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            CustomDataContext.Instance.AddProductToBusket((Product)DataContext, 1);
        }
    }
}
