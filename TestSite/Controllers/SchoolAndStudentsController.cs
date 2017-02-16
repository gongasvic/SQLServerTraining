using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestSite.Models;

namespace TestSite.Controllers
{
    public class SchoolAndStudentsController : Controller
    {
        private MyTestDBEntities db = new MyTestDBEntities();

        // GET: SchoolAndStudents
        public ActionResult Index()
        {
            var schoolAndStudents = db.SchoolAndStudents.Include(s => s.School).Include(s => s.Student);
            return View(schoolAndStudents.ToList());
        }

        // GET: SchoolAndStudents/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolAndStudent schoolAndStudent = db.SchoolAndStudents.Find(id);
            if (schoolAndStudent == null)
            {
                return HttpNotFound();
            }
            return View(schoolAndStudent);
        }

        // GET: SchoolAndStudents/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(db.Schools, "SchoolID", "Name");
            ViewBag.StudntID = new SelectList(db.Students, "studentID", "Name");
            return View();
        }

        // POST: SchoolAndStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudntID,SchoolID,StudiesInID")] SchoolAndStudent schoolAndStudent)
        {
            if (ModelState.IsValid)
            {
                db.SchoolAndStudents.Add(schoolAndStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SchoolID = new SelectList(db.Schools, "SchoolID", "Name", schoolAndStudent.SchoolID);
            ViewBag.StudntID = new SelectList(db.Students, "studentID", "Name", schoolAndStudent.StudntID);
            return View(schoolAndStudent);
        }

        // GET: SchoolAndStudents/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolAndStudent schoolAndStudent = db.SchoolAndStudents.Find(id);
            if (schoolAndStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.SchoolID = new SelectList(db.Schools, "SchoolID", "Name", schoolAndStudent.SchoolID);
            ViewBag.StudntID = new SelectList(db.Students, "studentID", "Name", schoolAndStudent.StudntID);
            return View(schoolAndStudent);
        }

        // POST: SchoolAndStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudntID,SchoolID,StudiesInID")] SchoolAndStudent schoolAndStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolAndStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SchoolID = new SelectList(db.Schools, "SchoolID", "Name", schoolAndStudent.SchoolID);
            ViewBag.StudntID = new SelectList(db.Students, "studentID", "Name", schoolAndStudent.StudntID);
            return View(schoolAndStudent);
        }

        // GET: SchoolAndStudents/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolAndStudent schoolAndStudent = db.SchoolAndStudents.Find(id);
            if (schoolAndStudent == null)
            {
                return HttpNotFound();
            }
            return View(schoolAndStudent);
        }

        // POST: SchoolAndStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SchoolAndStudent schoolAndStudent = db.SchoolAndStudents.Find(id);
            db.SchoolAndStudents.Remove(schoolAndStudent);
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
    }
}
