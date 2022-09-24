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

#region ע��File����
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
        Title = "����Api",
        Description = "���͵�WebApiSwagger�ĵ�",
        Contact = new OpenApiContact
        {
            Name = "Simple",
            Email = "239573049@qq.com",
            Url = new Uri("https://github.com/239573049")
        }
    });

    string[] files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");//��ȡapi�ĵ�
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
        Description = "��д���Tokne ��Ҫ���ǰ׺ Bearer {{token}}",
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

// ע�뷽��
services.AddCurrent();

// ע��Jwt������
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidateIssuer = true, //�Ƿ��������ڼ���֤ǩ����
            ValidateAudience = true, //�Ƿ���֤������
            ValidateLifetime = true, //�Ƿ���֤ʧЧʱ��
            ValidateIssuerSigningKey = true, //�Ƿ���֤ǩ��
            ValidAudience = jwt.Audience, //������
            ValidIssuer = jwt.Issuer, //ǩ���ߣ�ǩ����Token����
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey!)) // ��Կ
        };
    });

// ��ӹ�����
builder.Services.AddMvcCore(x =>
{
    x.Filters.Add<ResponseFilter>(); //  ��Ӧ��ʽ������
    x.Filters.Add<ExceptionFilter>();// �쳣������
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "����Api");
    c.DocExpansion(DocExpansion.None);
    c.DefaultModelsExpandDepth(-1);
});

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
