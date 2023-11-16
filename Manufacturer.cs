using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_socket
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Manufacturer(string name)
        {
            this.Name = name;
        }
    }
}
