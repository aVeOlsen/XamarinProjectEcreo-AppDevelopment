using System;
using System.Collections.Generic;
using System.Text;

namespace EcreoLibraryStandard
{
    public class ContactInformation : BaseEntity
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool AdminStatus { get; set; }
    }
}
