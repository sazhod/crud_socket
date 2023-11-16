using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_socket
{
    public class Basket
    {
        public int Id {  get; set; }
        public Product Products { get; set; }
        public int Count { get; set; }
    }
}
