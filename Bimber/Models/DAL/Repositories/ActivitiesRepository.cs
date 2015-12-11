using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models.DAL.Repositories
{
    class ActivitiesRepository
    {
        private BimberDbContext db = new BimberDbContext();

        public ActivitiesRepository()
        {
            //empty
        }

        public Activity GetById(int id)
        {
            Activity activity = db.Activities.FirstOrDefault(a => a.ActivityId == id);
            
            return activity;
        }

        public Activity GetById(string name)
        {
            Activity activity = db.Activities.FirstOrDefault(a => a.Name.Equals(name));
            
            return activity;
        }

        public List<Activity> GetAll()
        {
            return db.Activities.ToList();
        }

        public void Add(Activity activity)
        {
            db.Activities.Add(activity);
            db.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Activity tempActivity = GetById(id);
            db.Activities.Remove(tempActivity);
            db.SaveChanges();
        }

        public void Update(int id)
        {
            Activity tempActivity = GetById(id);
            db.Entry(tempActivity).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Update(Activity activity)
        {
            db.Entry(activity).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}
