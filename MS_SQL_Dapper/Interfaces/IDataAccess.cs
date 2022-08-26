namespace MS_SQL_Dapper.Interfaces
{
    public interface IDataAccess
    {
        Task<bool> Delete(int personId);
        Task<IEnumerable<IPerson>> GetAsync();
        Task<IEnumerable<IPerson>> GetBySearch(string? searchPhrase1, string searchPhrase2, int? age);
        Task<bool> Insert(IPerson person);
        Task<bool> Update(IPerson person);
    }
}