using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GraphAPIDemo {
    class graphClient {
        static async Task Main(string[] args) {
            // Build a client application.

            var appConfig = GetParameters();
            var scopes = appConfig["scopes"].Split(';');
            var appId = appConfig["appid"];

            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
            .Create(appId)
            .Build();

            // Create an authentication provider by passing in a client application and graph scopes.
            DeviceCodeProvider authProvider = new DeviceCodeProvider(publicClientApplication, scopes);
            // Create a new instance of GraphServiceClient with the authentication provider.
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            
            var contacts = await graphClient.Me.Contacts.Request().Top(10).GetAsync();
            Console.Write("Contacts--");
            var contactIterator = PageIterator<Contact>
                .CreatePageIterator(graphClient, contacts, (m) => {
                    Console.WriteLine($"- {m.DisplayName.ToString()}");
                    return true;
                });

            await contactIterator.IterateAsync();


            Console.Write("Groups--");
            var groups = await graphClient.Groups.Request().Top(10).GetAsync();
            var groupIterator = PageIterator<Group>
                .CreatePageIterator(graphClient, groups, (m) => {
                    Console.WriteLine($"- {m.ToString()}");
                    return true;
                });

            await groupIterator.IterateAsync();


            Console.Write("Messages--");
            var messages = await graphClient.Me.Messages.Request().Select(e => new {
                    e.Sender,
                    e.Subject
                }).Top(10).GetAsync();

            var msgIterator = PageIterator<Message>
                .CreatePageIterator(graphClient, messages, (m) => {
                    Console.WriteLine($"- {m.Subject}");
                    return true;
                });

            await msgIterator.IterateAsync();
        }


        private static Dictionary<string, string> GetParameters() {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var val1 = builder.Build().GetSection("appid").Value;
            var val2 = builder.Build().GetSection("scopes").Value;

            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("appid", val1);
            config.Add("scopes", val2);
            return config;
        }
    }
}
