using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using TestGrpc.Contracts;
using TestGrpc.Models;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Meta;

namespace GrpcClient
{
    class Program
    {
        private static async Task Main()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");

            try
            {
                // MONGO INTERACTION TESTS
                // 1) INSERT
                var client = channel.CreateGrpcService<ITodoService>();

                RuntimeTypeModel.Default[typeof(Base)].AddSubType(42, typeof(Todo));


                var reply = await client.CreateOneAsync(
                    new Todo
                    {
                        Title = "Some Title"
                    }
                );

                Console.WriteLine($"Insert: {reply.Message}");

                // // -------------- Uncomment after an insert
                // // 2) FIND BY ID
                // // replace the id bellow for one that exists in the database
                // var id = "61c8a4fc376511725a4f6290"; // make sure it exists
                // var res = await client.GetByIdAsync(id);

                // Console.WriteLine(value: $"GetById Message: {res.ToJson()}");

                // // --------------
                // // 3) FIND ONE BY TITLE
                // var title = "some title"; // lowercase
                // res = await client.GetOneByTitleAsync(title); // is not case sensitive

                // Console.WriteLine(value: $"GetOneByTitle Message: {res.ToJson()}");

                // // --------------
                // // 4) FIND ONE BY QUERY FILTER REQUEST
                // var serializerRegistry = BsonSerializer.SerializerRegistry;
                // var documentSerializer = serializerRegistry.GetSerializer<Todo>();

                // // find a todo by its title
                // var filter = Builders<Todo>.Filter.Eq(e => e.Title, "Some Title");
                // var filterString = filter.Render(documentSerializer, serializerRegistry);

                // string x = filterString.ToString();
                // res = await client.GetOneByQueryAsync(new Query {Filter = x});

                // Console.WriteLine($"GetOneByQueryAsync Message: {res.ToJson()}");

                // // many
                // var res2 = await client.GetManyByQueryAsync(new Query {Filter = x});

                // Console.WriteLine($"GetOneByQueryAsync Message: {res2.ToJson()}");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                await channel.ShutdownAsync();
            }
        }
    }
}
