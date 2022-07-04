using AutoMapper;
using Vendors.AzureTablerepo.Contracts;
using Vendors.AzureTablerepo.Models;
using Vendors.AzureTablerepo.Services;
using Vendors.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = Environment.GetEnvironmentVariable("AzureTableStorage");
string tableName = Environment.GetEnvironmentVariable("TableName");

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new VendorProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddScoped<IAzureRepo<Vendor>>(x => new AzureTableRepoVendors(connectionString, tableName, mapper));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())

    app.UseSwagger();
    app.UseSwaggerUI();
    //}
    app.UseCors("corsapp");
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

