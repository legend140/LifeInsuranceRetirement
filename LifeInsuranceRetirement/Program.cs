using LifeInsuranceRetirement.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>(true);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContextPool<LifeInsuranceRetirementDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LifeInsuranceRetirementDb"));
});
builder.Services.AddScoped<IConfigurationData, SQLConfigurationData>();
builder.Services.AddScoped<IConsumerData, SQLConsumerData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
