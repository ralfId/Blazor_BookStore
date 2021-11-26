using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaProducto_Server.Data
{
    public class BookBlazor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Is_InStock { get; set; }

        public List<BookType> BookType { get; set; }
    }
}
