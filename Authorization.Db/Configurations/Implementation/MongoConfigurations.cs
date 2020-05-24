namespace Authorization.Db.Configurations.Implementation
{
    using Interface;
    
    public class MongoConfigurations : IMongoConfigurations
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}