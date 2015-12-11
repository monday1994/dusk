using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models.DAL.Repositories
{
    class PreferencesRepository
    {
        private BimberDbContext db = new BimberDbContext();

        public PreferencesRepository()
        {
            //empty
        }

        public Preference GetById(int id)
        {
            return db.Preferences.FirstOrDefault(p => p.UserId == id);
        }

        public List<Preference> GetAll()
        {
            return db.Preferences.ToList();
        }

        public void Add(Preference preference)
        {
            db.Preferences.Add(preference);
            db.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Preference tempPreference = GetById(id);
            db.Preferences.Remove(tempPreference);
            db.SaveChanges();
        }

        public void Update(int id)
        {
            Preference tempPreference = GetById(id);
            db.Entry(tempPreference).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Update(Preference preference)
        {
            db.Entry(preference).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
