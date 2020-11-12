using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SupervisorAllotment.Models;

namespace SupervisorAllotment.Controllers
{
    public class StudentController : Controller
    {
        private SupervisorAllotmentDBEntities db = new SupervisorAllotmentDBEntities();

        // GET: Student
        public ActionResult Index()
        {
            if (TempData.Count > 0 && TempData.ContainsKey("message"))
                TempData.Keep();
            ViewData["Teachers"] = db.Supervisors.ToList();
            return View(db.Students.ToList());
        }

        // GET: Student/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollegeId,Name,Email,DOB,GateRank,GateScore,BtechScore,Supervisor,Preference,ChoiceFilled,ChoiceAlloted")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (db.Students.Find(student.CollegeId) == null)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = student.CollegeId });
                }
            }
            ModelState.AddModelError(string.Empty, "Error Occured");
            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CollegeId,Name,Email,DOB,GateRank,GateScore,BtechScore,Supervisor,Preference,ChoiceFilled,ChoiceAlloted")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = student.CollegeId });
            }
            ModelState.AddModelError(string.Empty, "Error Occured");
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UpdatePreference(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewData["Teachers"] = db.Supervisors.ToList();
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePreference([Bind(Include = "CollegeId,Name,Email,DOB,GateRank,GateScore,BtechScore,Supervisor,Preference,ChoiceFilled,ChoiceAlloted")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(student.Preference) && !string.IsNullOrEmpty(student.Preference.Trim()) && student.Preference.Trim().Split(',').Length != 0)
                {
                    var preferences = student.Preference.Trim().Split(',');
                    var supervisors = db.Supervisors.ToList();
                    if (preferences.All(x => supervisors.Find(y => y.ID.ToString() == x.Trim()) != null))
                    {
                        student.ChoiceFilled = (byte)preferences.Length;
                        db.Entry(student).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Details", new { id = student.CollegeId });
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Input");
            ViewData["Teachers"] = db.Supervisors.ToList();
            return View(student);
        }
        public ActionResult SeePreference(string id, bool isAdmin)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewData["layout"] = "~/Views/Shared/_LayoutStudent.cshtml";
            if (isAdmin)
                ViewData["layout"] = "~/Views/Shared/_LayoutAdmin.cshtml";
            ViewData["Student"] = student;
            if (string.IsNullOrEmpty(student.Preference) || string.IsNullOrEmpty(student.Preference.Trim()))
                return View(Enumerable.Empty<Supervisor>());
            //Logic 
            var teachers = db.Supervisors.ToList();
            var preferences = Array.ConvertAll(student.Preference.Trim().Split(','), x => Convert.ToInt32(x.Trim()));
            var sup = new List<Supervisor>();
            foreach (var item in preferences)
            {
                sup.Add(teachers.Find(x => x.ID == item));
            }
            return View(sup);
        }

        public ActionResult AllotSupervisor()
        {
            var supervisor = db.Supervisors.ToList();
            var students = db.Students.ToList();
            students.ForEach(x => { x.ChoiceAlloted = null; x.Supervisor = null; });
            supervisor.ForEach(x => { x.StudentsAlloted = 0; db.Entry(x).State = EntityState.Modified; db.SaveChanges(); });
            students.Sort((a, b) => a.GateRank.CompareTo(b.GateRank));
            foreach (var student in students)
            {
                if (string.IsNullOrEmpty(student.Preference) || string.IsNullOrEmpty(student.Preference.Trim()) || student.Preference.Trim().Split(',').Length == 0)
                    continue;
                var preferences = Array.ConvertAll(student.Preference.Trim().Split(','), x => Convert.ToInt32(x.Trim()));
                for (int i = 0; i < preferences.Length; i++)
                {
                    var tempSupervisor = supervisor.Find(x => x.ID == preferences[i]);
                    if (tempSupervisor.StudentsAlloted < 2)
                    {
                        tempSupervisor.StudentsAlloted += 1;
                        student.Supervisor = tempSupervisor.ID;
                        student.ChoiceAlloted = (byte)(i + 1);
                        db.Entry(tempSupervisor).State = EntityState.Modified;
                        db.SaveChanges();
                        break;
                    }
                }
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
            }
            TempData["message"] = "Supervisor Allotment Successful";
            return RedirectToAction("Index");
        }

        public ActionResult ClearAllotment()
        {
            var supervisor = db.Supervisors.ToList();
            var students = db.Students.ToList();
            students.ForEach(x => { x.ChoiceAlloted = null; x.Supervisor = null; db.Entry(x).State = EntityState.Modified; db.SaveChanges(); });
            supervisor.ForEach(x => { x.StudentsAlloted = 0; db.Entry(x).State = EntityState.Modified; db.SaveChanges(); });
            TempData["message"] = "Allotment Cleared";
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
