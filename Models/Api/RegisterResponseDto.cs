using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api
{
    public class RegisterResponseDto
    {
        public bool IsRegistered { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
