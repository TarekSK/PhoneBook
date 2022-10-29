using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Company
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<Person> Persons { get; set; }

        public Company()
        {
            Id = Guid.NewGuid();
            Persons = new List<Person>() { };
        }

        [NotMapped]
        public int NoOfPeople { 
            get { 
                if(Persons == null) return 0; 
                if(Persons.Count == 0) return 0;
                else return Persons.Count;  
            } 
        }
    }
}
