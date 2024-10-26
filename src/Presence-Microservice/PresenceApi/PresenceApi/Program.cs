using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using PresenceApi.Models;
using PresenceApi.Context;
using Microsoft.Extensions.Configuration;
using System;
using PresenceApi.Presences.CreatePresence;
using PresenceApi.Validators;
using PresenceApi.Presences.CreateVariousPresences;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

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

app.UseCors("AllowAllOrigins");

app.MapCarter();

app.Run();
