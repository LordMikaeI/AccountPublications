using Kursach_Backend.Models.Users;
using Kursach_Backend.Models.Publications;

namespace Kursach_Backend.Models.Employees
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidleName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Publication> Publications { get; set; }
        public List<PublicationFile> PublicationFiles { get; set; }
    }
}
