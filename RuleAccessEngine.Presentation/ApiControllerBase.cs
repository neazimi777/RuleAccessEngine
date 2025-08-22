using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleAccessEngine.Presentation
{
    [Route("api/v1/[controller]")]
    [ApiController, Produces("application/json")]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}
