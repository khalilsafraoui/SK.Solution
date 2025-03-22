using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using SK.Customer.UI.Blazor;
using SK.Inventory.UI.Blazor;
using SK.Solution.Components;
using SK.Solution.Components.Account;
using SK.Solution.Data;
using SK.Solution.Repository;
using SK.Solution.Repository.IRepository;
using SK.Solution.Services;
using Stripe;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddRadzenComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteCategoryRepository, NoteCategoryRepository>();
builder.Services.AddSingleton<SharedStateService>();
builder.Services.AddScoped<PaymentService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()
    , typeof(SK.Customer.Application.Features.Customers.Queries.GetAllCustomersQuery).Assembly
    , typeof(SK.Inventory.Application.Features.Products.Queries.GetAllProductsQuery).Assembly
    , typeof(SK.Inventory.Application.Features.Categories.Queries.GetAllCategoriesQuery).Assembly
    ));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#region Module

builder.Services.AddCustomerModuleServices(builder.Configuration);  // Register services from the Customers module
builder.Services.AddInventoryModuleServices(builder.Configuration);  // Register services from the Inventory module

#endregion

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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);



builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

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
    .AddAdditionalAssemblies(typeof(SK.Customer.UI.Blazor._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SK.Inventory.UI.Blazor._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
