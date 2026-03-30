using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SQLINKHomeAssignment;
using SQLINKHomeAssignment.Models;
using SQLINKHomeAssignment.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProjectsService>();

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Users.Any())
    {
        var alice = new User
        {
            Email = "alice@test.com",
            Password = "Password1",
            Name = "Alice Cohen",
            Team = "Backend",
            JoinedAt = new DateTime(2022, 3, 15),
            Avatar = "https://i.pravatar.cc/150?img=1"
        };

        var bob = new User
        {
            Email = "bob@test.com",
            Password = "Password2",
            Name = "Bob Levi",
            Team = "Frontend",
            JoinedAt = new DateTime(2023, 7, 1),
            Avatar = "https://i.pravatar.cc/150?img=2"
        };

        db.Users.AddRange(alice, bob);
        db.SaveChanges();

        db.Projects.AddRange(
            new Project { UserId = alice.Id, Name = "API Gateway",     Score = 85, DurationInDays = 30, BugsCount = 3,  MadeDeadline = true  },
            new Project { UserId = alice.Id, Name = "Auth Service",    Score = 92, DurationInDays = 14, BugsCount = 1,  MadeDeadline = true  },
            new Project { UserId = alice.Id, Name = "Data Pipeline",   Score = 65, DurationInDays = 45, BugsCount = 8,  MadeDeadline = false },
            new Project { UserId = bob.Id,   Name = "Dashboard UI",    Score = 78, DurationInDays = 21, BugsCount = 5,  MadeDeadline = true  },
            new Project { UserId = bob.Id,   Name = "Component Lib",   Score = 95, DurationInDays = 60, BugsCount = 2,  MadeDeadline = true  },
            new Project { UserId = bob.Id,   Name = "Mobile App",      Score = 68, DurationInDays = 90, BugsCount = 12, MadeDeadline = false }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();