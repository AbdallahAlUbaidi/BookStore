using System.Text;
using Data;
using Helpers;
using Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Middleware;
using Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
		});

builder.Services.AddDbContext<DataContext>(options =>
{
	options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection") ?? "");
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	var configuration = builder.Configuration;

	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = false,
		ValidIssuer = configuration["Jwt:issuer"],
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
	};
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IJsonWebTokenIssuer, JsonWebTokenIssuer>(provider =>
{
	var configuration = provider.GetRequiredService<IConfiguration>();
	var secretKey = configuration["Jwt:SecretKey"]!;
	var issuer = configuration["Jwt:issuer"]!;

	return new JsonWebTokenIssuer(secretKey, issuer);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<Seed>();

var app = builder.Build();

// app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapPost("/seed", (Seed seeder) =>
{
	seeder.SeedDB();

	return Results.Ok();
});


app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();
