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
using Bimber.Models.ViewModels;

namespace Bimber.Controllers
{
    public class PlacesController : Controller
    {
        private BimberDbContext db = new BimberDbContext();

        private PlacesRepository placesRepo = new PlacesRepository();

        private ActivitiesRepository activitiesRepo = new ActivitiesRepository();

        private UsersRepository usersRepo = new UsersRepository();

        // GET: Places
        public ActionResult Index()
        {
            return View(placesRepo.GetAll());
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaceId,Name,Lon,Lat")] Place place)
        {
            string placeType = Request["PlaceType"];

            switch (placeType)
            {
                case "PUB":
                    place.PlaceType = TypeOfPlace.PUB;
                    break;
                case "RESTAURANT":
                    place.PlaceType = TypeOfPlace.RESTAURANT;
                    break;
                case "CLUB":
                    place.PlaceType = TypeOfPlace.CLUB;
                    break;
            }

            if (ModelState.IsValid)
            {
                placesRepo.Add(place);

                return RedirectToAction("Index");
            }

            return View(place);
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int id)
        {
            Place place = placesRepo.GetById(id);

            place.Activities = activitiesRepo.GetAll();
            place.Users = usersRepo.GetAll();
           
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaceId,Name,Lon,Lat")] Place place)
        {
            string choosenActivity = Request["Activities"];
            string choosenUser = Request["Users"];
            string placeType = Request["PlaceType"];

            switch (placeType)
            {
                case "PUB":
                    place.PlaceType = TypeOfPlace.PUB;
                    break;
                case "RESTAURANT":
                    place.PlaceType = TypeOfPlace.RESTAURANT;
                    break;
                case "CLUB":
                    place.PlaceType = TypeOfPlace.CLUB;
                    break;
                case "":
                    break;
            }

            if (!choosenActivity.Equals(""))
            {
                placesRepo.AddNewActivity(place, choosenActivity);
            }

            if (!choosenUser.Equals(""))
            {
                placesRepo.AddNewUser(place, choosenUser);
            }

            if (ModelState.IsValid && choosenUser.Equals("") && choosenActivity.Equals(""))
            {
                placesRepo.Update(place);                
            }
            return RedirectToAction("Index");
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int id)
        {
            Place place = placesRepo.GetById(id);

            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            placesRepo.RemoveById(id);

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
