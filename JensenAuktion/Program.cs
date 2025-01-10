using JensenAuktion;
using JensenAuktion.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJensenAuctionContext, JensenAuctionContext>();



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();

app.Run();
