using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OAuthJwtExample API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. " +
        "Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }
    });
});

var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyHereAndTheKeyByteMustMoreThan256");

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true; // 是否保存令牌到HttpContext中
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // 是否验证令牌的发行者
        ValidateAudience = true, // 是否验证令牌的受众
        ValidateLifetime = true, // 是否验证令牌的生命周期
        ValidateIssuerSigningKey = true, // 是否验证令牌的签名密钥

        // 假设你有一个身份认证服务器，其URL为 https://auth.example.com。
        // 这个服务器负责生成和签署JWT令牌。
        // 在这种情况下，ValidIssuer 应设置为 https://auth.example.com。
        ValidIssuer = "https://auth.example.com",  // 令牌的有效发行者

        // ValidAudience 是指JWT令牌的预期接收者。
        // 它通常是一个标识符，用来确定令牌的目标受众是谁。通常是应用程序或API。
        // 假设你有一个API服务器，其URL为 https://api.example.com。
        // 这个服务器是JWT令牌的预期接收者。
        // 在这种情况下，ValidAudience 应设置为 https://api.example.com。
        ValidAudience = "https://api.example.com",  // 令牌的有效受众

        IssuerSigningKey = new SymmetricSecurityKey(key) // 用于验证令牌签名的密钥
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OAuthJwtExample API v1"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
