namespace LibraryEcreo
{
    public class User
    {
        public string? UserID { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public bool AdminStatus { get; set; }
        public Role Role { get; set; }
        public AbsenceStatus? AbsenceStatus { get; set; }

    }
        public enum Role
        {
            ProductionAdminstrator,
            CustomerAdminstrator
        }
}