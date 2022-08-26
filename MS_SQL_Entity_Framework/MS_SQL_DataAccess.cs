using MS_SQL_Entity_Framework.Interfaces;
using MS_SQL_Entity_Framework.Models;

namespace MS_SQL_Entity_Framework
{
    public class MS_SQL_DataAccess : IDataAccess
    {
        readonly IPerson _person;
        readonly DataBaseContext context = new();

        public MS_SQL_DataAccess(IPerson person)
        {
            _person = person;
        }

        public async Task<bool> DeleteAsync(int personId)
        {
            var result = await Task.Run(() => context.People.FirstOrDefault(x => x.PersonId == personId));

            if (result != null)
            {
                await Task.Run(() => context.People.Remove(result));
                var deleteResult = await context.SaveChangesAsync();

                return deleteResult == 1;
            }
            return false;
        }

        public async Task<IEnumerable<IPerson>> GetAsync()
        {
            return await Task.Run(() => context.People.ToList());
        }

        public async Task<IEnumerable<IPerson>> GetBySearchAsync(string? searchPhrase1, string searchPhrase2, int? age)
        {
            return await Task.Run(() => context.People
                    .Where(p => searchPhrase1 == null || p.FirstName.Contains(searchPhrase1) || p.LastName.Contains(searchPhrase1))
                    .Where(p => searchPhrase2 == "" || (p.FirstName.Contains(searchPhrase2) || p.LastName.Contains(searchPhrase2)))
                    .Where(p => age == null || p.Age == age)
                    .ToList());
        }

        public async Task<bool> InsertAsync(IPerson person)
        {
            Person insertPerson = new()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age
            };
            _ = await context.People.AddAsync(insertPerson);
            var updateresult = await context.SaveChangesAsync();

            return updateresult == 1;
        }

        public async Task<bool> UpdateAsync(IPerson person)
        {
            var result = await Task.Run(() => context.People.SingleOrDefault(x => x.PersonId == person.PersonId));

            if (result != null)
            {
                result.Age = person.Age;
                result.FirstName = person?.FirstName;
                result.LastName = person?.LastName;
                var updateResult = await context.SaveChangesAsync();

                return updateResult == 1;
            }
            return false;
        }
    }
}
