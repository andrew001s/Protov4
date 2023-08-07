using MongoDB.Driver;

namespace Protov4.DAO
{
    public class DBMongo
    {
        private readonly IMongoDatabase _db;

        public DBMongo(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDBConnection");
            var client = new MongoClient(connectionString);
            var databaseName = configuration.GetConnectionString("DatabaseName");
            _db = client.GetDatabase(databaseName);
        }

        public IMongoDatabase GetDatabase()
        {
            return _db;
        }
    }
}
