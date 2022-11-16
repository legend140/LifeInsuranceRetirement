using LifeInsuranceRetirement.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<LifeInsuranceRetirementDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LifeInsuranceRetirementDb"));
});
builder.Services.AddScoped<IConfigurationData, SQLConfigurationData>();
builder.Services.AddScoped<IConsumerData, SQLConsumerData>();
builder.Services.AddScoped<IBenefitData, SQLBenefitData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
