using DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using ServiceLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DataLayer.Repositories;
using DataLayer.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),x=>x.MigrationsAssembly("VideoTaxi")));

builder.Services.AddIdentity<AppUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
       
        .AddEntityFrameworkStores<DataContext>() ;

builder.Services.Configure<IdentityOptions>(options =>
{

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;

});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireExpirationTime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration["JwtSettings:SecurityKey"]))
    };
});


builder.Services.AddScoped<IAccountsServiceInterface, AccountsService>();
builder.Services.AddScoped<ITokenServiceInterface, TokenService>();
builder.Services.AddScoped<IRegisterUserRepositoryInterface, RegisterUserRepository>();





var app = builder.Build();


// Configure the HTTP request pipeline.



app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(x=>x.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());

app.MapControllers();

app.Run();
