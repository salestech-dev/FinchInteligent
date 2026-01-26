using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace finchInteligent.Controllers
{
    [Route("[controller]")]
    public class BaseController : Controller
    {
        protected string usuarioId =>
             User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}