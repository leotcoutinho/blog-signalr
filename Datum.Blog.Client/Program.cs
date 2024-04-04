
using Datum.Blog.Client;
using Newtonsoft.Json;

Console.WriteLine("===========================");
Console.WriteLine("=====Bem Vindo ao Blog=====");
Console.WriteLine("===========================");

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

            var listaPosts = JsonConvert.DeserializeObject<List<Post>>(json);

            if (listaPosts != null)
            {
                foreach (Post post in listaPosts.ToList().OrderBy(x => x.DataCadastro))
                {
                    Console.WriteLine($"{post.Nome.ToUpper()} : {post.Comentario}");
                    Console.WriteLine("--------------------------------------------------------");
                }
            }
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

