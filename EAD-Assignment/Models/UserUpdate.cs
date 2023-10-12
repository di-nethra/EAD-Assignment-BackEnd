namespace EAD_Assignment.Models
{
    public class UserUpdate
    {
        public string NIC { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
