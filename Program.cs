using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Web_final.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
AddCookie(options =>
{
    options.Cookie.Name = "GiovanniCookie";
    options.LoginPath = "/User/Login";
});
builder.Services.AddSession();
var connectionString =
builder.Configuration.GetConnectionString("WebsiteNuocHoaConnection");
builder.Services.AddDbContext<WebsiteNuocHoaContext>(options => options.UseSqlServer(connectionString));

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
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
    name: "trang-chu",
    pattern: "trang-chu",
    defaults: new { controller = "Home", action = "Index" });
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
     name: "san-pham",
     pattern: "san-pham",
     defaults: new { controller = "Product", action = "Index" });
    
    endpoints.MapControllerRoute(
    name: "bai-viet",
    pattern: "bai-viet",
    defaults: new { controller = "Blog", action = "Index" });
    endpoints.MapControllerRoute(
    name: "phan-hoi-khach-hang",
    pattern: "phan-hoi-khach-hang",
    defaults: new { controller = "PH", action = "Index" });
    
    endpoints.MapControllerRoute(
    name: "lien-he",
    pattern: "lien-he",
    defaults: new { controller = "LH", action = "Index" });
    endpoints.MapControllerRoute(
     name: "tim-kiem",
     pattern: "tim-kiem",
     defaults: new { controller = "Product", action = "FindProduct" });
    endpoints.MapControllerRoute(
    name: "dang-ky",
    pattern: "dang-ky",
    defaults: new { controller = "User", action = "Register" });
    endpoints.MapControllerRoute(
    name: "dang-nhap",
     pattern: "dang-nhap",
    defaults: new { controller = "User", action = "Login" });
    endpoints.MapControllerRoute(
    name: "dang-xuat",
    pattern: "dang-xuat",
    defaults: new { controller = "User", action = "Logout" });
    endpoints.MapControllerRoute(
     name: "gio-hang",
     pattern: "gio-hang",
     defaults: new { controller = "Cart", action = "Index" });
    endpoints.MapControllerRoute(
 name: "them-gio-hang",
 pattern: "them-gio-hang",
 defaults: new { controller = "Cart", action = "AddItem" });
    endpoints.MapControllerRoute(
 name: "thanh-toan",
 pattern: "thanh-toan",
 defaults: new { controller = "Cart", action = "Payment" });
    endpoints.MapControllerRoute(
 name: "hoan-thanh",
 pattern: "hoan-thanh",
 defaults: new { controller = "Cart", action = "Success" });
    endpoints.MapControllerRoute(
name: "thong-tin",
pattern: "thong-tin",
defaults: new { controller = "User", action = "Info" });
    endpoints.MapControllerRoute(
 name: "chinh-sua-thong-tin",
 pattern: "chinh-sua-thong-tin",
 defaults: new { controller = "User", action = "EditInfo" });
    endpoints.MapControllerRoute(
name: "chinh-sua-mat-khau",
pattern: "chinh-sua-mat-khau",
defaults: new { controller = "User", action = "ChangePassword" });
    endpoints.MapControllerRoute(
        name: "the-loai-san-pham",
        pattern: "{slug}-{id}",
        defaults: new { controller = "Product", action = "CateProd" });
    endpoints.MapControllerRoute(
    name: "chi-tiet-san-pham",
 pattern: "san-pham/{slug}-{id}",
 defaults: new { controller = "Product", action = "ProdDetail" });
    endpoints.MapControllerRoute(
    name: "chi-tiet-bai-viet",
 pattern: "bai-viet/{slug}-{id}",
 defaults: new { controller = "Blog", action = "BlogDetail" });
    endpoints.MapControllerRoute(
    name: "gui-phan-hoi",
    pattern: "gui-phan-hoi",
    defaults: new { controller = "PH", action = "SendPH" });
    endpoints.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=HomeAdmin}/{action=Index}/{id?}"
    );
});

app.Run();
