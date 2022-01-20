using UniversityProject.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.InitConfigure();
builder.Services.AddServices();
var app = builder.Build();
await app.InitializeDb();
app.AddMiddlewares();

app.Run();