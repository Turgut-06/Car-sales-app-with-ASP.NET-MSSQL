using OtoServisSatis.Data;
using OtoServisSatis.Service.Abstract;
using OtoServisSatis.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataBaseContext>(); // bunu da web ui katmanýna tanýtmamýz gerekiyor

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); //kendi yazdýðým servisi programa tanýtýyorum AddTransient yapýsý ile

builder.Services.AddTransient<ICarService,CarService>(); //ICarService talebi gelirse CarService ver demek
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => {
    x.LoginPath = "/Admin/Login";
    x.AccessDeniedPath = "/AccessDenied";
    x.LogoutPath = "/Admin/Logout";
    x.Cookie.Name = "Admin";
    x.Cookie.MaxAge = TimeSpan.FromDays(7); //oturum açýldýktan sonra 7 gün açýk duracak
    x.Cookie.IsEssential = true;

});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim("Role", "Admin")); // bu policy isimlerini verdiðim controller a sadece bu policy i içeren rollerdekiler eriþecek
	x.AddPolicy("UserPolicy", policy => policy.RequireClaim("Role", "User"));
});

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

app.UseAuthentication(); // Authorization dan önce gelmesi lazým yoksa sisteme giriþ yapmana izin vermez
app.UseAuthorization();

app.MapControllerRoute(
           name: "Admin",
           pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
