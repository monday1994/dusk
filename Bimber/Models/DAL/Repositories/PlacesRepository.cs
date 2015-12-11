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

        public void RemoveById(int id)
        {
            Place tempPlace = GetById(id);
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
            db.Entry(place).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
