namespace Authorization.Entities.Base.Implementation
{
    using System;
    using MongoDB.Bson;
    using Interface;
    
    public class Document : IDocument
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
    }
}