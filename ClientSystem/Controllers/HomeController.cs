using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ClientSystem.Models;

namespace ClientSystem.Controllers
{
    public class HomeController : Controller
    {
        private DBCMSEntities db = new DBCMSEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            bool student = db.StudentProfiles.ToList().Exists(x => x.StudentEmail == email && x.Password == password);
            bool teacher = db.TeacherProfiles.ToList().Exists(x => x.TeacherEmail == email && x.TeacherPassword == password);
            if (email=="admin"&& password=="12345")
            {
                Session["name"] = "admin";
                return RedirectToAction("Index", "Student");
            }
            if(student==true || teacher==true)
            {
                if(student==true)
                {
                    var StudentId = db.StudentProfiles.Where(x => x.StudentEmail == email && x.Password == password).FirstOrDefault().StudentProfileId;
                    var Studentname = db.StudentProfiles.Where(x => x.StudentEmail == email && x.Password == password).FirstOrDefault().StudentName;
                    Session["sid"] = StudentId;
                    Session["sname"] = Studentname;
                    return RedirectToAction("StudentProfile", "Student");
                }
                else
                {
                    var teacherid = db.TeacherProfiles.Where(x => x.TeacherEmail == email && x.TeacherPassword == password).FirstOrDefault().TeacherProfileId;
                    var teachername = db.TeacherProfiles.Where(x => x.TeacherEmail == email && x.TeacherPassword == password).FirstOrDefault().TeacherName;
                    Session["tid"] = teacherid;
                    Session["tname"] = teachername;
                    return RedirectToAction("TeacherProfile", "Teacher");
                }

            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Your Email or Password is incorrect'); window.location='/Home/Login/'</script>");

            }
           
        }
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            Session.Abandon();
            Response.Cookies.Clear();
            Session["id"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}