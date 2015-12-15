using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models.DAL.Repositories
{
    class PlacesRepository
    {
        private BimberDbContext db = new BimberDbContext();

        public PlacesRepository()
        {
            //empty
        }

        public Place GetById(int id)
        {
            Place place = db.Places.FirstOrDefault(p => p.PlaceId == id);
            
            return place;
        }

        public Place GetById(string name)
        {
            Place place = db.Places.FirstOrDefault(p => p.Name.Equals(name));
            
            return place;
        }

        public List<Place> GetAll()
        {
            return db.Places.ToList();
        }

        public void Add(Place place)
        {
            db.Places.Add(place);
            db.SaveChanges();
        }

        public void AddNewUser(Place place, string username)
        {
            User tempUser = db.Users.FirstOrDefault(u => u.Username.Equals(username));

            tempUser.Places.Add(place);
            place.Users.Add(tempUser);

            db.Entry(tempUser).State = EntityState.Modified;
            db.Entry(place).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void AddNewActivity(Place place, string activityName)
        {
            Activity tempActivity = db.Activities.FirstOrDefault(a => a.Name.Equals(activityName));

            tempActivity.Place = place;
            place.Activities.Add(tempActivity);

            db.Entry(tempActivity).State = EntityState.Modified;
            db.Entry(place).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Place tempPlace = GetById(id);

            foreach(var activity in tempPlace.Activities.ToList())
            {
                db.Activities.Remove(activity);
            }
            
            db.Places.Remove(tempPlace);
            db.SaveChanges();
        }

        public void Update(int id)
        {
            Place tempPlace = GetById(id);
            db.Entry(tempPlace).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Update(Place place)
        {
            foreach(var placeActivity in place.Activities)
            {
                if (placeActivity.Place.PlaceId == place.PlaceId)
                {
                    placeActivity.Place = place;
                    db.Entry(placeActivity).State = EntityState.Modified;
                }
            }

            foreach(var user in place.Users)
            {
                foreach(var tempPlace in user.Places)
                {
                    if(tempPlace.PlaceId == place.PlaceId)
                    {
                        user.Places.Remove(tempPlace);
                        user.Places.Add(place);
                        db.Entry(user.Places).State = EntityState.Modified;
                    }
                }
            }

            db.Entry(place).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
