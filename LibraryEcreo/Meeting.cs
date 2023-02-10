using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEcreo
{
    public class Meeting
    {
        public string? Title { get; set; }
        public DateTime TimeOfMeeting { get; set; }
        public List<User>? Users { get; set; }



    }
}
