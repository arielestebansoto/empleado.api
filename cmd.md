#Crear el proyecto
dotnet new web --framework net6.0

#Instalar Swagger
dotnet add package Swashbuckle.AspNetCore -v 6.2.3

#Instalar Entity Framework Core
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer