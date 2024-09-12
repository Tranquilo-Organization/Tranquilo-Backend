using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.BLL.Manager.AccountManager;
using TranquiloSystem.DAL.Data.DbHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));


builder.Services.AddDbContext<TranquiloContext>(
	options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ,b=>b.MigrationsAssembly("TranquiloSystem.API")
    ));
builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = "JWT";
	options.DefaultChallengeScheme = "JWT";

}).AddJwtBearer("JWT", options =>
{
	#region SecretKey
	var SecretKeyString = builder.Configuration.GetValue<string>("Key");
	var SecretKeyBytes = Encoding.ASCII.GetBytes(SecretKeyString);
	SecurityKey securityKey = new SymmetricSecurityKey(SecretKeyBytes);
	#endregion

	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
	{
		IssuerSigningKey = securityKey,
		ValidateIssuer = false,
		ValidateAudience = false,
	};
});

builder.Services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>(options =>
{
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 5;
})
				.AddEntityFrameworkStores<TranquiloContext>();

builder.Services.AddScoped<IAccountManager, AccountManager>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
