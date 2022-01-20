 # UniversityProject 
[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/sominola/UniversityProject/Build%20and%20deploy?style=flat-square)](https://github.com/sominola/UniversityProject/actions/workflows/workflow.yml)

[Test Website](http://university-project.azurewebsites.net/)

### Database Configuration
Add connection string to the file appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  }
}
```
### Database Migrations

Apply Migrations

* `cd .\UniversityProject.Web\`
* `dotnet ef database update`

Add Migration
* `cd .\UniversityProject.Data\`
* `dotnet ef migrations add Check -s ../UniversityProject.Web./UniversityProject.Web.csproj`
