using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Smarty.Data.Models;
using Smarty.Data.Repositories.Implementations;
using Smarty.Data.Repositories.Interfaces;
using Smarty.Data.Services;
using Smarty.Data.SmartyDBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddNToastNotifyToastr(new ToastrOptions(){
        ProgressBar = true,PositionClass = ToastPositions.TopRight,
        PreventDuplicates = true,CloseButton = true});

builder.Services.AddRazorPages();

builder.Services.AddDefaultIdentity<SmartyUser> (options =>
options.SignIn.RequireConfirmedAccount = false)
.AddEntityFrameworkStores<SmartyDbContext>();

builder.Services.AddDbContext<SmartyDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IInstructorService, InstructorService>();


var app = builder.Build();

// Configure the HTTP request pipeline 
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
