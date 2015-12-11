using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Group()
        {
            Users = new List<User>();
        }

        public Group(string name) : this()
        {
            Name = name;
        }

    }
}
