using JensenAuktion.Repository.Repos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddScoped<UserRepo>();

builder.Services.AddControllers();




var app = builder.Build();
app.UseSession();


app.UseRouting();
app.UseEndpoints(endpoints => {  endpoints.MapControllers(); });


app.Run();
