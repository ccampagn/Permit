using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Permit.Models;
using Permit.DataBase;

namespace Permit.Controllers
{
    public class ApplicationController : Controller
    {

        [HttpGet]
        public IActionResult Index(int id)
        {
            Db db = new Db();
            Application a = db.getapplicationsbyid(id);

            return View(a);
        }
        public IActionResult Apply(int id)
        {
            Db db = new Db();
            List<Park> park = null;// db.getparks();

            return View(park);
        }
    }
}
