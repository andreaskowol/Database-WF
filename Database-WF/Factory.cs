// Use for Dapper
using MS_SQL_Dapper;
using MS_SQL_Dapper.Interfaces;

//// Use for Entity Framework
//using MS_SQL_Entity_Framework;
//using MS_SQL_Entity_Framework.Interfaces;
//using MS_SQL_Entity_Framework.Models;


namespace Database_WF
{
    public static class Factory
    {
        public static IPerson CreatePerson()
        {
            return new Person();
        }

        public static IDataAccess CreateDataAccess()
        {
            return new MS_SQL_DataAccess(CreatePerson());
        }
    }
}
