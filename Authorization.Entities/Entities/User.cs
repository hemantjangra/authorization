namespace Authorization.Entities.Entities
{
    using Db.Attributes;
    using Base.Implementation;

    [BsonCollection("user")]
    public class User : Document
    {
        public int UserId { get; set; }
        
        public string Name { get; set; }
        
        public string Password { get; set; }
        
        public string Role { get; set; }
        
        public string FavouriteColor { get; set; }
    }
}