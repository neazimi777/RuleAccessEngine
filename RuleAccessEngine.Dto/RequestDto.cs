using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleAccessEngine.Dto
{
    public class RequestDto
    {
        public string IP { get; set; } = string.Empty;
        public DateTime RequestTime { get; set; } = DateTime.UtcNow;
    }
}
