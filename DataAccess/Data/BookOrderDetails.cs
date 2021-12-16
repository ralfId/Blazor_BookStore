using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class BookOrderDetails
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
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        public string Status { get; set; }
    }
}
