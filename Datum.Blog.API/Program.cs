using Datum.Blog.API.Configurations;
using Datum.Blog.API.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddResponseCompression(opt =>
{
    opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
});

// configura injeção de dependência
DIPSetup.Register(builder.Services);
// configura swagger
SwaggerSetup.AddSwaggerSetup(builder.Services);
// configura o entity framework
EntityFrameworkSetup.AddEntityFrameworkSetup(builder.Services, builder.Configuration);
// configura o cors
CorsSetup.AddCorsSetup(builder.Services);
// configura o jwt
JwtTokenSetup.JwtSetup(builder.Services, builder.Configuration);
// configura o signalR
SignalRSetup.AddSignalRSetup(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // setup para configuração do swagger
    SwaggerSetup.UseSwaggerSetup(app);
}

CorsSetup.UseCorsSetup(app);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<BlogHub>("/bloghub");

app.Run();
