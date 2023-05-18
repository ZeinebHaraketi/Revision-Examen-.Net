using AM.ApplicationCore.Interfaces;
using AM.Infrastructure;
using Examen.ApplicationCore.Interfaces;
using Examen.ApplicationCore.Services;
using Examen.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DbContext, ExamenContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<Type>(t => typeof(GenericRepository<>));
builder.Services.AddScoped<IServiceCourse,ServiceCourse>();
builder.Services.AddScoped<IServiceVoiture,ServiceVoiture>();
builder.Services.AddScoped<IServiceClient,ServiceClient>();
builder.Services.AddScoped<IServiceChauffeur,ServiceChauffeur>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
