using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// agregar los controladores
builder.Services.AddControllers();

// tambien es necesario para que funcione swagger
builder.Services.AddEndpointsApiExplorer();

// agregar el generador de Swagger
builder.Services.AddSwaggerGen();

// Agregar conection string
builder.Services.AddDbContext<EmpleadoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmpleadoConnection"));
    // para ver algunos errores
    //options.EnableSensitiveDataLogging();
});

var app = builder.Build();


if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// agrega los endpoints
app.MapControllers();

app.Run();
