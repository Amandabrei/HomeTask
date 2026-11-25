using System.Reflection;
using HomeTask.HomeTask.Core.Interfaces;
using HomeTask.HomeTask.Core.Services;
using HomeTask.HomeTask.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "HomeTask API",
        Version = "v1",
        Description = "API para gerenciamento de tarefas e organização familiar. "
                    + "Permite cadastrar membros, criar tarefas e atribuí-las aos integrantes da família."
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<FamilyMemberService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<TaskAssignmentService>();

builder.Services.AddSingleton<IFamilyMemberRepository, FamilyMemberRepository>();
builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
builder.Services.AddSingleton<ITaskAssignmentRepository, TaskAssignmentRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
