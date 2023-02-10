using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinEcreo.Models
{
    public class UserModel
    {
        public string BaseID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public UserPersonalInfoModel PersonalInformation { get; set; }

    }
    public class UserPersonalInfoModel
    {

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
