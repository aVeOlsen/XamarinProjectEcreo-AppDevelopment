using System;
using System.Collections.Generic;
using System.Text;

namespace EcreoLibraryStandard
{
    public class ConfenrenceRoom : BaseEntity
    {
        public RoomAvailability Available { get; set; }
        public string Placement { get; set; }

    }
}
