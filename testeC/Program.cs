using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TestC.Data; // Ajuste para o namespace correto
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Registro do ApplicationDbContext com a string de conexão do PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurações adicionais de serviços, como controle de API
builder.Services.AddControllers();

// Configuração do Swagger/OpenAPI, se necessário
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Minha API", Version = "v1" });
});

var app = builder.Build();

// Configuração do ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API v1"));
}

// Aplicação das configurações de CORS
app.UseCors("MyAllowSpecificOrigins");

// Configurações do middleware
app.UseRouting();

app.UseAuthorization(); // UseAuthentication() pode ser necessário se a autenticação estiver configurada

// Configuração dos endpoints, como as rotas da API
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Garante que as rotas dos controllers sejam mapeadas
});

app.Run();
