# GrpcTest

<p>A simple project where a client can run <b>CRUD</b> functions on a todo service using <b>gRPC</b> protocol.</p>
Built with: 

<ul>
    <li><b>.Net 6.0</b> gRPC Service - client & server</li>
    <li><i>protobuf-net.Grpc.AspNetCore</i> - Code-first approach isntead of <b>.proto</b> files</li>
    <li><b>MongoDB</b> - local or an Atlas Cluster</li>
</ul>

<p>Also, this project makes full use of <i>Generics</i>, <i>Inheritance</i> and <i>Dependency injection</i>.</p>

## Setup


1. MongoDB setup:
    <code><i>GrpcServer/appsettings.json</i></code>

    ```json
    "MongoDB": {
    "connectionString": "mongodb://localhost:27017",
        "database": "test",
        "collections": {
            "Todo": "todos"
        }
    }
    ```
    Run an instance of MongoDB locally or add a connection to an Atlas Cluster.
2.
    In <code><i>GrpcClient/Program.cs</i></code> make sure to insert at least one
        todo and then make query calls to the server. Because is hard coded, uncomment and comment bloc of code if needed.

3.  Run an instance of the server <code>dotnet run --project ./GrpcServer/GrpcServer.csproj</code> then run the client <code>dotnet run --project ./GrpcClient/GrpcClient.csproj</code>

## Comments

In case you have to share the .proto file to other than services built with .Net, the <code><i>protobuf-net.Grpc.AspNetCore</i></code> package can't generate the .proto file for this project using generics and inheritance structure.

## License
[MIT](https://choosealicense.com/licenses/mit/)