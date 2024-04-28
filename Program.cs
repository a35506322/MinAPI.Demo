var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

// EF Core
builder.Services.AddDbContext<TodoContext>(options => options.UseSqlServer(config.GetConnectionString("Todo")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.FluentValidationSetting();

// Error Handler
// https://www.milanjovanovic.tech/blog/global-error-handling-in-aspnetcore-8?utm_source=YouTube&utm_medium=social&utm_campaign=25.03.2024#configuring-iexceptionhandler-implementations
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.MapEndpoint();

app.Run();