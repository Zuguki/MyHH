using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<FindWork.BL.Auth.IAuthBL, FindWork.BL.Auth.AuthBL>();
builder.Services.AddSingleton<FindWork.BL.Auth.IEncrypt, FindWork.BL.Auth.Encrypt>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<FindWork.BL.Auth.ICurrentUser, FindWork.BL.Auth.CurrentUser>();
builder.Services.AddSingleton<FindWork.DAL.IAuthDAL, FindWork.DAL.AuthDal>();

builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();