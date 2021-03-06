using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using NoSearch.Common;
using NoSearch.Data.DataAccess;
using NoSearch.Data.Resources;
using WebSiteMeta.Scraper;
using WebSiteMeta.Scraper.HttpClientWrapper;

var builder = WebApplication.CreateBuilder(args);

/*
if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential());
}
*/

// Add services to the container.
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllersWithViews();// options =>
builder.Services.AddRazorPages()
    .AddMicrosoftIdentityUI();

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.Scan(scan => scan
    .FromCallingAssembly()
        .AddClasses(true)
            .AsMatchingInterface()
            .WithScopedLifetime()
    .FromAssemblyOf<IResourceDataAccess>()
        .AddClasses(true)
            .AsMatchingInterface()
            .WithScopedLifetime()
    .FromAssemblyOf<IDateTimeHelper>()
        .AddClasses(true)
            .AsMatchingInterface()
            .WithSingletonLifetime());

builder.Services.AddScoped<IFindMetaData, FindMetaData>(a =>
{
    var factory = a.GetService<IHttpClientFactory>();
    var client = factory.CreateClient();
    var wrapper = new DefaultHttpClientWrapper(client);
    return new FindMetaData(wrapper);
});

builder.Services.ConfigureServices(
    builder.Configuration.GetConnectionString("SqlConnectionString"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UpdateDatabase();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


public partial class Program { } // so you can reference it from tests