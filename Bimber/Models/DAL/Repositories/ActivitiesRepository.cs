using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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

        public void AddNewUser(Activity activity, string username)
        {
            User user = db.Users.FirstOrDefault(u => u.Username.Equals(username));
            Activity tempActivity = db.Activities.FirstOrDefault(a => a.Name.Equals(activity.Name));         

            if (!activity.Name.Equals(tempActivity.Name))
            {
                tempActivity.Name = activity.Name;
            }

            if (activity.StartTime != tempActivity.StartTime)
            {
                tempActivity.StartTime = activity.StartTime;
            }            
           
            user.Activities.Add(tempActivity);

    
            user.Places.Add(tempActivity.Place);
            
            tempActivity.Users.Add(user);

            db.Entry(user).State = EntityState.Modified;
            db.Entry(tempActivity).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void AddPlace(Activity activity, string placename)
        {
            Place place = db.Places.FirstOrDefault(p => p.Name.Equals(placename));
            Activity tempActivity = db.Activities.FirstOrDefault(a => a.Name.Equals(activity.Name));


            if (!activity.Name.Equals(tempActivity.Name))
            {
                tempActivity.Name = activity.Name;
            }

            if (activity.StartTime != tempActivity.StartTime)
            {
                tempActivity.StartTime = activity.StartTime;
            }

            place.Activities.Add(tempActivity);            

            tempActivity.Place = place;

            db.Entry(place).State = EntityState.Modified;
            db.Entry(tempActivity).State = EntityState.Modified;

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
            Place tempPlace = activity.Place;

            if(tempPlace != null)
            {
                db.Entry(tempPlace).State = EntityState.Modified;
            }

            foreach (var user in activity.Users)
            {
                foreach(var tempActivity in user.Activities)
                {
                    if(tempActivity.ActivityId == activity.ActivityId)
                    {
                        user.Activities.Remove(tempActivity);
                        user.Activities.Add(activity);
                        db.Entry(user).State = EntityState.Modified;
                    }
                }
            }


            db.Entry(activity).State = EntityState.Modified;
            db.SaveChanges();
        }

    }
}
