using System;
using System.Net;

class Program
{
    static async Task Main()
    {
        try
        {
            string externalIp = await GetExternalIp().ConfigureAwait(false);
            Console.WriteLine($"My IP Address: {externalIp}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.ReadLine();
    }

    static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };

    static async Task<string> GetExternalIp()
    {
        string apiUrl = "http://api.ipify.org/?format=text";

        HttpResponseMessage response = await httpClient.GetAsync(apiUrl).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        else
        {
            throw new HttpRequestException($"Failed to retrieve IP address. Status code: {response.StatusCode}");
        }
    }
}


