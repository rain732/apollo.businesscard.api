using Apollo.BusinessCard.Application;
using Apollo.BusinessCard.Application.Common.Shared;
using Apollo.BusinessCard.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


AppSetting _appSetting = new AppSetting();
builder.Configuration.GetSection("AppSettings").Bind(_appSetting);
builder.Services.AddSingleton(_appSetting);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
                          .WithOrigins(_appSetting.AllowedCrossOrign)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
        });
});

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfraStructure(builder.Configuration);
builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
