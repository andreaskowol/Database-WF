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


        public async Task<bool> DeleteAsync(int personId)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionString("Dapper-WF"));

            IPerson deletePerson = _person;
            deletePerson.PersonId = personId;

            return await connection.ExecuteAsync("dbo.DeletePerson @PersonId", deletePerson) == 1;
        }

        public async Task<IEnumerable<IPerson>> GetAsync()
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionString("Dapper-WF"));

            return await connection.QueryAsync<Person>("dbo.GetAll");
        }

        public async Task<IEnumerable<IPerson>> GetBySearchAsync(string? searchPhrase1, string searchPhrase2, int? age)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionString("Dapper-WF"));

            return await connection.QueryAsync<Person>("dbo.SelectBySearchPhrase @SearchPhrase1, @SearchPhrase2, @Age",
                new
                {
                    SearchPhrase1 = searchPhrase1,
                    SearchPhrase2 = searchPhrase2,
                    Age = age
                });
        }


        public async Task<bool> InsertAsync(IPerson person)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionString("Dapper-WF"));

            return await connection.ExecuteAsync("dbo.InsertPerson @LastName, @FirstName, @Age", person) == 1;
        }

        public async Task<bool> UpdateAsync(IPerson person)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(ConnectionHelper.ConnectionString("Dapper-WF"));

            return await connection.ExecuteAsync("dbo.UpdatePerson @PersonId, @LastName, @FirstName, @Age", person) == 1;
        }
    }
}
