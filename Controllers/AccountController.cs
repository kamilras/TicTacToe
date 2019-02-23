using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class AccountController : Controller
    {
        // GET: 
        private UserContext db = new UserContext();

        public ActionResult Index()
        {
            if (HttpContext.Session["Id"] != null)
            {
                IEnumerable<User> gracze = db.Users.ToList();
                return View(gracze);
            }

            return RedirectToAction("Login");

        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Some Error Occured.");
            }

            return View(user);
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {

            if (ModelState.IsValid)
            {
                using (UserContext db = new UserContext())
                {
                    var usr = db.Users.Where(u => u.Name == user.Name && u.Password == user.Password).FirstOrDefault();
                    if (usr != null)
                    {
                        HttpContext.Session["Id"] = usr.Id;
                        HttpContext.Session["user"] = 0;
                        HttpContext.Session["computer"] = 0;

                    }
                    return RedirectToAction("Profil");
                }
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            HttpContext.Session["Id"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Profil()
        {
            if (HttpContext.Session["Id"] != null)
            {
                var Id = int.Parse(HttpContext.Session["Id"].ToString());
                var user = db.Users.Where(u => u.Id == Id).FirstOrDefault();
                return View(user);
            }
            return RedirectToAction("Login");
        }
    }
}