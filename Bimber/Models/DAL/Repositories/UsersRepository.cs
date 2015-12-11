using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models.DAL.Repositories
{
    class UsersRepository
    {
        private BimberDbContext db = new BimberDbContext();

        private PreferencesRepository prefRepo = new PreferencesRepository();

        public UsersRepository()
        {
            //empty
        }

        public User GetById(int id)
        {   
            User user = db.Users.FirstOrDefault(u => u.UserId == id);           

            return user;
        }

        public User GetById(string name)
        {
            User user = db.Users.FirstOrDefault(u => u.Username.Equals(name));

            return user;
        }

        public List<User> GetAll()
        {
            List<User> usersList = db.Users.ToList();

            return usersList;
        }

        public int GetUserId(string name)
        {
            int id = db.Users.FirstOrDefault(u => u.Username.Equals(name)).UserId;

            return id;
        }

        public void Add(User user)
        {
            Preference prefToBeAdded = new Preference();

            User userToBeAdded = new User(user.Username, user.Lat, user.Lon);

            userToBeAdded.Preferences = prefToBeAdded;
            prefToBeAdded.User = userToBeAdded;

            db.Users.Add(userToBeAdded);
            db.Preferences.Add(prefToBeAdded);
            db.SaveChanges();
        }

        public void RemoveById(int id)
        {
            User tempUser = GetById(id);
            Preference tempPref = prefRepo.GetById(tempUser.UserId);
            
            prefRepo.RemoveById(tempPref.UserId);           

            db.Users.Remove(tempUser);
            db.SaveChanges();         
        }

        public void Update(int id)
        {
            User tempUser = GetById(id);
            db.Entry(tempUser).State = EntityState.Modified;
            db.SaveChanges();
          
        }
        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
         
        }

        public void DisposeDbConnection()
        {
            db.Dispose();
        }
    }
}
