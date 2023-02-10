using System;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms;

namespace EcreoLibraryStandard
{
    public class User : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool AdminStatus { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public ImageSource ImageURL { get; set; }
        public Role Role { get; set; }
        public AbsenceStatusRole CurrentAbsenceStatus { get; set; }
        public ContactInformation PersonalInformation { get; set; }
        public string Password { get; set; }
    }
    public enum Role
    {
        RegularEmployee,
        ProductionAdminstrator,
        CustomerAdminstrator
    }
    public enum AbsenceStatusRole
    {
        OnSite,
        Home,
        Sick,
        Late,
    }
}

