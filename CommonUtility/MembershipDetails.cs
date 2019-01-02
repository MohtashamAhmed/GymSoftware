using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
    public class MembershipDetails
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Price { get; set; }
        public bool IsActive { get; set; }
        public bool isoffer { get; set; }
        public DateTime Date { get; set; }
    }

    public class DisplayOffers
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter price")]
        public Nullable<int> Price { get; set; }
        public bool IsActive { get; set; }
        public bool isoffer { get; set; }
        public DateTime Date { get; set; }
        public List<MembershipDetails> Offerlist { get; set; }
    }
}
