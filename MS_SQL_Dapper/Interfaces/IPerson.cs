namespace MS_SQL_Dapper.Interfaces
{
    public interface IPerson
    {
        int? Age { get; set; }
        string? FirstName { get; set; }
        string FullInfo { get; set; }
        string? LastName { get; set; }
        int PersonId { get; set; }
    }
}