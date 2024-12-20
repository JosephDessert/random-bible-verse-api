using Microsoft.EntityFrameworkCore;
using random_bible_verse_api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") 
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<VerseContext>(opt =>
    opt.UseInMemoryDatabase("VerseDB"));
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowLocal");

app.MapControllers();

app.Run();
