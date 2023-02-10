using System;
using System.Collections.Generic;
using System.Text;

namespace EcreoLibraryStandard
{
    public class RoomAvailabilityTime : BaseEntity
    {
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public bool Available { get; set; } = false;

    }
}
