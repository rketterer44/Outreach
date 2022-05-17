// See https://aka.ms/new-console-template for more information

using OutreachCli;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

Console.WriteLine("Starting...");


var url = "https://api.outreach.io/oauth/authorize?client_id=5WohDIKnAUXmeiGFYAyhfcwQ-L3Gg8VrF8zHUpufP_Q&redirect_uri=https://localhost:51903/api/v1/token/callback&response_type=code&scope=accounts.all";
OpenUrl(url);
await OAuthAsync();

//string name = Environment.GetCommandLineArgs()[1];
//Console.WriteLine($"Hello, {name}!");

async Task OAuthAsync()
{

    //var config = await ServiceConfig.LoadAsync();

    string code = null;

    using (var listener = new OAuthListener())
    {
        await listener.StartAsync();
        Process autoauth = null;
        try
        {
            //autoauth = Process.Start(AutoAuthPath, $"{config.ClientId} {username} {password}");
            code = await listener.WaitForOAuthCode();


            var url = $"https://api.outreach.io/oauth/token?client_id=5WohDIKnAUXmeiGFYAyhfcwQ-L3Gg8VrF8zHUpufP_Q&client_secret=27HPjvWlniCJRsmZy8m9blNCU5GkfmJBYltz_u62Ipo&redirect_uri=https://localhost:51903/api/v1/token/callback&grant_type=authorization_code&code={code}";
            using var client = new HttpClient();
            var msg = new HttpRequestMessage(HttpMethod.Post, url);
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            //Convert.ToBase64String(code));

            var response = await client.SendAsync(msg);

            var token = await response.Content.ReadAsStringAsync();
            //string jsonString = JsonSerializer.Deserialize<WeatherForecast>(token);
            Console.WriteLine(token);
            await listener.StopAsync();
        }
        finally
        {
            if (!(autoauth?.HasExited ?? true))
                autoauth?.Kill();
        }
    }

    //var client = await CreateClientAsync();
    //await client.AuthenticateAsync(token, new Uri(config.RedirectUri));
    //SaveContext(username, password, client);
}

void OpenUrl(string url)
    {

    var ps = new ProcessStartInfo(url)
    {
        UseShellExecute = true,
        Verb = "open", 
        CreateNoWindow = false
    };
    Process.Start(ps);
}







