using GPU.Helpers;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseStatusCodePages(async context =>
{
    //await Task.Delay(500);
    if (context.HttpContext.Response.StatusCode == 404)
    {
        context.HttpContext.Response.Redirect("/Students/NotFound404");
    }
});
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Students/Error");
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
await DbConnectionHelper.OpenConnection();

await DbConnectionHelper.LoadAll();
app.Run();
