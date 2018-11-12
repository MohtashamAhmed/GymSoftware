using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CommonUtility
{
    public class CustomerRegistration
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Source { get; set; }
        public string DateofBirth { get; set; }
        public string Weight { get; set; }
        public string JoinDate { get; set; }
        public int TrainerID { get; set; }
        public int MembershipID { get; set; }
        public string MembershipName { get; set; }
        public int BatchID { get; set; }
        public string BatchName { get; set; }
        public string TotalPayment { get; set; }
        [Required]
        public int Payment { get; set; }
        public int Outstanding { get; set; }
        public DateTime DateOfPayment { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool ExistingUser { get; set; }
        public List<DisplayCustomers> UsersList { get; set; }
    }

    public class DisplayCustomers
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string MembershipName { get; set; }
        public string BatchName { get; set; }
        public string TotalPayment { get; set; }
        public int Payment { get; set; }
        public int Outstanding { get; set; }
        public DateTime DateOfPayment { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
