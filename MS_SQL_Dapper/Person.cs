using MS_SQL_Dapper.Interfaces;

namespace MS_SQL_Dapper
{
    public class Person : IPerson
    {
        public int PersonId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public int? Age { get; set; }

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
