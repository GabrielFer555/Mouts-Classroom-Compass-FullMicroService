using Microsoft.OpenApi.Models;
using PresenceApi.Presences.CreatePresence;
using PresenceApi.Presences.CreateVariousPresences;
using PresenceApi.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Presence Api", Version = "v1" }));
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins",
		builder => builder.AllowAnyOrigin()
						  .AllowAnyMethod()
						  .AllowAnyHeader());
});

var mongoDBSection = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDbSettings>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddScoped<IValidator<CreatePresenceRequest>, PresenceValidator>();
builder.Services.AddScoped<IValidator<CreateVariousPresencesRequest>, CreateVariousPresenceValidator>();

builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
	var connectionString = mongoDBSection.AtlasURI;
	return new MongoClient(connectionString);
});
builder.Services.AddScoped<MongoDBContext>();

builder.Services.AddMediatR(configs =>
{
	configs.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(e => e.RoutePrefix="swagger");
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.MapCarter();
app.Run();
