using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bimber.Models.ViewModels
{
    public class DbViewModel
    {
        public List<User> Users { get; set; }

        public Place Place { get; set; }

        public List<Activity> Activities { get; set; }

        public List<TypeOfPlace> PlaceTypes { get; set; }

        public DbViewModel()
        {
            Users = new List<User>();
            Activities = new List<Activity>();
            PlaceTypes = new List<TypeOfPlace>();
        }
    }
}