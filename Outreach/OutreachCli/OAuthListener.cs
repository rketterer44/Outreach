using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutreachCli
{
    class OAuthListener : IDisposable
    {
        private IWebHost _host;


        public async Task<string> WaitForOAuthCode()
        {
            var delay = TimeSpan.FromMilliseconds(300);
            while (AuthController.Code is null)
                await Task.Delay(delay);

            var ret = AuthController.Code;

            return ret;
        }

        public bool IsStarted { get; private set; }
        public async Task StartAsync()
        {

            if (IsStarted)
                throw new InvalidOperationException();

            AuthController.Code = null;

            _host = WebHost
                .CreateDefaultBuilder()
                //.ConfigureLogging(x => x.ClearProviders())
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("https://localhost:51903/")
                .Build();

            await _host.StartAsync();

            IsStarted = true;
        }

        public Task StopAsync() => _host.StopAsync();
        public void Dispose()
        {
            if (IsStarted)
                StopAsync();
        }
    }

    
}
