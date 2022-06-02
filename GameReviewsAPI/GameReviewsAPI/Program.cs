using GameReviewsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using GameReviewsAPI.Domain.Repositories;
using GameReviewsAPI.Domain.Services;
using GameReviewsAPI.Domain.Abstractions;
using GameReviewsAPI;
using GameReviewsAPI.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddScoped<IRepository<Game>, GameRepository>();
builder.Services.AddTransient<IRepository<Review>, ReviewRepository>();
builder.Services.AddTransient<GetReviewsService>();
builder.Services.AddTransient<AddReviewService>();
builder.Services.AddTransient<GetAllSortedService>();
builder.Services.AddTransient<DeleteGameService>();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<GameReviewsContext>(options => 
    options.UseSqlServer(connection, b => b.MigrationsAssembly("GameReviewsAPI")));
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "GameReviews API",
        Description = "АPI для записи и хранения игр и рецензий к ним, а так же получения их данных",

    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    }); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
