using MS_SQL_Entity_Framework.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS_SQL_Entity_Framework.Models
{
    public partial class Person : IPerson
    {
        public int PersonId { get; set; }
        public string LastName { get; set; } = null!;
        public string? FirstName { get; set; }
        public int? Age { get; set; }

        [NotMapped]
        public string FullInfo
        {
            get
            {
                return $"{FirstName} {LastName} Age: {Age}";
            }
            set
            {
                ;
            }
        }
    }
}
