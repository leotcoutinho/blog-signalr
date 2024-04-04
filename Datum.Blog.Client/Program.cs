
using Datum.Blog.Client;
using System.Text;

var HubConnectionTask = HubClient.GetHubConnection();
var GetPostsTask = GetPostsAsync();

await Task.WhenAll(HubConnectionTask, GetPostsTask).ConfigureAwait(false);

async Task GetPostsAsync()
{
    string apiUrl = "http://localhost:5119/api/posts/getAll";

    using var httpClient = new HttpClient();

    try
    {
        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"{json}");
           
        }
        else
        {
            Console.WriteLine($"Erro ao fazer a solicitação. Status: {response.StatusCode}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao fazer a solicitação: {ex.Message}");
    }
}

