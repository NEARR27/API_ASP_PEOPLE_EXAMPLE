using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PeopleApi.Automappers;
using PeopleApi.DTOS;
using PeopleApi.Models;
using PeopleApi.Repository;
using PeopleApi.Services;
using PeopleApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleService, PeopleService>();
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService");
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service");

builder.Services.AddKeyedSingleton<IRandomService, RandomServices>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomServices>("randomScope");
builder.Services.AddKeyedTransient<IRandomService, RandomServices>("randomTransient");

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddKeyedScoped<ICommonServices<BeerDTO, BeerIdDTO, BeerUpdateDTO>, BeerService>("beerService");

//Repository
builder.Services.AddScoped<IRepository<Beer>, BeerRepository>();

builder.Services.AddHttpClient<IPostService, PostService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]);
});

//EntityFramework
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

//Validadores
builder.Services.AddScoped<IValidator<BeerIdDTO>, BeerInsertValidator>  ();
builder.Services.AddScoped<IValidator<BeerUpdateDTO>, BeerUpdateValidator> ();

//mapppers
builder.Services.AddAutoMapper(typeof(MappingProfile));


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
