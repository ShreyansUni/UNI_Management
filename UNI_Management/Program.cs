using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using UNI_Management.Common;
using UNI_Management.Common.Email;
using UNI_Management.Common.Utility;
using UNI_Management.Controllers;
using UNI_Management.Domain.DataContext;
using UNI_Management.Service;
using System.Net;
using UNI_Management.Common.Email;
using UNI_Management.Common.Utility;
using UNI_Management.Common;
using UNI_Management.Domain.DataContext;
using UNI_Management.Service;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetSection("Data")
                                           .GetSection("DefaultConnection")
                                           .GetValue<string>("ConnectionString");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpContextAccessor();
ApplicationDbContext.Initialize(builder.Services.BuildServiceProvider().GetService<IHttpContextAccessor>());
ConfigItems.Initialize(builder.Services.BuildServiceProvider().GetService<IHttpContextAccessor>(), configuration);
AWSHelper.Initialize(builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>());
ServiceRegistry.RegisterServices(builder.Services);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseExceptionHandler(
    options =>
    {
        options.Run(
            async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var ex = context.Features.Get<IExceptionHandlerFeature>();

                if (ex != null)
                {
                    if (!app.Environment.IsDevelopment())
                    {
                        EmailHelper.SendMail("knowidont499@gmail.com", "RB News MVC Exception Mail " + DateTime.Now, "test.uniqueitsolution@gmail.com, dhorajiyabrijesh607@gmail.com");
                    }
                }
            }
            );
    }
);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();
