using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using webapi_first.Database;
using webapi_first.Repositories;
using webapi_first.Services.CharacterService;
using webapi_first.Services.Fights;
using webapi_first.Services.WeaponService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more abo configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = ""
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//builder.Services.AddSwaggerGen(c =>
//{
//    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Description = "",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey

//    });
//    c.OperationFilter<SecurityRequirementsOperationFilter>();
//});

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//Services
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IWeaponService, WeaponService>();
builder.Services.AddScoped<IFightService, FightService>();
//Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
//Authentication JWT

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = false,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
//        .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
//        ValidateAudience = false,
//    });



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
options.TokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
    ValidateIssuer = false,
    ValidateAudience = false
});
//Http Accessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Added Authentication
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
