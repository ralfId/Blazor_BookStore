using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api
{
    public class AuthRequestDto
    {
        [Required(ErrorMessage = "{0} is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid {0}")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public string Password { get; set; }
    }
}
