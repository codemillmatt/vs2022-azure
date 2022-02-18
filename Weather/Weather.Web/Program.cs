using Azure.Identity;
var builder = WebApplication.CreateBuilder(args);

var vaultUri = Environment.GetEnvironmentVariable("VaultUri");

if (!string.IsNullOrEmpty(vaultUri))
{
    var keyVaultEndpoint = new Uri(vaultUri);
    builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
}

var connectionString = builder.Configuration["AzureAppConfig"];

builder.Host.ConfigureAppConfiguration(app =>
{
    app.AddAzureAppConfiguration(connectionString);
})
.ConfigureServices(services => {  
    services.AddRazorPages();
    services.AddSingleton<Weather.Web.Services.WeatherForecastService>(new Weather.Web.Services.WeatherForecastService(new HttpClient(), builder.Configuration));
});

// Add services to the container.
//builder.Services.AddRazorPages();

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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
