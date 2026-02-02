using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<Selu383.SP26.Api.Data.DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")));

// CORS (allow any origin in Development for quick local testing)
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Selu383.SP26.Api.Data.DataContext>();
    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.Migrate();
    }
    else
    {
        db.Database.EnsureCreated();
    }
    if (!db.Locations.Any())
    {
        db.Locations.AddRange(
            new Selu383.SP26.Api.Entities.Location { Name = "Downtown Commons", Address = "100 Main St", TableCount = 15 },
            new Selu383.SP26.Api.Entities.Location { Name = "University Lounge", Address = "200 College Dr", TableCount = 25 },
            new Selu383.SP26.Api.Entities.Location { Name = "Lakefront Terrace", Address = "300 River Rd", TableCount = 12 }
        );
        db.Users.AddRange(
            new Selu383.SP26.Api.Entities.User { UserName = "bob", DisplayName = "Bob Builder" },
            new Selu383.SP26.Api.Entities.User { UserName = "sue", DisplayName = "Sue Storm" }
        );
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply permissive CORS only in Development
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
}

app.UseAuthorization();

app.MapControllers();

app.Run();

//see: https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0
// Hi 383 - this is added so we can test our web project automatically
public partial class Program { }
