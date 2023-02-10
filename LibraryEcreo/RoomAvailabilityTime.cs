using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEcreo
{
    public class RoomAvailabilityTime
    {
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public bool Available { get; set; } = false;
    }
}
