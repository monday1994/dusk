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
using Bimber.Models.DAL.Repositories;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;

namespace Bimber.Controllers
{
    public class UsersController : Controller
    {
        private BimberDbContext db = new BimberDbContext();
        private UsersRepository usersRepo = new UsersRepository();

        // GET: Users
        public ActionResult Index()
        {
            var users = usersRepo.GetAll();
            return View(users.ToList());
        }
      
        // GET: Users/Create
        public ActionResult Create()
        {          
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                usersRepo.Add(user);

                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            User user = usersRepo.GetById(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,Lat,Lon")] User user)
        {
            if (ModelState.IsValid)
            {
                usersRepo.Update(user);
                return RedirectToAction("Index");
            }
        
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            User user = usersRepo.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usersRepo.RemoveById(id);
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
