using UniversityProject.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.InitConfigure();
builder.Services.AddServices();
var app = builder.Build();
app.AddMiddlewares();
app.Run();