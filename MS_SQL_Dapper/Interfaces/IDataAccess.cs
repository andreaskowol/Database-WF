namespace MS_SQL_Dapper.Interfaces
{
    public interface IDataAccess
    {
        Task<bool> DeleteAsync(int personId);
        Task<IEnumerable<IPerson>> GetAsync();
        Task<IEnumerable<IPerson>> GetBySearchAsync(string? searchPhrase1, string searchPhrase2, int? age);
        Task<bool> InsertAsync(IPerson person);
        Task<bool> UpdateAsync(IPerson person);
    }
}