using System.Threading.Tasks;
using MongoDB.Driver;
using GrpcServer.Database;
using TestGrpc.Models;

namespace GrpcServer.Repositories
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(IMongoDBContext context)
            :base(context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async ValueTask<Todo> FindTodoByTitleAsync(string title)
        {
            var filter = Builders<Todo>.Filter.Regex(e => e.Title, $"/.*{title}.*/i");
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }

    public interface ITodoRepository : IBaseRepository<Todo>
    {
        ValueTask<Todo> FindTodoByTitleAsync(string title);
    }
}