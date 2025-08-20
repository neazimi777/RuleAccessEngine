using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleAccessEngine.Dto
{
    public class AccessRequestDto
    {
        public UserDto User { get; set; } = new();
        public RequestDto Request { get; set; } = new();
    }
}
