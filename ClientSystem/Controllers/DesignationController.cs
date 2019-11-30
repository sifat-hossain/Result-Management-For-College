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
    public class DesignationController : Controller
    {
        private DBCMSEntities db = new DBCMSEntities();

        public ActionResult Index()
        {
            if (Session["name"] != null && Session["name"].ToString() == "admin")
            {
                return View(db.Designations.ToList());
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
                Designation designation = db.Designations.Find(id);
                if (designation == null)
                {
                    return HttpNotFound();
                }
                return View(designation);
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
        public ActionResult Create([Bind(Include = "DesignationId,DesignationName")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                db.Designations.Add(designation);
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
                Designation designation = db.Designations.Find(id);
                if (designation == null)
                {
                    return HttpNotFound();
                }
                return View(designation);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DesignationId,DesignationName")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(designation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public JsonResult CheckDesignation(string designation)
        {
            bool value = db.Designations.ToList().Exists(x => x.DesignationName.ToLower().Equals(designation.ToLower()));

            return Json(value);
        }
    }
}