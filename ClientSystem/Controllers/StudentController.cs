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
    public class StudentController : Controller
    {
        private DBCMSEntities db = new DBCMSEntities();

        public ActionResult Index()
        {
            return View(db.StudentProfiles.ToList());
        }
        public ActionResult StudentProfile(int? id)
        {
            if (Session["sid"] != null)
            {
                id = Convert.ToInt32(Session["sid"]);
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StudentProfile studentProfile = db.StudentProfiles.Find(id);
                if (studentProfile == null)
                {
                    return HttpNotFound();
                }
                return View(studentProfile);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentProfileId,StudentName,Gender,StudentPhone,StudentEmail,Password,FatherName,FatherPhone,MotherName,MotherPhone,Address,DepartmentId,AdmissionDate")] StudentProfile studentProfile)
        {
            if (ModelState.IsValid)
            {
                studentProfile.AdmissionDate = DateTime.Now;
                db.StudentProfiles.Add(studentProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", studentProfile.DepartmentId);
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            if (studentProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", studentProfile.DepartmentId);
            return View(studentProfile);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentProfileId,StudentName,Gender,StudentPhone,StudentEmail,Password,FatherName,FatherPhone,MotherName,MotherPhone,Address,DepartmentId,AdmissionDate")] StudentProfile studentProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", studentProfile.DepartmentId);
            return View();
        }

        public ActionResult ResultById(int? id)
        {
            if (Session["sid"] != null)
            {
                id = Convert.ToInt32(Session["sid"]);
                ViewBag.student = db.StudentProfiles.Where(x => x.StudentProfileId == id).FirstOrDefault().StudentName;
                ViewBag.ExamType = new SelectList(db.Results, "ResultId", "ExamType");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult ResultById(Result result)
        {

            return RedirectToAction("GetResult", new { exam = result.ExamType });
        }

        public ActionResult GetResult(string exam)
        {
           int id = Convert.ToInt32(Session["sid"]);

            double i = 0, point = 0,ave=0, total = 0;

            ViewBag.ExamType = exam;
            ViewBag.Department = db.Results.Where(x => x.ExamType == exam && x.StudentProfileId == id).FirstOrDefault().Department.DepartmentName;
            ViewBag.StudentName = db.Results.Where(x => x.ExamType == exam && x.StudentProfileId == id).FirstOrDefault().StudentProfile.StudentName;

            var Result = db.Results.Where(x => x.ExamType == exam && x.StudentProfileId == id).ToList();
        
            foreach (var item in Result)
            {

                if (item.Marks >= 0 && item.Marks <= 39)
                {
                    point = 0;
                }
                if (item.Marks >= 40 && item.Marks <= 49)
                {
                    point = 2;
                }
                if (item.Marks >= 50 && item.Marks <= 59)
                {
                    point = 3;
                }
                if (item.Marks >= 60 && item.Marks <= 69)
                {
                    point = 3.5;
                }
                if (item.Marks >= 70 && item.Marks <= 79)
                {
                    point = 4;
                }
                if (item.Marks >= 80 && item.Marks <= 100)
                {
                    point = 5;
                }

                i++;
                total += point;
            }
            ave= total / i;
            ViewBag.TotalPoint = total;
            ViewBag.Average = ave;
            if(ave>=0 && ave < 2)
            {
                ViewBag.Grade = "F";
            }
            if (ave>= 2 && ave < 3)
            {
                ViewBag.Grade = "C";
            }
            if (ave >= 3 && ave < 3.5)
            {
                ViewBag.Grade = "B";
            }
            if (ave >= 3.5 && ave < 4)
            {
                ViewBag.Grade = "A-";
            }
            if (ave >= 4 && ave <5)
            {
                ViewBag.Grade = "A";
            }
            if (ave==5)
            {
                ViewBag.Grade = "A+";
            }
            return View(Result);

        }

        public ActionResult Details(int? id)
        {
            if(Session["name"]!=null && Session["name"].ToString()=="admin")
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                StudentProfile studentProfile = db.StudentProfiles.Find(id);
                if (studentProfile == null)
                {
                    return HttpNotFound();
                }
                return View(studentProfile);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
      
    }
}