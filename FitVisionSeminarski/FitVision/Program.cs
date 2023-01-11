using FitVision.Data;
using FitVision.Hub;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConString"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
    options => options
        .SetIsOriginAllowed(x => _ = true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
); //This needs to set everything allowed

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("/info");
});

app.MapControllers();

app.Run();
