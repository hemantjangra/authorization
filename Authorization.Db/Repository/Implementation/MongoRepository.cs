using System.Reflection;

namespace Authorization.Db.Repository.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Attributes;
    using Authorization.Db.Configurations.Interface;
    using Interface;
    using Authorization.Entities.Base.Interface;
    using MongoDB.Bson;
    using MongoDB.Driver;
    
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument:IDocument
    {

        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IMongoConfigurations mongoConfigurations)
        {
            var database = new MongoClient(mongoConfigurations.ConnectionString)
                .GetDatabase(mongoConfigurations.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));

        }

        private protected string GetCollectionName(Type documentType)
        {
            var collectionName = ((BsonCollectionAttribute) documentType
                .GetCustomAttributes(typeof(BsonCollectionAttribute), true)
                .FirstOrDefault())?.CollectionName;

            return string.IsNullOrEmpty(collectionName) ? documentType.Name : collectionName;
        }
        
        public IQueryable<TDocument> AsQuerable()
        {
            return _collection.AsQueryable();
        }

        public virtual IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }

        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }

        public virtual TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).Limit(1).FirstOrDefault();
        }

        public virtual async Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression)
        {
            var asyncCursor = await _collection.FindAsync(filterExpression, new FindOptions<TDocument>{Limit = 1});
            return await asyncCursor.FirstOrDefaultAsync();
        }

        public virtual TDocument FindById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, new ObjectId(id));
            return _collection.Find(filter).Limit(1).FirstOrDefault();
        }

        public virtual async Task<TDocument> FindByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, new ObjectId(id));

            var asyncCursor = await _collection.FindAsync(filter, new FindOptions<TDocument>{Limit = 1});
            return asyncCursor.FirstOrDefault();
        }

        public virtual void InsertOne(TDocument document)
        {
            _collection.InsertOne(document);
        }

        public virtual async Task<TDocument> InsertOneAsync(TDocument document)
        {
            await _collection.InsertOneAsync(document);
            return document;
        }

        public virtual void InsertMany(ICollection<TDocument> documents)
        {
            _collection.InsertMany(documents);
        }

        public async Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.InsertManyAsync(documents);
        }

        public virtual bool ReplaceOne(TDocument document, string field, object val)
        {
            var filter = Builders<TDocument>.Filter.Eq(field, val);
            var replaceResults = _collection.ReplaceOne(filter, document);
            return replaceResults.IsAcknowledged;
        }
        
        public virtual async Task<bool> ReplaceOneAsync(TDocument document, string field, object val)
        {
            var filter = Builders<TDocument>.Filter.Eq(field, val);
            var replaceResults = await _collection.ReplaceOneAsync(filter, document);
            return replaceResults.IsAcknowledged;
        }

        public virtual bool DeleteOne(Expression<Func<TDocument, bool>> filter)
        {
            var deleteResults =_collection.DeleteOne(filter);
            return deleteResults.IsAcknowledged;
        }

        public virtual async Task<bool> DeleteOneAsync(Expression<Func<TDocument, bool>> filter)
        {
            var deleteResults = await _collection.DeleteOneAsync(filter);
            return deleteResults.IsAcknowledged;
        }

        public virtual void DeleteById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(document => document.Id, new ObjectId(id));
            _collection.FindOneAndDelete(filter); // return deleted document
        }

        public virtual async Task DeleteByIdAsync(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(document => document.Id, new ObjectId(id));
            await _collection.FindOneAndDeleteAsync(filter); //return deleted document
        }

        public virtual bool DeleteMany(Expression<Func<TDocument, bool>> filter)
        {
            var deleteResult = _collection.DeleteMany(filter);
            return deleteResult.IsAcknowledged;
        }
        public virtual async Task<bool> DeleteManyAsync(Expression<Func<TDocument, bool>> filter)
        {
            var deleteResult = await _collection.DeleteManyAsync(filter);
            return deleteResult.IsAcknowledged;
        }
    }
}