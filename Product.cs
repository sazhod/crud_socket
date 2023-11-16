using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_socket
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Manufacturer Manufacturer{ get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }

        public Product(string name, Category category, Manufacturer manufacturer, 
            string description, int count, int price)
        {
            Name = name;
            Category = category;
            Manufacturer = manufacturer;
            Description = description;
            Count = count;
            Price = price;
        }
    }
}
