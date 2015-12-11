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
            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
