using categorizationapi.Services;
using categorizationapi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registra os serviços e repositórios
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<IDepartamentalizacaoService, DepartamentalizacaoService>();
builder.Services.AddScoped<IDepartamentalizacaoRepository, DepartamentalizacaoRepository>();

// Adiciona controladores
builder.Services.AddControllers();
builder.Services.AddHttpClient();

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
