namespace Authorization.Db.Repository.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Authorization.Entities.Base.Interface;
    using System.Threading.Tasks;

    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        IQueryable<TDocument> AsQuerable();

        IEnumerable<TDocument> FilterBy(Expression<Func<TDocument, bool>> filterExpression);

        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        TDocument FindOne(Expression<Func<TDocument, bool>> filterExpression);

        Task<TDocument> FindOneAsync(Expression<Func<TDocument, bool>> filterExpression);

        TDocument FindById(string id);

        Task<TDocument> FindByIdAsync(string id);

        void InsertOne(TDocument document);

        Task<TDocument> InsertOneAsync(TDocument document);

        void InsertMany(ICollection<TDocument> documents);

        Task InsertManyAsync(ICollection<TDocument> documents);

        bool ReplaceOne(TDocument document, string field, object val);

        Task<bool> ReplaceOneAsync(TDocument document, string field, object val);

        bool DeleteOne(Expression<Func<TDocument, bool>> filter);

        Task<bool> DeleteOneAsync(Expression<Func<TDocument, bool>> filter);

        void DeleteById(string id);

        Task DeleteByIdAsync(string id);

        bool DeleteMany(Expression<Func<TDocument, bool>> filter);

        Task<bool> DeleteManyAsync(Expression<Func<TDocument, bool>> filter);
    }
}