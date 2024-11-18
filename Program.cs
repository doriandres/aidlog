using AidLog.Components;
using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.FluentUI.AspNetCore.Components;

namespace AidLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton(_ => new CosmosClient("https://aidlog.documents.azure.com:443/", new DefaultAzureCredential(), new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                }
            }));
            builder.Services.AddSingleton(provider => provider.GetRequiredService<CosmosClient>().GetDatabase("aidlog"));
            builder.Services.AddKeyedSingleton("TicketsCosmosContainer", (provider, key) => provider.GetRequiredService<Database>().GetContainer("Tickets"));

            // Add services to the container.
            builder.Services
                .AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddFluentUIComponents();

            var app = builder.Build();            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // preheat service initialization
            app.Services.GetRequiredKeyedService<Container>("TicketsCosmosContainer");

            app.Run();
        }
    }
}
