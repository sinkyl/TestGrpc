using System.Threading.Tasks;
using System.Collections.Generic;
using TestGrpc.Models;
using MongoDB.Driver;
using GrpcServer.Database;

namespace GrpcServer.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        protected readonly IMongoCollection<T> Collection;

        protected BaseRepository(IMongoDBContext context)
        {
            Collection = context.GetCollection<T>();
        }

        public async ValueTask InsertOneAsync(T e)        
        {           
            await Collection.InsertOneAsync(e);        
        }

        public async ValueTask<T> FindByIdAsync(string id)
        {   
            var filter = Builders<T>.Filter.Eq(e => e.Id, id);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async ValueTask<T> FindOneByQueryAsync(FilterDefinition<T> filter)
        {
            var temp = await Collection.Find(filter).FirstOrDefaultAsync();
            return temp;
        }

        public async ValueTask<IEnumerable<T>> FindManyByQueryAsync(FilterDefinition<T> filter)
        {
            var res = await Collection.Find(filter).ToListAsync();
            return res;
        }
    }

    public interface IBaseRepository<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        ValueTask InsertOneAsync(T e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<T> FindByIdAsync(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        ValueTask<T> FindOneByQueryAsync(FilterDefinition<T> filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        ValueTask<IEnumerable<T>> FindManyByQueryAsync(FilterDefinition<T> filter);
    }
}