using Bimber.Models.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models
{
    public class Preference
    {
        UsersRepository UserRepo = new UsersRepository();

        [Key, ForeignKey("User")]
        public int UserId { get; set; }

        public int Radius { get; set; }

        public Sex Sex { get; set; }

        public virtual User User { get; set; }

        public Preference()
        {
            Radius = 20000;
            Sex = Sex.BOTH;
        }
    }

    public enum Sex
    {
        MALE,
        FEMALE,
        BOTH
    }
}
