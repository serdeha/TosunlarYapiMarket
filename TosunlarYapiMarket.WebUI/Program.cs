using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.FileProviders;
using TosunlarYapiMarket.Core.Extensions;
using TosunlarYapiMarket.Data.Concrete.EntityFramework.Context;
using TosunlarYapiMarket.Entity.Concrete;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser().Build();

    options.Filters.Add(new AuthorizeFilter(policy));

    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "Lütfen kutucuklarý boþ býrakmayýnýz.");
}).AddRazorRuntimeCompilation();
services.AddRazorPages();
services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    // User Username and Email Options
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<TosunlarYapiMarketDbContext>().AddDefaultTokenProviders();

services.AddSqlAndCookie(configuration.GetConnectionString("TosunlarYapiMarketDB"));
services.AddMyServices();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
    RequestPath = "/node_modules"
});
app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();




app.MapControllerRoute(
    name: "defaults",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}",
    defaults: new {controller = "Home", action = "Index"}
);





app.Run();
