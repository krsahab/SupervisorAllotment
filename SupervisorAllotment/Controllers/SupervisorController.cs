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
    public class SupervisorController : Controller
    {
        private SupervisorAllotmentDBEntities db = new SupervisorAllotmentDBEntities();

        // GET: Supervisor
        public ActionResult Index()
        {
            ViewData["Students"] = db.Students.ToList();
            return View(db.Supervisors.ToList());
        }

        // GET: Supervisor/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisor supervisor = db.Supervisors.Find(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            return View(supervisor);
        }

        // GET: Supervisor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supervisor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Email,Phone,StudentsAlloted")] Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                if (db.Supervisors.Find(supervisor.ID)==null)
                {
                    db.Supervisors.Add(supervisor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "ID Already Exists");
            }
            return View(supervisor);
        }

        // GET: Supervisor/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisor supervisor = db.Supervisors.Find(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            return View(supervisor);
        }

        // POST: Supervisor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Email,Phone,StudentsAlloted")] Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supervisor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supervisor);
        }

        // GET: Supervisor/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisor supervisor = db.Supervisors.Find(id);
            if (supervisor == null)
            {
                return HttpNotFound();
            }
            return View(supervisor);
        }

        // POST: Supervisor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Supervisor supervisor = db.Supervisors.Find(id);
            db.Supervisors.Remove(supervisor);
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
