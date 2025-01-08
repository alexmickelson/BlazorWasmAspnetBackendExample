using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
  opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
      ["application/octet-stream"]);
});

var app = builder.Build();
app.UseCors(policy =>
  policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);
app.MapGet("/", () => "Hello World!");

app.MapHub<LobbyHub>("/api/gameHub");

app.Run();
