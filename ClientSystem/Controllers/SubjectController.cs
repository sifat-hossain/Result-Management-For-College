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
    public class SubjectController : Controller
    {
        private DBCMSEntities db = new DBCMSEntities();

        public ActionResult Index()
        {
            if (Session["name"] != null && Session["name"].ToString() == "admin")
            {
                return View(db.Subjects.ToList());
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
                Subject subject = db.Subjects.Find(id);
                if (subject == null)
                {
                    return HttpNotFound();
                }
                return View(subject);
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
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectId,SubjectName,DepartmentId")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", subject.DepartmentId);
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
                Subject subject = db.Subjects.Find(id);
                if (subject == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", subject.DepartmentId);
                return View(subject);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectId,SubjectName,DepartmentId")] Department subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", subject.DepartmentId);
            return View();
        }

        [HttpPost]
        public JsonResult CheckDepartment(int? department, string subject)
        {
            bool value = db.Subjects.ToList().Exists(x => x.DepartmentId == department && x.SubjectName.ToLower() == subject.ToLower());

            return Json(value);
        }
    }
}