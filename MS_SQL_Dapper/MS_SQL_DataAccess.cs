using Dapper;
using MS_SQL_Dapper.Interfaces;
using System.Data;


namespace MS_SQL_Dapper
{
    public class MS_SQL_DataAccess : IDataAccess
    {
        readonly IPerson _person;

        public MS_SQL_DataAccess(IPerson person)
        {
            _person = person;
        }


        public bool Delete(int personId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionString("Dapper-WF")))
            {
                IPerson deletePerson = _person;
                deletePerson.PersonId = personId;

                var result = connection.Execute("dbo.DeletePerson @PersonId", deletePerson);

                return result == 1;
            }
        }

        public IEnumerable<IPerson> Get()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionString("Dapper-WF")))
            {
                return connection.Query<Person>("dbo.GetAll").ToList();
            }
        }

        public IEnumerable<IPerson> GetBySearch(string? searchPhrase1, string searchPhrase2, int? age)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionString("Dapper-WF")))
            {
                return connection.Query<Person>("dbo.SelectBySearchPhrase @SearchPhrase1, @SearchPhrase2, @Age",
                    new
                    {
                        SearchPhrase1 = searchPhrase1,
                        SearchPhrase2 = searchPhrase2,
                        Age = age
                    }).ToList();
            }
        }


        public bool Insert(IPerson person)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionString("Dapper-WF")))
            {
                var result = connection.Execute("dbo.InsertPerson @LastName, @FirstName, @Age", person);

                return result == 1;
            }
        }

        public bool Update(IPerson person)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionString("Dapper-WF")))
            {
                var result = connection.Execute("dbo.UpdatePerson @PersonId, @LastName, @FirstName, @Age", person);

                return result == 1;
            }
        }
    }
}
