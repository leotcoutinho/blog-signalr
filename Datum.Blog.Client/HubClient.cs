using Microsoft.AspNetCore.SignalR.Client;

namespace Datum.Blog.Client
{
    public static class HubClient
    {
        const string url = "http://localhost:5119/bloghub";

        public static async Task GetHubConnection()
        {
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

                if (hubConnection != null)
                {
                    await hubConnection.StartAsync();

                    Console.WriteLine("===========================");
                    Console.WriteLine("    conectado no hub!"); 
                    Console.WriteLine("===========================");
                }

                // buscar os posts do blog e listar aqui
                Console.WriteLine();
                Console.ReadLine();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
