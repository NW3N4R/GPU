using GPU.Helpers;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GPU.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<GPUContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GPUContext") ?? throw new InvalidOperationException("Connection string 'GPUContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/PersonalStudents/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PersonalStudents}/{action=Index}/{id?}");
await DbConnectionHelper.OpenConnection();
app.Run();
