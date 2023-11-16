using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//using System.Activities.Statements;

namespace CRUD_socket
{
    internal class CustomDataContext
    {
        private static CustomDataContext instance;

        public static CustomDataContext Instance
        {
            get
            {
                if (instance == null)
                    instance = new CustomDataContext();
                return instance;
            }
        }

        private List<Product> products = new List<Product>();
        public List<Product> Products
        {
            get => products;
        }

        private List<Category> categories = new List<Category>();
        public List<Category> Categories 
        {
            get => categories;
        }

        private List<Manufacturer> manufacturers = new List<Manufacturer>();
        public List<Manufacturer> Manufacturers 
        {
            get => manufacturers;
        }

        private List<Basket> baskets = new List<Basket>();
        public List<Basket> Baskets
        {
            get => baskets;
        }

        private Socket client;

        private CustomDataContext()
        {
            LoadManufacturer();
            LoadCategories();
            LoadProducts();
            CreateConnection();
        }

        private void LoadManufacturer()
        {
            using(StreamReader sr = new StreamReader("manufacturers.json", true))
            {
                manufacturers = JsonSerializer.Deserialize<List<Manufacturer>>(sr.ReadToEnd());
            }
        }

        private void LoadCategories()
        {
            using (StreamReader sr = new StreamReader("categories.json", true))
            {
                categories = JsonSerializer.Deserialize<List<Category>>(sr.ReadToEnd());
            }
        }

        private void LoadProducts()
        {
            using (StreamReader sr = new StreamReader("products.json", true))
            {
                products = JsonSerializer.Deserialize<List<Product>>(sr.ReadToEnd());
            }
        }


        public void SaveChanges()
        {
            SaveManufacturers();
            SaveCategories();
            SaveProducts();
        }

        private void SaveManufacturers()
        {
            using (StreamWriter sw = new StreamWriter("manufacturers.json"))
            {
                sw.Write(JsonSerializer.Serialize(manufacturers));
            }
        }

        private void SaveCategories()
        {
            using (StreamWriter sw = new StreamWriter("categories.json"))
            {
                sw.Write(JsonSerializer.Serialize(categories));
            }
        }

        private void SaveProducts()
        {
            using (StreamWriter sw = new StreamWriter("products.json"))
            {
                sw.Write(JsonSerializer.Serialize(products));
            }
        }


        public int GetManufacturerMaxId() => manufacturers.Count > 0 ? manufacturers.MaxBy(m => m.Id).Id : 0;
        public int GetCategoryMaxId() => categories.Count > 0 ? categories.MaxBy(c => c.Id).Id : 0;
        public int GetProductMaxId() => products.Count > 0 ? products.MaxBy(p => p.Id).Id : 0;

        public void AddManufacturer(Manufacturer manufacturer)
        {
            manufacturer.Id = GetManufacturerMaxId() + 1;
            if (manufacturers.SingleOrDefault(m => m.Id == manufacturer.Id) != null)
            {
                manufacturers.Add(manufacturer);
            }
            else
            {
                throw new Exception("Объект с таким id уже существует!");
            }
        }

        public void AddCategory(Category category)
        {
            category.Id = GetCategoryMaxId() + 1;
            if (categories.SingleOrDefault(c => c.Id == category.Id) != null)
            {
                categories.Add(category);
            }
            else
            {
                throw new Exception("Объект с таким id уже существует!");
            }
        }

        public void AddProduct(Product product)
        {
            product.Id = GetProductMaxId() + 1;
            if (products.SingleOrDefault(p => p.Id == product.Id) != null)
            {
                products.Add(product);
            }
            else
            {
                throw new Exception("Объект с таким id уже существует!");
            }
        }


        public void RemoveManufacturer(Manufacturer manufacturer) => manufacturers.Remove(manufacturer);
        public void RemoveCategory(Category category) => categories.Remove(category);
        public void RemoveProduct(Product product) => products.Remove(product);


        public void AddProductToBusket(Product product, int count)
        {
            Basket item;
            if ((item = baskets.SingleOrDefault(b => b.Products.Equals(product))) != null)
            {
                item.Count = count;
            }
            else
            {
                item = new Basket();
                item.Products = product;
                item.Count = count;
                baskets.Add(item);
            }
        }

        public async void CreateConnection()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);   // связываем с локальной точкой ipPoint
            socket.Listen(5);
            // получаем входящее подключение
            this.client = await socket.AcceptAsync();
            // получаем адрес клиента
        }

        public void SendDataToPython()
        {
            string json = JsonSerializer.Serialize(baskets);

            this.client.Send(Encoding.UTF8.GetBytes(json));
        }
    }
}
