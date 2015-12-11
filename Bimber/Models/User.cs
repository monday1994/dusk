using Bimber.Models.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models
{
    public class User
    {
        private UsersRepository UserRepo = new UsersRepository();

        public int UserId { get; set; }

        public string Username { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }

        public virtual ICollection<Place> Places { get; set; }

        public virtual Preference Preferences { get; set; }

        public User()
        {
            Groups = new List<Group>();
            Activities = new List<Activity>();
            Places = new List<Place>();            
        }

        public User(string username, double lat, double lon) : this()
        {
            Username = username;
            Lat = lat;
            Lon = lon;
        }

    }
}
