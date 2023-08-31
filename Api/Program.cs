using Aws.Api.Injections;
using Aws.Api.Middlewares;
using Aws.Services.Injections;
using Infra.Injections;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddConfiguredSwagger();
builder.Services.AddServices();
builder.Services.AddJWT(builder);
builder.Services.AddProfiles();
builder.Services.AddValidators();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.Run();
