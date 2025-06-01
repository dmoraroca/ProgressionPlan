using ProgressPlanDDD.Application.Interfaces;
using ProgressPlannerDDD.Application.Services;
using ProgressPlannerDDD.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Agrega el servicio CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSingleton<ITodoList, TodoService>();
builder.Services.AddSingleton<ITodoListRepository, InMemoryTodoRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. Usar la política de CORS antes de UseAuthorization
app.UseCors("PermitirTodo");

app.UseAuthorization();

app.MapControllers();

app.Run();