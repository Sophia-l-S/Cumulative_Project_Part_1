using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;

namespace BlogProject.Controllers
{
    public class AuthorController : Controller
    {
      public ActionResult Index()
        {
            return View();
        }

        //GET
        public ActionResult List()
        {
            AuthorDataController Controller = new AuthorDataController();
            IEnumerable<Author> Authors = Controller.ListAuthors();

            return View(Authors);
        }
    }
}
