namespace MS_SQL_Entity_Framework.Interfaces
{
    public interface IDataAccess
    {
        IEnumerable<IPerson> Get();
        bool Insert(IPerson person);
        bool Update(IPerson person);
        bool Delete(int personId);
        IEnumerable<IPerson> GetBySearch(string? searchPhrase1, string searchPhrase2, int? age);
    }
}
