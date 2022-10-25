using Application.Core.DomainServices;
using DemoAppliaction.Core.Application;
using DemoApplication.Infrastructure.Persistance;
using DemoApplication.Infrastructure.Persistance.Seeds;
using WebApplication1.Extensions;

var builder = WebApplication.CreateBuilder(args);

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "http://localhost:4200/", "localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register services of persistance layer.
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

//register services of application layer
builder.Services.AddApplicationLayer();

builder.Services.AddDomainServicesLayer();

builder.Services.AddSwaggerExtension();

builder.Services.AddApiVersioningExtension();


var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var services = app.Services.CreateScope();
await services.ServiceProvider.SeedAsync();
app.Run();
