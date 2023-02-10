using System;
using System.Collections.Generic;
using System.Text;

namespace EcreoLibraryStandard
{
    public class AbsenceStatus:BaseEntity
    {
        public AbsenceStatusRole AbsenceStatusRole { get; set; }
        public DateTime FromeTime { get; set; }
        public DateTime ToTime { get; set; }
        public User User { get; set; }

    }
}
