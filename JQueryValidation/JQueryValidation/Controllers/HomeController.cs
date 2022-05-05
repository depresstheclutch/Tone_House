using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JQueryValidation.Models;

namespace JQueryValidation.Controllers
{
    public class HomeController : Controller
    {
        db_TheToneHouseEntities db = new db_TheToneHouseEntities();
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken] 
        public ActionResult Register(tb_SignUp tb)
        {

            return View();
        }
       
    }
}