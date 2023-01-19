using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using static Microsoft.OpenApi.Models.OpenApiInfo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MessageBoardContext>(
                  dbContextOptions => dbContextOptions
                    .UseMySql(
                      builder.Configuration["ConnectionStrings:DefaultConnection"],
                      ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
                    )
                  )
                );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
  {
    Title = "jwtToken_Auth_Api",
    Version = "v1"
  });
  c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
  {
    Name = "Authorization",
    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    Description = "Enter token here"
  });
  c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Reference= new Microsoft.OpenApi.Models.OpenApiReference
            {
                Type=Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                Id="Bearer"
            }
        },
        new string[] {}
        }
    });
});

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
  o.TokenValidationParameters = new TokenValidationParameters
  {
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey
      (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = false,
    ValidateIssuerSigningKey = true
  };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
else
{
  app.UseHttpsRedirection();
}

app.MapGet("/security/getMessage",
() => "Hello World!").RequireAuthorization();

app.MapPost("/security/createToken",
[AllowAnonymous] async (AppUser user) =>
{
  if (user.UserName == "chris" && user.Password == "chris123")
  {
    var issuer = builder.Configuration["Jwt:Issuer"];
    var audience = builder.Configuration["Jwt:Audience"];
    var key = Encoding.ASCII.GetBytes
    (builder.Configuration["Jwt:Key"]);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[]
        {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
            }),
      Expires = DateTime.UtcNow.AddMinutes(5),
      Issuer = issuer,
      Audience = audience,
      SigningCredentials = new SigningCredentials
        (new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha512Signature)
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    var jwtToken = tokenHandler.WriteToken(token);
    var stringToken = tokenHandler.WriteToken(token);
    return Results.Ok(stringToken);
  }
  return Results.Unauthorized();
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();