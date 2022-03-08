using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Azure;
using Weather.API;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

var vaultUri = Environment.GetEnvironmentVariable("VaultUri");

if (!string.IsNullOrEmpty(vaultUri))
{
    var keyVaultEndpoint = new Uri(vaultUri);
    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// all of these are from app settings
builder.Services.AddSingleton<CityDataService>(new CityDataService(
    builder.Configuration["ConnectionStrings:Cosmos"], 
    builder.Configuration["Cosmos:DatabaseName"], 
    builder.Configuration["Cosmos:CollectionId"])
);

builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["azureite:blob"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["azureite:queue"], preferMsi: true);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
