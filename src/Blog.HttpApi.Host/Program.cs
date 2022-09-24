using Blog.Application;
using Blog.HttpApi.Host.Filters;
using Blog.HttpApi.Host.Options;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// Add services to the container.
var configuration = services.BuildServiceProvider()?.GetRequiredService<IConfiguration>();

#region 注入File配置
var fileOptionsSection = configuration.GetSection(nameof(BlogFileOptions));

services.Configure<BlogFileOptions>(fileOptionsSection);

#endregion

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1.0", new OpenApiInfo()
    {
        Version = "v1.0",
        Title = "博客Api",
        Description = "博客的WebApiSwagger文档",
        Contact = new OpenApiContact
        {
            Name = "Simple",
            Email = "239573049@qq.com",
            Url = new Uri("https://github.com/239573049")
        }
    });

    string[] files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");//获取api文档
    string[] array = files;
    foreach(string filePath in array)
    {
        options.IncludeXmlComments(filePath, includeControllerXmlComments: true);
    }

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "填写你的Tokne 需要添加前缀 Bearer {{token}}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
});

services.AddApplication();
services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", corsBuilder =>
    {
        corsBuilder.SetIsOriginAllowed((string _) => true).AllowAnyMethod().AllowAnyHeader()
            .AllowCredentials();
    });
});

var JwtOptionsSection = configuration.GetSection(nameof(JwtOptions));

services.Configure<JwtOptions>(JwtOptionsSection);

var jwt = JwtOptionsSection.Get<JwtOptions>();

// 注入方法
services.AddCurrent();

// 注入Jwt的配置
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = true, //是否在令牌期间验证签发者
            ValidateAudience = true, //是否验证接收者
            ValidateLifetime = true, //是否验证失效时间
            ValidateIssuerSigningKey = true, //是否验证签名
            ValidAudience = jwt.Audience, //接收者
            ValidIssuer = jwt.Issuer, //签发者，签发的Token的人
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey!)) // 密钥
        };
    });

// 添加过滤器
builder.Services.AddMvcCore(x =>
{
    x.Filters.Add<ResponseFilter>(); //  相应格式过滤器
    x.Filters.Add<ExceptionFilter>();// 异常过滤器
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "博客Api");
    c.DocExpansion(DocExpansion.None);
    c.DefaultModelsExpandDepth(-1);
});

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
