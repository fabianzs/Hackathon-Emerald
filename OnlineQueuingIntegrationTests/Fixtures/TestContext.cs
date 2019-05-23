using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OnlineQueuing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace OnlineQueuingIntegrationTests.Fixtures
{
    class TestContext : IDisposable
    {
        private TestServer server;
        public HttpClient Client { get; set; }
        public readonly ApplicationContext context;

        public TestContext()
        {
            var builder = new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>()
                .UseConfiguration(new ConfigurationBuilder()
                .AddJsonFile("appsettings.Testing.json").Build());

            server = new TestServer(builder);

            context = server
                .Host
                .Services
                .GetService(typeof(ApplicationContext)) as ApplicationContext;

            Client = server.CreateClient();
        }

        public void Dispose()
        {
            server.Dispose();
            Client.Dispose();
        }
    }
}
