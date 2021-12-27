using System.Threading.Tasks;
using TestGrpc.Models;
using GrpcServer.Repositories;
using ProtoBuf.Grpc;
using System.Collections.Generic;
using TestGrpc.Contracts;
using MongoDB.Bson;
using Grpc.Core;
using MongoDB.Driver;

namespace GrpcServer.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : Base
    {
        private readonly IBaseRepository<T> _repo;

        protected BaseService(IBaseRepository<T> repo)
        {
            _repo = repo;
        }

        // ------ CREATE ------
        public async Task<RStatus> CreateOneAsync(T request, CallContext context = default)
        {
            try
            {
                await _repo.InsertOneAsync(request);

                return await Task.FromResult(
                    new RStatus
                    {
                        Message = "OK"
                    });
            }
            catch (RpcException)
            {
                return null;
            }
        }

        // ------ FIND ------
        public async ValueTask<T> GetByIdAsync(string id, CallContext context = default)
        {
            try
            {
                FilterDefinition<T> filter = FilterDefinition<T>.Empty;
                if (ObjectId.TryParse(id, out _)) { /* validate id */
                    return await _repo.FindByIdAsync(id);
                }
                else {
                    return null;
                }
            }
            catch (RpcException)
            {
                return null;
            }
        }

        public async ValueTask<T> GetOneByQueryAsync(Query query, CallContext context = default)
        {
            try
            {
                var qry = BsonDocument.Parse(query.Filter);
 
                return await _repo.FindOneByQueryAsync(qry);

            }
            catch (RpcException)
            {
                return null;
            }
        }

        public async ValueTask<IEnumerable<T>> GetManyByQueryAsync(Query query, CallContext context = default)
        {
            try
            {    
                var filter = BsonDocument.Parse(query.Filter);
            
                return await _repo.FindManyByQueryAsync(filter);
            }
            catch (RpcException)
            {
                return null;
            }
        }
    }
}