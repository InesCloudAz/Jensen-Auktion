using JensenAuktion.Repository.Interfaces;
using JensenAuktion.Repository.Repos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepo, UserRepo>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseSession();
app.UseAuthorization();
app.MapControllers(); app.Run();
