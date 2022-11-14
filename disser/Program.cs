using disser.Extensions;
using disser.Models.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddServices();

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
builder.Services.AddCors(options =>
{
    var frontend_url = configuration.GetValue<string>("frontend_url");

    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontend_url).AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "jwtToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert JWT token"
    });
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
            },
        new string[]{}
        }
        });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.RequireHttpsMetadata = false;
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 // укзывает, будет ли валидироваться издатель при валидации токена
                 ValidateIssuer = true,
                 // строка, представляющая издателя
                 ValidIssuer = AuthOptions.ISSUER,

                 // будет ли валидироваться потребитель токена
                 ValidateAudience = true,
                 // установка потребителя токена
                 ValidAudience = AuthOptions.AUDIENCE,
                 // будет ли валидироваться время существования
                 ValidateLifetime = true,

                 // установка ключа безопасности
                 IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                 // валидация ключа безопасности
                 ValidateIssuerSigningKey = true,
                 ClockSkew = TimeSpan.Zero
             };
         });
builder.Services.AddControllersWithViews();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
//app.UseStaticFiles();

//app.UseRouting();

app.UseCors();

app.MapControllers();

app.Run();
