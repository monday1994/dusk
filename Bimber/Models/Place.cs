using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models
{
    public class Place
    {
        public int PlaceId { get; set; }

        [Display(Name="Place name")]
        public string Name { get; set; }        

        public double Lon { get; set; }
        public double Lat { get; set; }
        
        public TypeOfPlace PlaceType { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }

        public virtual ICollection<PhotoLink> PhotoList { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Place()
        {
            PhotoList = new List<PhotoLink>();
            Users = new List<User>();
            Activities = new List<Activity>();
        }

        public Place(string name, double lon, double lat, TypeOfPlace placeType) : this()
        {            
            Name = name;
            Lon = lon;
            Lat = lat;
            PlaceType = placeType;
        }
    }
    public enum TypeOfPlace
    {
        PUB,
        CLUB,
        RESTAURANT,
    }
}
