using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class BookImages
    {
        public int Id { get; set; }
        public string BookImageUrl { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
    }
}
