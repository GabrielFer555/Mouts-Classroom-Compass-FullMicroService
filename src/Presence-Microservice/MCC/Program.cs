using Microsoft.EntityFrameworkCore;
using MCC.Data;
using MCC.Models;
using Microsoft.AspNetCore.Identity;
using MCC.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MCC.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MCC.Services.Authentication;
using Microsoft.AspNetCore.Builder;

namespace MCC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Database connection configuration
			var connString = builder.Configuration.GetConnectionString("StringConnection");
			builder.Services.AddDbContext<UserDBContext>(opts =>
			{
				opts.UseNpgsql(connString);
			});

			// Identity configuration
			builder.Services
				.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<UserDBContext>()
				.AddDefaultTokenProviders();

			// AutoMapper configuration
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			// Register services
			builder.Services
				.AddScoped<RegisterService>()
				.AddScoped<LoginService>()
				.AddScoped<TokenService>();

			builder.Services.AddControllers();

			// Swagger configuration
			builder.Services.AddEndpointsApiExplorer();

			// Additional DbContext
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseNpgsql(builder.Configuration.GetConnectionString("StringConnection")));

			// Repository services
			builder.Services.AddScoped<StudentService>();
			builder.Services.AddScoped<StudentRepository>();
			builder.Services.AddScoped<SubjectService>();
			builder.Services.AddScoped<SubjectRepository>();
			builder.Services.AddScoped<GradeService>();
			builder.Services.AddScoped<GradeRepository>();
			builder.Services.AddScoped<TeacherService>();
			builder.Services.AddScoped<TeacherRepository>();
			builder.Services.AddScoped<ValidationService>();

			// JWT authentication configuration
			var jwtKey = builder.Configuration["Jwt:Key"];
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
				};
			});

			// CORS configuration
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins",
					builder => builder.AllowAnyOrigin()
									  .AllowAnyMethod()
									  .AllowAnyHeader());
			});

			// Authorization configuration
			builder.Services.AddAuthorization(options =>
			{
				options.AddPolicy("RequerProfessor", policy =>
					policy.RequireRole("TEACHER"));
				options.AddPolicy("RequerAluno", policy =>
					policy.RequireRole("STUDENT"));
			});

			var app = builder.Build();

			// Enable middleware for error handling (optional)
			// app.UseExceptionHandler("/error");

			// Enable HTTPS redirection
			app.UseHttpsRedirection();

			app.UseRouting();

			// Enable authentication and authorization middleware
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCors("AllowAllOrigins");

			// Map endpoints to controllers
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			// Enable Swagger UI
			app.UseSwagger();

			app.Run();
		}
	}
}
