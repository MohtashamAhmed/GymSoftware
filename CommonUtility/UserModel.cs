using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CommonUtility
{
   public class UserModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please Enter UserName")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Please Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

    }
}
