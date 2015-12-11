using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimber.Models
{
    public class PhotoLink
    {
        public int PhotoLinkId { get; set; }

        public string Link { get; set; }

        public virtual Place Place { get; set; }
        public PhotoLink()
        {
            //empty
        }
    }
}
