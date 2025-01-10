using JensenAuktion.Repository.Repos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();





builder.Services.AddEndpointsApiExplorer();


builder.Services.AddScoped<UserRepo>();

builder.Services.AddControllers();




var app = builder.Build();
app.UseSession();


app.UseRouting();
app.UseEndpoints(endpoints => {  endpoints.MapControllers(); });

app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();

app.Run();
