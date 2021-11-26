using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Type a name for the book")]
        public string Name { get; set; }
        [Range(5,100)]
        public double Regular_price { get; set; }
        [Required(ErrorMessage = "Type a name for author´s book")]
        public string Author { get; set; }
        public string Details { get; set; }
    }
}
