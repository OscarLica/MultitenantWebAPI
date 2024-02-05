using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MultiTenantApi.Aplication;
using MultiTenantWebApi;
using MultiTenantWebApi.Aplication;
using MultiTenantWebApi.CQRS.Product.Handlers.Commands;
using MultiTenantWebApi.CQRS.Product.Handlers.Queries;
using MultiTenantWebApi.CQRS.Product.Request.Commands;
using MultiTenantWebApi.CQRS.Product.Request.Queries;
using MultiTenantWebApi.Entities;
using MultiTenantWebApi.Infraestructura;
using MultiTenantWebApi.Infraestructura.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MasterDBSecurity")));
builder.Services.AddDbContext<AppDbContext>(options =>{}, ServiceLifetime.Scoped);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IDesignTimeDbContextFactory<AppDbContext>, AppDbContextFactory>();
builder.Services.AddScoped<ITenantResolver, TenantResolver>();
builder.Services.AddTransient<IOrganzationRepository, OrganizationRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<BaseDbContext>();
builder.Services.AddTransient<IRequestHandler<GetProductRequest, List<Products>>, GetProductHandler>();
builder.Services.AddTransient<IRequestHandler<GetProductIdRequest, Products>, GetProductIdHandler>();
builder.Services.AddTransient<IRequestHandler<CreateProductCommand, Products>, CreateProductHandler>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                            {
                                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                                {
                                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = JwtHelper.GetTokenParameters();
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseTenant();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.InitializeMigrations();
app.Run();
