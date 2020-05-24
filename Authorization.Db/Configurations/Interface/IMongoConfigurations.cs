namespace Authorization.Db.Configurations.Interface
{
    public interface IMongoConfigurations
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}