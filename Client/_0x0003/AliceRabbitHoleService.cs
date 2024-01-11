using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

#line hidden
namespace _.__._0x0003
{
    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    internal class AliceRabbitHoleService : BackgroundService
    {
        private readonly _0x0004.AliceRabbitHole _aliceRabbitHole;

        internal AliceRabbitHoleService(_0x0004.AliceRabbitHole aliceRabbitHole)
        {
            _aliceRabbitHole = aliceRabbitHole ?? throw new ArgumentNullException(nameof(aliceRabbitHole));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _aliceRabbitHole.Descend(43215);
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Adjust the delay as needed
            }
        }

        internal static void Main()
        {
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<AliceRabbitHoleService>();
                    services.AddSingleton<_0x0004.AliceRabbitHole>();
                })
                .Build();

            host.Run();
        }
    }
}
#line default