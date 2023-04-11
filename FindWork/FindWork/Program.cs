using FindWork.BL.Auth;
using FindWork.BL.General;
using FindWork.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddSingleton<IAuthDAL, AuthDAL>();
builder.Services.AddSingleton<IDbSessionDAL, DbSessionDAL>();
builder.Services.AddScoped<IDbSession, DbSession>();
builder.Services.AddSingleton<IUserTokenDAL, UserTokenDAL>();
builder.Services.AddSingleton<IWebCookie, WebCookie>();

builder.Services.AddMvc();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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