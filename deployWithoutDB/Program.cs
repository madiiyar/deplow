var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton(sp =>
    new deployAPI.Services.JsonFileService(Path.Combine(builder.Environment.ContentRootPath, "users.json")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
