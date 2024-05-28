
using System.Net.Http.Json;

HttpClient client = new HttpClient();

//client.BaseAddress = new Uri("");

HttpResponseMessage? response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "http://localhost:63412/first"));

if (response is not null && response.StatusCode == System.Net.HttpStatusCode.OK)
{
    string responseMessage = await response.Content.ReadAsStringAsync();

    Console.WriteLine(responseMessage);
    var json = await response.Content.ReadFromJsonAsync<string>();
    Console.WriteLine(json);

}

if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
{

}
