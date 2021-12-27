using System.ServiceModel;
using System.Threading.Tasks;
using TestGrpc.Models;
using ProtoBuf.Grpc;

namespace TestGrpc.Contracts
{
    [ServiceContract]
    public interface ITodoService : IBaseService<Todo>
    {
        // FIND
        /// <summary>
        ///  
        /// </summary>
        /// <param name="title"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [OperationContract]
        ValueTask<Todo> GetOneByTitleAsync(string title,
            CallContext context = default);
    }
}