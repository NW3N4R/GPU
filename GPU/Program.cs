using GPU;
using GPU.Helpers;
using GPU.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication("CookieAuth")
       .AddCookie("CookieAuth", options =>
       {
           options.LoginPath = "/Account/Login";
           options.LogoutPath = "/Account/Logout";

           options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

           options.SlidingExpiration = false;

       });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireStuList", policy => policy.RequireClaim("Permission", "StuList"));
    options.AddPolicy("RequireArchList", policy => policy.RequireClaim("Permission", "ArchList"));
});

builder.Services.AddHttpContextAccessor();
// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddHostedService<TimedHostedService>();


// Add session support if needed
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMicroseconds(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Error/403";
});
var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
DbConnectionHelper db = new DbConnectionHelper();
await db.OpenConnection();
await Task.WhenAll(
    ArchiveService.Insert(),
    StudentServices.Insert(),
    WebPropsServices.LoadProps(),
    ManagerServices.LoadManagers());

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.Run();
