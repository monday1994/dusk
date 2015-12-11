using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bimber.Models;
using Bimber.Models.DAL;
using System.Diagnostics;
using Bimber.Models.DAL.Repositories;

namespace Bimber.Controllers
{
    public class ActivitiesController : Controller
    {
        private BimberDbContext db = new BimberDbContext();

        private ActivitiesRepository activitiesRepo = new ActivitiesRepository();

        // GET: Activities
        public ActionResult Index()
        {
            var activities = activitiesRepo.GetAll();

            return View(activities);
        }

        // GET: Activities/Details/5
        public ActionResult Details(int id)
        {
            Activity activity = activitiesRepo.GetById(id);

            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityId,Name,PlaceId,StartTime,PlaceType")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                activitiesRepo.Add(activity);

                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int id)
        {
            Activity activity = activitiesRepo.GetById(id);

            if (activity == null)
            {
                return HttpNotFound();
            }
          
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityId,Name,PlaceId,StartTime,PlaceType")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                activitiesRepo.Update(activity);

                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int id)
        {
            Activity activity = activitiesRepo.GetById(id);

            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            activitiesRepo.RemoveById(id);

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
