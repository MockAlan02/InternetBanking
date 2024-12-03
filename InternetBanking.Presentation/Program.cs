using System.Reflection;

using InternetBanking.Application;
using InternetBanking.Infrastructure.Identity;
using InternetBanking.Infrastructure.Persistence;
using InternetBanking.Presentation.Impl;
using InternetBanking.Presentation.Interfacces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(12);
    options.Cookie.Name = ".InternetBanking.Session";
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityLayer(builder.Configuration)
                .AddPersistenceLayer(builder.Configuration);

builder.Services.AddApplicationLayer();


builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ILoginStore, SessionLogginStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (IServiceScope scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.SeedIdentity();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Index}/{id?}");

app.Run();
