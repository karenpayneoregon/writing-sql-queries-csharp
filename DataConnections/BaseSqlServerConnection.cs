using ExceptionsLibrary;

namespace DataConnections
{
    public class BaseSqlServerConnection : BaseExceptionsHandler
    {
        /// <summary>
        /// This points to your database server
        /// </summary>
        protected string DatabaseServer = ".\\SQLEXPRESS";
        /// <summary>
        /// Name of database containing required tables
        /// </summary>
        protected string DefaultCatalog = "NorthWindAzure1";
        public string ConnectionString =>
            $"Data Source={DatabaseServer};" +
            $"Initial Catalog={DefaultCatalog};"+ 
            "Integrated Security=True";
    }
}
