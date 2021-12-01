using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookImagesDto
    {
        public int Id { get; set; }
        public string BookImageUrl { get; set; }
        public int BookId { get; set; }
    }
}
