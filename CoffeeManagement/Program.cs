using CoffeeManagement.Middleware;
using CoffeeManagement.Model;
using CoffeeManagement.Repositories.Implement;
using CoffeeManagement.Repositories.Interface;
using CoffeeManagement.Services.Implement;
using CoffeeManagement.Services.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Add connectionString
builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// add IUnitOfWork and UnitOfWork
builder.Services.AddScoped<IUnitOfWork<DataBaseContext>, UnitOfWork<DataBaseContext>>();

// Add this line before registering your services
builder.Services.AddHttpContextAccessor();

// register autoMapper because autoMapper can register it self 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Add Interface and service
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IShiftService,ShiftService>();

// Add services to the container.

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
// Add the ExceptionHandlerMiddleware to the pipeline
// comment lai doan code phia duoi neu chuong khong doc duoc loi tu swagger
// ===============================================
app.UseMiddleware<ExceptionHandlerMiddleware>();
// ===============================================


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
