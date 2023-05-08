using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudWithAdo.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return FirstName+" "+LastName;}
        }
    }
}
