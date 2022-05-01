// See https://aka.ms/new-console-template for more information

using OutreachCli;
using System.Diagnostics;
using System.Net;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();

//var app = builder.Build();
//app.UseHttpsRedirection();
//app.MapControllers();
//app.Run();s


var url = "https://api.outreach.io/oauth/authorize?client_id=5WohDIKnAUXmeiGFYAyhfcwQ-L3Gg8VrF8zHUpufP_Q&redirect_uri=https://www.bombora.com/oauth/outreach&response_type=code&scope=accounts.all";
Console.WriteLine("Starting...");
OpenUrl(url);

//string name = Environment.GetCommandLineArgs()[1];
//Console.WriteLine($"Hello, {name}!");
await OAuthAsync();

async Task OAuthAsync()
{

    //var config = await ServiceConfig.LoadAsync();

    string token = null;

    using (var listener = new OAuthListener())
    {
        await listener.StartAsync();
        Process autoauth = null;
        try
        {
            //autoauth = Process.Start(AutoAuthPath, $"{config.ClientId} {username} {password}");
            token = await listener.WaitForOAuthCode();
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
        Verb = "open"
    };
    Process.Start(ps);
    //var prs = new ProcessStartInfo("iexplore.exe");
    //prs.Arguments = url;
    //Process.Start(prs);
    
}






