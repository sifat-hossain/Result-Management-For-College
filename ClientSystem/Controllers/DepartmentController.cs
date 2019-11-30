using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClientSystem.Models;

namespace ClientSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private DBCMSEntities db = new DBCMSEntities();

        public ActionResult Index()
        {
            if (Session["name"] != null && Session["name"].ToString() == "admin")
            {
                return View(db.Departments.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Details(int? id)
        {
            if (Session["name"] != null && Session["name"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Department department = db.Departments.Find(id);
                if (department == null)
                {
                    return HttpNotFound();
                }
                return View(department);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Create()
        {
            if (Session["name"] != null && Session["name"].ToString() == "admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentId,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (Session["name"] != null && Session["name"].ToString() == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Department department = db.Departments.Find(id);
                if (department == null)
                {
                    return HttpNotFound();
                }
                return View(department);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentId,DepartmentName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public JsonResult CheckDepartment(string department)
        {
            bool value = db.Departments.ToList().Exists(x => x.DepartmentName.ToLower().Equals(department.ToLower()));

            return Json(value);
        }
    }
}