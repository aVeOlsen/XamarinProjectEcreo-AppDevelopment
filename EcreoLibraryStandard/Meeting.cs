using System;
using System.Collections.Generic;
using System.Text;

namespace EcreoLibraryStandard
{
    public class Meeting : BaseEntity
    {
        public string Title { get; set; }
        public DateTime TimeOfMeeting { get; set; }
        public List<User> Users { get; set; }

    }
}
