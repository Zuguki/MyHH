using FindWork.BL.Auth;
using FindWork.BL.Email;
using FindWork.BL.General;
using FindWork.BL.Profile;
using FindWork.DAL;
using FindWork.DAL.Email;
using FindWork.DAL.Profile;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IAuthDAL, AuthDAL>();
builder.Services.AddSingleton<IUserTokenDAL, UserTokenDAL>();
builder.Services.AddSingleton<IDbSessionDAL, DbSessionDAL>();
builder.Services.AddSingleton<IProfileDAL, ProfileDAL>();
builder.Services.AddSingleton<IEmailQueueDAL, EmailQueueDAL>();
builder.Services.AddSingleton<IUserSecurityDAL, UserSecurityDAL>();

builder.Services.AddScoped<IAuth, Auth>();
builder.Services.AddSingleton<IEncrypt, Encrypt>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IDbSession, DbSession>();
builder.Services.AddSingleton<IWebCookie, WebCookie>();
builder.Services.AddSingleton<IProfile, Profile>();
builder.Services.AddSingleton<IEmailQueue, EmailQueue>();
builder.Services.AddSingleton<IUserSecurity, UserSecurity>();


if (builder.Environment.IsDevelopment())
    builder.Services.AddSingleton<ICaptcha, DevCaptcha>();
else
    builder.Services.AddSingleton<ICaptcha>(provider => new GoogleCaptcha(
        builder.Configuration["Captcha:SiteKey"],
        builder.Configuration["Captcha:SecretKey"]));

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