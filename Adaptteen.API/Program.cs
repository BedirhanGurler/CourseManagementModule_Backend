using Adaptteen.Business.Abstract;
using Adaptteen.Business.Concrete;
using Adaptteen.Common.Validations.Abstract;
using Adaptteen.Common.Validations.Concrete;
using Adaptteen.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddTransient(typeof(ICourseService<>), typeof(CourseService<>));
builder.Services.AddTransient(typeof(ICategoryService<>), typeof(CategoryService<>));
builder.Services.AddScoped<IModelStateResponseService, ModelStateResponseManager>();

builder.Services.AddDbContext<ConfigDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("connstr")!, sqlOptions =>
    {
        sqlOptions.UseNetTopologySuite();
    });
});

var corsPolicyName = "_allowFrontendOrigin";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
