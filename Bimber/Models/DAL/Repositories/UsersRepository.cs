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

        public void AddNewGroup(User user,string groupToBeAdd)
        {
            Group tempGroup = db.Groups.FirstOrDefault(g => g.Name.Equals(groupToBeAdd));

            user.Groups.Add(tempGroup);
            tempGroup.Users.Add(user);

            db.Entry(user).State = EntityState.Modified;
            db.Entry(tempGroup).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void AddNewActivity(User user, string activityToBeAdd)
        {
            Activity tempActivity = db.Activities.FirstOrDefault(g => g.Name.Equals(activityToBeAdd));

            user.Activities.Add(tempActivity);
            user.Places.Add(tempActivity.Place);
            tempActivity.Users.Add(user);

            db.Entry(user).State = EntityState.Modified;
            db.Entry(tempActivity).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void AddNewPlace(User user, string placeToBeAdd)
        {
            Place tempPlace = db.Places.FirstOrDefault(g => g.Name.Equals(placeToBeAdd));

            user.Places.Add(tempPlace);
            tempPlace.Users.Add(user);

            db.Entry(user).State = EntityState.Modified;
            db.Entry(tempPlace).State = EntityState.Modified;

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

        //method updates all entities where user exists
        public void Update(User user)
        {            
            foreach(var userGroup in user.Groups)
            {
                //userGroup.Users.FirstOrDefault(u => u.UserId == user.UserId).Username = user.Username;
                //db.Entry(userGroup).State = EntityState.Modified;
                foreach (var tempUser in userGroup.Users)
                {
                    if (tempUser.UserId == user.UserId)
                    {
                        userGroup.Users.Remove(tempUser);
                        userGroup.Users.Add(user);
                        db.Entry(userGroup).State = EntityState.Modified;
                    }
                }

            }

            foreach (var userPlace in user.Places)
            {
                foreach (var tempUser in userPlace.Users)
                {
                    if (tempUser.UserId == user.UserId)
                    {
                        userPlace.Users.Remove(tempUser);
                        userPlace.Users.Add(user);
                        db.Entry(userPlace).State = EntityState.Modified;
                    }
                }
            }

            foreach (var userActivity in user.Activities)
            {
                foreach (var tempUser in userActivity.Users)
                {
                    if (tempUser.UserId == user.UserId)
                    {
                        userActivity.Users.Remove(tempUser);
                        userActivity.Users.Add(user);
                        db.Entry(userActivity).State = EntityState.Modified;
                    }
                }
            }
            

            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
         
        }

        public void DisposeDbConnection()
        {
            db.Dispose();
        }
    }
}
