using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

// EF Core
builder.Services.AddDbContext<TodoContext>(options => options.UseSqlServer(config.GetConnectionString("Todo")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// FluentValidation
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapEndpoint();

app.Run();

