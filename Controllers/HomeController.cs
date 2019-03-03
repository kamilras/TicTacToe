using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.Session["Id"] != null)
            {
                UserContext db = new UserContext();
                var Id = int.Parse(HttpContext.Session["Id"].ToString());
                var user = db.Users.Where(u => u.Id == Id).FirstOrDefault();
                return View(user);
            }
            else
            {
                return View();
            }

        }

        public ActionResult Game(User user)
        {
            UserContext db = new UserContext();
            if (HttpContext.Session["Id"] != null)
            {
                var Id = int.Parse(HttpContext.Session["Id"].ToString());
                user = db.Users.Where(u => u.Id == Id).FirstOrDefault();
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpPost]
        public ActionResult Ajax(string data)
        {
            UserContext db = new UserContext();
            User user = new User();
            var Id = int.Parse(HttpContext.Session["Id"].ToString());
            user = db.Users.Where(u => u.Id == Id).FirstOrDefault();
            switch (data)
            {
                case "user":
                    Session["user"] = int.Parse(Session["user"].ToString()) + 1;
                    user.Score++;
                    db.SaveChanges();
                    break;
                case "computer":
                    Session["computer"] = int.Parse(Session["computer"].ToString()) + 1;
                    break;
            }
            Json(data, JsonRequestBehavior.AllowGet);
            return RedirectToAction("Game", "Account", user);
        }

        public ActionResult Contact()
        {
            ViewBag.Message ="Strona kontaktowa";
            return View();
        }
    }
}