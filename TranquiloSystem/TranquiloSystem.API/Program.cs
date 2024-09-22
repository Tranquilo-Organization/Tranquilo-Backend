using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using Tranquilo.DAL.Data.Models;
using Tranquilo.DAL.Repositories.AccountRepo;
using Tranquilo.DAL.Repositories.CommentRepo;
using Tranquilo.DAL.Repositories.PostRepo;
using Tranquilo.DAL.Repositories.RoutineRepo;
using TranquiloSystem.BLL.AutoMapper;
using TranquiloSystem.BLL.Manager.AccountManager;
using TranquiloSystem.BLL.Manager.EmailManager;
using TranquiloSystem.BLL.Manager.Notification;
using TranquiloSystem.BLL.Manager.OtpManager;
using TranquiloSystem.BLL.Manager.PostCommentManager;
using TranquiloSystem.BLL.Manager.PostManager;
using TranquiloSystem.BLL.Manager.ProfileManager;
using TranquiloSystem.BLL.Manager.RoutineManager;
using TranquiloSystem.DAL.Data.DbHelper;
using TranquiloSystem.DAL.Data.Models;
using TranquiloSystem.DAL.Repositories.NotificarioRepo;

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
		ValidateIssuer =true,
		ValidateAudience = true,
	};
});


builder.Services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>(options =>
{
	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 5;
	options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
}).AddEntityFrameworkStores<TranquiloContext>()
.AddDefaultTokenProviders();

builder.Services.AddMemoryCache();



builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
//builder.Services.Configure<SmtpSettings>(options =>
//   {
//	options.Host = Environment.GetEnvironmentVariable("SMTP_HOST");
//	options.Port = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));
//	options.UserName = Environment.GetEnvironmentVariable("SMTP_USERNAME");
//	options.Password = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
//})
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<INotificationManager, NotificationManager>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<IProfileManager, ProfileManager>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();

builder.Services.AddScoped<IPostCommentRepository, PostCommentRepository>();
builder.Services.AddScoped<IPostCommentManager, PostCommentManager>();

builder.Services.AddScoped<IRoutineManager, RoutineManager>();
builder.Services.AddScoped<IRoutineRepository, RoutineRepository>();

builder.Services.AddScoped<IEmailManager, EmailManager>();
builder.Services.AddScoped<IOtpManager, OtpManager>();
builder.Services.AddScoped<IAccountManager, AccountManager>();

builder.Services.AddScoped<IPostManager,PostManager>();
builder.Services.AddScoped<IPostRepository, PostRepository>();




var app = builder.Build();

var smtpSettings = app.Services.GetRequiredService<IOptions<SmtpSettings>>().Value;
Console.WriteLine($"SMTP Host: {smtpSettings.Host}");


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
