using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BookOrderDetailsDto
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string StripeSessionId { get; set; }
        [Required]
        public double TotalCost { get; set; }
        [Required]
        public int BookId { get; set; }
        public bool IsPaid { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public BookDto BookDto { get; set; }
        public string Status { get; set; }
    }
}
