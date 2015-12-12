using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models.DAL.Repositories
{
    class GroupsRepository
    {
        private BimberDbContext db = new BimberDbContext();

        public GroupsRepository()
        {
            //empty
        }

        public Group GetById(int id)
        {
            Group group = db.Groups.FirstOrDefault(g => g.GroupId == id);
                       
            return group;
        }

        public Group GetById(string name)
        {
            Group group = db.Groups.FirstOrDefault(g => g.Name.Equals(name));
            
            return group;
        }

        public List<Group> GetAll()
        {
            return db.Groups.ToList();
        }

        public void Add(Group group)
        {
            db.Groups.Add(group);
            db.SaveChanges();
           
        }

        public void AddNewUser(Group group, string username)
        {
            User user = db.Users.FirstOrDefault(u => u.Username.Equals(username));

            user.Groups.Add(group);
            group.Users.Add(user);

            db.Entry(user).State = EntityState.Modified;
            db.Entry(group).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void RemoveById(int id)
        {
            Group tempGroup = GetById(id);
            db.Groups.Remove(tempGroup);
            db.SaveChanges();
        }

        public void Update(int id)
        {
            Group tempGroup = GetById(id);
            db.Entry(tempGroup).State = EntityState.Modified;
            db.SaveChanges();
        }
        public void Update(Group group)
        {
            foreach(var user in group.Users)
            {
                foreach(var tempGroup in user.Groups)
                {
                    if(tempGroup.GroupId == group.GroupId)
                    {
                        user.Groups.Remove(tempGroup);
                        user.Groups.Add(group);

                        db.Entry(user).State = EntityState.Modified;
                    }
                }
            }

            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
