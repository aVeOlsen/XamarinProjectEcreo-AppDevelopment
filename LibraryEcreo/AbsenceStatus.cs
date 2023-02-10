using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEcreo
{
    public class AbsenceStatus
    {
        public AbsenceStatusRole AbsenceStatusRole { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
    }
    public enum AbsenceStatusRole
    {
        Onsite,
        Home,
        Sick,
        Late
    }
}
