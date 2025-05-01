using Microsoft.AspNetCore.Identity;
using Radzen;
using SK.CRM.UI.Blazor;
using SK.Inventory.UI.Blazor;
using SK.Identity.UI.Blazor;
using SK.Solution.Components;
using Stripe;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
#region Module

builder.Services.AddCrmModuleServices(builder.Configuration);  // Register services from the Customers module
builder.Services.AddInventoryModuleServices(builder.Configuration);  // Register services from the Inventory module
builder.Services.AddIdentityModuleServices(builder.Configuration);  // Register services from the Identity module
builder.Services.AddNoteModuleServices(builder.Configuration);  // Register services from the Note module
#endregion
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddRadzenComponents();
builder.Services.AddCascadingAuthenticationState();


//builder.Services.AddScoped<INoteRepository, NoteRepository>();
//builder.Services.AddScoped<INoteCategoryRepository, NoteCategoryRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()
    , typeof(SK.CRM.Application.Features.Customers.Queries.GetAllCustomersQuery).Assembly
    , typeof(SK.Inventory.Application.Features.Products.Queries.GetAllProductsQuery).Assembly
    , typeof(SK.Inventory.Application.Features.Categories.Queries.GetAllCategoriesQuery).Assembly
    ));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddMicrosoftAccount(microsoftOptions =>
    {
        microsoftOptions.ClientId = "8c0470e8-9b70-4bfa-b777-8b5647734245";//uilder.Configuration["Authentication:Microsoft:ClientId"];
        microsoftOptions.ClientSecret = "ajU8Q~z5m0fWTHklvWL6POKhImocWueNZbxC8cTQ"; // builder.Configuration["Authentication:Microsoft:ClientSecret"];
    })
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "1072238982017-3ultbmigjh001uht8o19kla00ga0g7o4.apps.googleusercontent.com";// builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = "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"; // builder.Configuration["Authentication:Google:ClientSecret"];
    })
    .AddIdentityCookies();





builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();
StripeConfiguration.ApiKey = builder.Configuration.GetSection("StripeApiKey").Value;
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
    .AddAdditionalAssemblies(typeof(SK.Inventory.UI.Blazor._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
