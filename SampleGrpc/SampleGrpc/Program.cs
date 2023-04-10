// See https://aka.ms/new-console-template for more information
using Grpc.Core;

// create GreeterImplementation object providing the 
// RPC SayHello implementation
Server server = new Server
{
    //Services = { Greeter.BindService(greeterImplementation) }
};

server.Ports.Add(new ServerPort("localhost", 5555, ServerCredentials.Insecure));

server.Start();

Console.ReadLine();

server.ShutdownAsync().Wait();

Console.WriteLine("Hello, World!");
