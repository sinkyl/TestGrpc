using TestGrpc.Models;
using GrpcServer.Repositories;
using System.Threading.Tasks;
using ProtoBuf.Grpc;
using TestGrpc.Contracts;
using Grpc.Core;

namespace GrpcServer.Services
{
    public class TodoService : BaseService<Todo>, ITodoService
    {
        private readonly ITodoRepository _todoRepo;
        public TodoService(IBaseRepository<Todo> repo, ITodoRepository todoRepo) : base(repo)
        {
            _todoRepo = todoRepo;
        }

        public async ValueTask<Todo> GetOneByTitleAsync(string title, CallContext context = default)
        {
            try
            {    
                return await _todoRepo.FindTodoByTitleAsync(title);
            }
            catch (RpcException)
            {
                return null;
            }
        }
    }
}