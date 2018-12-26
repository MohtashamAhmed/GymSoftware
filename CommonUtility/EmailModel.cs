using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
    public class EmailModel
    {
        public List<ReminderEmail> ReminderList { get; set; }
        public List<BdayEmail> BdayList { get; set; }
    }

    public class ReminderEmail
    {
        public string Name { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int days { get; set; }
    }

    public class BdayEmail
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
