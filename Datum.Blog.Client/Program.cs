using Microsoft.AspNetCore.SignalR.Client;

const string url = "http://localhost:5119/bloghub";

await using var hubConnection = new HubConnectionBuilder()
                                  .WithUrl(url)
                                  .WithAutomaticReconnect()
                                  .Build();

try
{
    hubConnection.On<string>("ReceiveMessage", message =>
   {
       Console.WriteLine(message);
   });

    await hubConnection.StartAsync();

    Console.WriteLine("conectado ao servidor do blog");
    Console.WriteLine("================================");
    // buscar os posts do blog e listar aqui
    Console.WriteLine();
    Console.ReadLine();
}
catch (Exception)
{
    throw;
}