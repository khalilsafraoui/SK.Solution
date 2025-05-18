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

var builder = WebApplication.CreateBuilder(args);
#region Module

builder.Services.AddCrmModuleServices(builder.Configuration);  // Register services from the Customers module
builder.Services.AddInventoryModuleServices(builder.Configuration);  // Register services from the Inventory module
builder.Services.AddIdentityModuleServices(builder.Configuration);  // Register services from the Identity module
builder.Services.AddNoteModuleServices(builder.Configuration);  // Register services from the Note module
builder.Services.AddVisitModuleServices(builder.Configuration);  // Register services from the Visit module
#endregion
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddRadzenComponents();
builder.Services.AddCascadingAuthenticationState();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()
    , typeof(SK.CRM.Application.Features.Customers.Queries.GetAllCustomersQuery).Assembly
    , typeof(SK.Inventory.Application.Features.Products.Queries.GetAllProductsQuery).Assembly
    , typeof(SK.Inventory.Application.Features.Categories.Queries.GetAllCategoriesQuery).Assembly
     , typeof(SK.Visit.Application.Features.Visit.Schedule.Queries.GetAllVisitPlanningStartingFromTomorrowQuery).Assembly
    ));
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(
    typeof(InventoryMappingProfile).Assembly,
    typeof(CrmMappingProfile).Assembly,
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
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SK.Solution.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SK.CRM.UI.Blazor._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SK.Note.UI.Blazor._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SK.Visit.UI.Blazor._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SK.Inventory.UI.Blazor._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
