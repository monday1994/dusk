using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bimber.Models.ViewModels
{
    public class ActivitiesViewModel
    {
        public List<User> Users { get; set; }

        public Place Place { get; set; }

        public Activity Activity { get; set; }

        public IEnumerable<Place> Places { get; set; }

        public List<Activity> Activities { get; set; }

        public List<TypeOfPlace> PlaceTypes { get; set; }

        public ActivitiesViewModel()
        {
            Users = new List<User>();
            Activities = new List<Activity>();
            PlaceTypes = new List<TypeOfPlace>();
            Places = new List<Place>();
        }
    }
}