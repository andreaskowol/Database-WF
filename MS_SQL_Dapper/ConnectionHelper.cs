using System.Configuration;

namespace MS_SQL_Dapper
{
    class ConnectionHelper
    {
        public static string ConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
