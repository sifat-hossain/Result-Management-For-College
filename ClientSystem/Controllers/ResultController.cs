using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClientSystem.Models;

namespace ClientSystem.Controllers
{
    public class ResultController : Controller
    {
        private DBCMSEntities db = new DBCMSEntities();

        // GET: Result
        public ActionResult Index()
        {
            if (Session["name"] != null && Session["name"].ToString() == "admin")
            {
                var results = db.Results.Include(r => r.Department).Include(r => r.StudentProfile).Include(r => r.Subject);
                return View(results.ToList());
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult StudentResult(int? id)
        {
            if (Session["tid"] != null)
            {
                id = Convert.ToInt32(Session["tid"]);
                var subjectId = db.TeacherProfiles.Where(x => x.TeacherProfileId == id).FirstOrDefault().SubjectId;
                var result = db.Results.Where(x => x.Subject.SubjectId == subjectId).ToList();
                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // GET: Result/Create
        public ActionResult Create()
        {
            if (Session["tid"] != null)
            {
                int id = Convert.ToInt32(Session["tid"]);

                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
                ViewBag.StudentProfileId = new SelectList(db.StudentProfiles, "StudentProfileId", "StudentName");
                ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Result/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResultId,ExamType,StudentProfileId,DepartmentId,SubjectId,Marks,SubmitDate")] Result result)
        {
            if (ModelState.IsValid)
            {
                result.SubmitDate = DateTime.Now;

                bool value = db.Results.ToList().Exists(x => x.StudentProfileId == result.StudentProfileId && x.ExamType == result.ExamType && x.SubmitDate.Value.Year == result.SubmitDate.Value.Year && x.SubjectId==result.SubjectId);
                if (value != true)
                {
                    
                    db.Results.Add(result);
                    db.SaveChanges();
                    return RedirectToAction("StudentResult");
                }
                else
                {
                    return Content("<script language='javascript' type='text/javascript'>alert('You Already Submit this Result'); window.location='/Result/Create/'</script>");

                }
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", result.DepartmentId);
            ViewBag.StudentProfileId = new SelectList(db.StudentProfiles, "StudentProfileId", "StudentName", result.StudentProfileId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", result.SubjectId);
            return View(result);
        }

        // GET: Result/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["tid"] != null)
            {


                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Result result = db.Results.Find(id);
                if (result == null)
                {
                    return HttpNotFound();
                }
                ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", result.DepartmentId);
                ViewBag.StudentProfileId = new SelectList(db.StudentProfiles, "StudentProfileId", "StudentName", result.StudentProfileId);
                ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", result.SubjectId);
                return View(result);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        // POST: Result/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultId,ExamType,StudentProfileId,DepartmentId,SubjectId,Marks,SubmitDate")] Result result)
        {
            if (ModelState.IsValid)
            {
                result.SubmitDate = DateTime.Now;
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("StudentResult");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", result.DepartmentId);
            ViewBag.StudentProfileId = new SelectList(db.StudentProfiles, "StudentProfileId", "StudentName", result.StudentProfileId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", result.SubjectId);
            return View(result);
        }

        // GET: Result/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Result result = db.Results.Find(id);
            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Result result = db.Results.Find(id);
            db.Results.Remove(result);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult GetSubjectByDepartment(int? id)
        {
            return Json(new SelectList(db.Subjects.Where(x => x.DepartmentId == id), "SubjectId", "SubjectName"), JsonRequestBehavior.AllowGet);
        }
    }
}
