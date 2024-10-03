using Microsoft.EntityFrameworkCore;
using WebApiProject1.Data;
using WebApiProject1.Filters.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Register the dbcontext 
builder.Services.AddDbContext<ProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

// Register the ActionFilter
builder.Services.AddScoped<User_ValidateExistingUserFilterAttribute>();

// Add data protection services (required by session)
builder.Services.AddDataProtection();

// Add session services and configure for in-memory session storage
builder.Services.AddDistributedMemoryCache(); // Use in-memory cache for sessions

// Add session services
builder.Services.AddSession(options =>
{
    // Optional: configure session timeout
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Required for GDPR compliance
});

// Add services to the container and configure Newtonsoft.Json for handling reference loops
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();
app.UseSession(); // Use session middleware

app.MapControllers();

app.Run();
