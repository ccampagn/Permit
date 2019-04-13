using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Internal.System.Collections.Sequences;
using Microsoft.EntityFrameworkCore;
using Permit.DataBase;
using Permit.Models;

namespace Permit.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Db db = new Db();
            return View();
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
