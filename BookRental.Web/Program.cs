using System.Reflection;
using Application;
using FluentValidation;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddWebAppServices(builder.Configuration);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
var app = builder.Build();

app.UseRequestLocalization(options => {
    var supportedCultures = new[] { "en", "ro", "ru" };
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
    
    options.RequestCultureProviders.Clear();
    options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
    options.RequestCultureProviders.Add(new AcceptLanguageHeaderRequestCultureProvider());
});
app.Use(async (context, next) =>
{
    var culture = System.Globalization.CultureInfo.CurrentUICulture.Clone() as System.Globalization.CultureInfo;
    culture.NumberFormat = System.Globalization.CultureInfo.InvariantCulture.NumberFormat;
    
    System.Globalization.CultureInfo.CurrentCulture = culture;
    await next();
});
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();