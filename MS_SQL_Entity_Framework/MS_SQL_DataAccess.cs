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

        public bool Delete(int personId)
        {
            var result = context.People.SingleOrDefault(x => x.PersonId == personId);

            if (result != null)
            {
                context.People.Remove(result);
                var deleteResult = context.SaveChanges();

                return deleteResult == 1;
            }
            return false;
        }

        public IEnumerable<IPerson> Get()
        {
            return context.People.ToList();
        }

        public IEnumerable<IPerson> GetBySearch(string? searchPhrase1, string searchPhrase2, int? age)
        {
            return context.People
                    .Where(p => searchPhrase1 == null || p.FirstName.Contains(searchPhrase1) || p.LastName.Contains(searchPhrase1))
                    .Where(p => searchPhrase2 == "" || (p.FirstName.Contains(searchPhrase2) || p.LastName.Contains(searchPhrase2)))
                    .Where(p => age == null || p.Age == age)
                    .ToList();
        }

        public bool Insert(IPerson person)
        {
            Person insertPerson = new()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age
            };
            _ = context.People.Add(insertPerson);
            var updateresult = context.SaveChanges();

            return updateresult == 1;
        }

        public bool Update(IPerson person)
        {
            var result = context.People.SingleOrDefault(x => x.PersonId == person.PersonId);

            if (result != null)
            {
                result.Age = person.Age;
                result.FirstName = person?.FirstName;
                result.LastName = person?.LastName;
                var updateResult = context.SaveChanges();

                return updateResult == 1;
            }
            return false;
        }
    }
}
