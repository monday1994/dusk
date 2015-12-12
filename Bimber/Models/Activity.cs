using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }

        public string Name { get; set; }

        public virtual Place Place { get; set; }

        public DateTime? StartTime { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Activity()
        {
            Users = new List<User>();
        }

        public Activity(string name, DateTime startTime) : this()
        {            
            Name = name;
            StartTime = startTime;
        }
    }

    
}
