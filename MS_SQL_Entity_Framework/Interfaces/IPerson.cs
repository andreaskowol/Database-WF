namespace MS_SQL_Entity_Framework.Interfaces
{
    public interface IPerson
    {
        int? Age { get; set; }
        string? FirstName { get; set; }
        string FullInfo { get; set; }
        string LastName { get; set; }
        int PersonId { get; set; }
    }
}