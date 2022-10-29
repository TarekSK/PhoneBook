using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Person
    {
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public Guid CompanyId { get; set; }

        public Person()
        {

        }

        public Person(string fullName, string phoneNumber, string address, Guid companyId)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;  
            Address = address;
            CompanyId = companyId;
        }
    }
}
