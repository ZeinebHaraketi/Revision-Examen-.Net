using Examen.ApplicationCore.Interfaces;
using Examen.ApplicationCore.Services;
using Examen.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Injection de dépendance
builder.Services.AddDbContext<DbContext, ExamenContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<Type>(t => typeof(GenericRepository<>));
builder.Services.AddScoped<IPretLivreService, PretLivreService>();
builder.Services.AddScoped<IAbonneService, AbonneService>();
builder.Services.AddScoped<ILivreService, LivreService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
