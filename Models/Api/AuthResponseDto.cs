using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Api
{
    public class AuthResponseDto
    {
        public bool IsAuthenticated { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Token { get; set; }
        public UserDto UserInfo { get; set; }
    }
}
