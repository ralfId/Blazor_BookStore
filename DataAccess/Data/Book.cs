using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Regular_price { get; set; }
        [Required]
        public string Author { get; set; }
        public string Details { get; set; }
        public DateTime Creation_date { get; set; } = DateTime.Now;
        public DateTime Update_date { get; set; }
    }
}
