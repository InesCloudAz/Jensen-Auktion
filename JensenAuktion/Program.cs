using JensenAuktion.Repository.Interfaces;
using JensenAuktion.Repository.Repos;

using JensenAuktion;
using JensenAuktion.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<IJensenAuctionContext, JensenAuctionContext>();

var app = builder.Build();

app.UseCors(policy => 
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
);

app.UseSwagger();
app.UseSwaggerUI();

app.UseSession();
app.UseAuthorization();
app.MapControllers(); app.Run();
