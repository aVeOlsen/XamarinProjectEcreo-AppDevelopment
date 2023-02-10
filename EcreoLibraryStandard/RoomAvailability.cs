using System;
using System.Collections.Generic;
using System.Text;

namespace EcreoLibraryStandard
{
    public class RoomAvailability : BaseEntity
    {
        public List<RoomAvailabilityTime> Times { get; set; }
    }
}
