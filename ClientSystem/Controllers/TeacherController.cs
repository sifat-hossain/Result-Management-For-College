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
    public class TeacherController : Controller
    {

        private DBCMSEntities db = new DBCMSEntities();

        public ActionResult Index()
        {
            return View(db.TeacherProfiles.ToList());
        }

        public ActionResult TeacherProfile(int? id)
        {
            if (Session["tid"] != null)
            {
                id = Convert.ToInt32(Session["tid"]);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TeacherProfile teacherProfile = db.TeacherProfiles.Find(id);
                if (teacherProfile == null)
                {
                    return HttpNotFound();
                }
                return View(teacherProfile);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Create()
        {
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherProfileId,TeacherName,Gender,Degree,DesignationId,TeacherEmail,TeacherPassword,TeacherPhone,TeacherAddress,JoiningDate,DepartmentId,SubjectId")] TeacherProfile teacherProfile)
        {
            if (ModelState.IsValid)
            {
                teacherProfile.JoiningDate = DateTime.Now;
                db.TeacherProfiles.Add(teacherProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacherProfile.DesignationId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", teacherProfile.DepartmentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", teacherProfile.SubjectId);

            return View(teacherProfile);

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherProfile teacherProfile = db.TeacherProfiles.Find(id);
            if (teacherProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacherProfile.DesignationId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", teacherProfile.DepartmentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", teacherProfile.SubjectId);

            return View(teacherProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherProfileId,TeacherName,Gender,Degree,DesignationId,TeacherEmail,TeacherPassword,TeacherPhone,TeacherAddress,JoiningDate,DepartmentId,SubjectId")] TeacherProfile teacherProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DesignationId = new SelectList(db.Designations, "DesignationId", "DesignationName", teacherProfile.DesignationId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", teacherProfile.DepartmentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", teacherProfile.SubjectId);

            return View(teacherProfile);
        }

        public ActionResult Details(int? id)
        {
            if (Session["name"] != null && Session["name"].ToString() == "admin")
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TeacherProfile teacherProfile = db.TeacherProfiles.Find(id);
                if (teacherProfile == null)
                {
                    return HttpNotFound();
                }
                return View(teacherProfile);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
    }
}