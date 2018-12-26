using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtility
{
    public class DashboardModel
    {
        public string UpcomingBirthdays { get; set; }
        public string TotalMembers { get; set; }
        public string MonthlySales { get; set; }
        public string FestiveOffer { get; set; }
    }

    public class Graph
    {
        public string Month { get; set; }
        public int Revenue { get; set; }
    }
}
