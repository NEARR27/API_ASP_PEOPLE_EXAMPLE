using PeopleApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleService, PeopleService>();
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService");
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service");

builder.Services.AddKeyedSingleton<IRandomService, RandomServices>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomServices>("randomScope");
builder.Services.AddKeyedTransient<IRandomService, RandomServices>("randomTransient");




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