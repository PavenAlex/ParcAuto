using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient();
builder.Services.AddHttpClient<VehicleService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7280/");
});
builder.Services.AddHttpClient<ClientService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7280/");
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
   policy.RequireRole("Admin"));
});


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Mentenantas", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Clients", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Rezervares", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Facturas", "AdminPolicy");
    options.Conventions.AuthorizeFolder("/Vehicles", "AdminPolicy");
    options.Conventions.AllowAnonymousToPage("/Vehicles/Index");
    options.Conventions.AllowAnonymousToPage("/Vehicles/Details");








});

builder.Services.AddDbContext<WebContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("WebContext")
        ?? throw new InvalidOperationException("Connection string 'WebContext' not found.")));

// Configure Identity for ParcAutoIdentityContext
builder.Services.AddDbContext<ParcAutoIdentityContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ParcAutoIdentityContextConnection")
        ?? throw new InvalidOperationException("Connection string 'ParcAutoIdentityContextConnection' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ParcAutoIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
