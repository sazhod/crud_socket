using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //SendData();
            //CreateTestData();
            LoadItems();
            //CustomDataContext.Instance.Manufacturers
        }

        public void LoadItems()
        {
            listView.Items.Clear();
            foreach (var item in CustomDataContext.Instance.Products)
            {
                listView.Items.Add(new MyUserControl(item, this));
            }
        }
        

        public void SendData()
        {
            //Category category = new Category("test");
            //Manufacturer manufacturer = new Manufacturer(1, "man");
            //Product product1 = new Product(1, "product1", category, manufacturer, "desc", 10, 100);
            //Product product2 = new Product(2, "product2", category, manufacturer, "desc", 10, 100);
            //Product product3 = new Product(3, "product3", category, manufacturer, "desc", 10, 100);

            //List<Product> products = new List<Product>();
            //products.Add(product1);
            //products.Add(product2);
            //products.Add(product3);

            //IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            //using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.Bind(ipPoint);   // связываем с локальной точкой ipPoint
            //socket.Listen();
            //Console.WriteLine("Сервер запущен. Ожидание подключений...");
            //// получаем входящее подключение
            //using Socket client = socket.Accept();
            //// получаем адрес клиента
            //Console.WriteLine($"Адрес подключенного клиента: {client.RemoteEndPoint}");
            ////client.Send()

            //string json = JsonSerializer.Serialize(products);

            //client.Send(Encoding.UTF8.GetBytes(json));
        }

        private void CreateTestData()
        {
            Category category1 = new Category("category1");
            CustomDataContext.Instance.Categories.Add(category1);

            Category category2 = new Category("category2");
            CustomDataContext.Instance.Categories.Add(category2);

            Manufacturer manufacturer1 = new Manufacturer("man");
            CustomDataContext.Instance.Manufacturers.Add(manufacturer1);
            Product product1 = new Product("product1", category1, manufacturer1, "desc", 10, 100);
            CustomDataContext.Instance.Products.Add(product1);
            Product product2 = new Product("product2", category1, manufacturer1, "desc", 10, 100);
            CustomDataContext.Instance.Products.Add(product2);
            Product product3 = new Product("product3", category2, manufacturer1, "desc", 10, 100);
            CustomDataContext.Instance.Products.Add(product3);

            CustomDataContext.Instance.SaveChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomDataContext.Instance.SendDataToPython();
        }
    }
}
