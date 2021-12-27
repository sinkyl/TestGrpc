using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using TestGrpc.Models;
using ProtoBuf.Grpc;
using System.Collections.Generic;

namespace TestGrpc.Contracts
{
    [DataContract]
    public class RStatus
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }

    [ServiceContract]
    public interface IBaseService<T>
    {
        // -------- CREATE --------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [OperationContract]
        Task<RStatus> CreateOneAsync(T request,
            CallContext context = default);

        // -------- FIND --------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [OperationContract]
        ValueTask<T> GetByIdAsync(string id,
            CallContext context = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [OperationContract]
        ValueTask<T> GetOneByQueryAsync(Query query,
            CallContext context = default);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [OperationContract]
        ValueTask<IEnumerable<T>> GetManyByQueryAsync(Query query,
            CallContext context = default);
    }
}
