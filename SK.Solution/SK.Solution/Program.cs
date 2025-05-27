using Microsoft.AspNetCore.Identity;
using Radzen;
using SK.CRM.UI.Blazor;
using SK.Inventory.UI.Blazor;
using SK.Identity.UI.Blazor;
using SK.Visit.UI.Blazor;
using SK.Solution.Components;
using Stripe;
using System.Reflection;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using SK.Inventory.Application.MappingProfiles;
using SK.CRM.Application.MappingProfiles;
using SK.Visit.Application.MappingProfiles;
using Serilog;
using SK.Solution.Shared.Interfaces.GeoLocalisation;
using SK.Solution.Utility.Services;
using SK.Identity.Application.MappingProfiles;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
#region Module

builder.Services.AddCrmModuleServices(builder.Configuration);  // Register services from the Customers module
builder.Services.AddInventoryModuleServices(builder.Configuration);  // Register services from the Inventory_Viewer module
builder.Services.AddIdentityModuleServices(builder.Configuration);  // Register services from the Identity module
builder.Services.AddNoteModuleServices(builder.Configuration);  // Register services from the Note_Viewer module
builder.Services.AddVisitModuleServices(builder.Configuration);  // Register services from the Visit_Viewer module
builder.Services.AddSingleton<JsonFileService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ICityService, CityService>();
#endregion
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddRadzenComponents();
builder.Services.AddCascadingAuthenticationState();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()
    , typeof(SK.CRM.Application.Features.Customers.Queries.GetAllCustomersQuery).Assembly
    , typeof(SK.Inventory.Application.Features.Products.Queries.GetAllProductsQuery).Assembly
    , typeof(SK.Inventory.Application.Features.Categories.Queries.GetAllCategoriesQuery).Assembly
    , typeof(SK.Identity.Application.Features.Users.Commands.CreateUserCommand).Assembly
     , typeof(SK.Visit.Application.Features.Visit.Schedule.Queries.GetAllVisitPlanningStartingFromTomorrowQuery).Assembly
    ));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(
    typeof(InventoryMappingProfile).Assembly,
    typeof(CrmMappingProfile).Assembly,
     typeof(IdentityMappingProfile).Assembly,
    typeof(VisitMappingProfile).Assembly
);
var keyVaultUriString = builder.Configuration["KeyVault:Uri"];
var keyVaultUri = new Uri(keyVaultUriString);
var secretClient = new SecretClient(keyVaultUri, new DefaultAzureCredential());

string microsoftSecret = secretClient.GetSecret("MicrosoftClientSecret").Value.Value;
string googleSecret = secretClient.GetSecret("GoogleClientSecret").Value.Value;

// --- Authentification externe ---
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
.AddMicrosoftAccount(microsoftOptions =>
{
    microsoftOptions.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
    microsoftOptions.ClientSecret = microsoftSecret;
})
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = googleSecret;
})
.AddIdentityCookies();
builder.Services.AddAuthorization();



builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllers();
builder.Services.AddLocalization();

// Culture par défaut
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-TN"),
                    new CultureInfo("fr")
                };
    options.DefaultRequestCulture = new RequestCulture("fr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();
StripeConfiguration.ApiKey = secretClient.GetSecret("StripeApiKey").Value.Value;
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SK.Solution.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SK.CRM.UI.Blazor._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SK.Note.UI.Blazor._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SK.Visit.UI.Blazor._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SK.Identity.UI.Blazor._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SK.Inventory.UI.Blazor._Imports).Assembly);
app.MapFallback(() => Results.Redirect("/not-found"));
// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
