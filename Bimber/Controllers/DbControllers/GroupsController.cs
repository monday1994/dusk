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

namespace Bimber.Controllers
{
    public class GroupsController : Controller
    {
        private BimberDbContext db = new BimberDbContext();

        private GroupsRepository groupsRepo = new GroupsRepository();

        private UsersRepository usersRepo = new UsersRepository();

        // GET: Groups
        public ActionResult Index()
        {
            return View(groupsRepo.GetAll());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int id)
        {
            Group group = groupsRepo.GetById(id);

            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {           
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,Name")] Group group)
        {
            

            if (ModelState.IsValid)
            {
                groupsRepo.Add(group);

                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int id)
        {
            Group group = groupsRepo.GetById(id);
            group.Users = usersRepo.GetAll();

            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,Name,NumberOfUsers")] Group group)
        {
            string choosenUser = Request["Users"];           
            
            if (!choosenUser.Equals(""))
            {
                groupsRepo.AddNewUser(group, choosenUser);
            }

            if (ModelState.IsValid && choosenUser.Equals(""))
            {
                groupsRepo.Update(group);
                
            }
            return RedirectToAction("Index");
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int id)
        {
            Group group = groupsRepo.GetById(id);

            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            groupsRepo.RemoveById(id);

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
