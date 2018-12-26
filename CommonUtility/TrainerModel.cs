using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommonUtility
{
   public class TrainerModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string TrainerName { get; set; }
        [Required(ErrorMessage = "Select Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Enter Mobile")]
        [Remote("CheckMobile", "Trainer", HttpMethod = "POST", ErrorMessage = "This Mobile number is already taken")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
