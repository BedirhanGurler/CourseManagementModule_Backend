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
        sqlOptions.UseNetTopologySuite(); // Enable NetTopologySuite support
    });
});

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
