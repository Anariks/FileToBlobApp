using Api.Validation;
using FluentValidation;
using Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IValidator<FormData>, FormDataValidator>();

builder.Services.AddBlobContainer(builder.Configuration);
builder.Services.AddSingleton<FileService>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseCors(
    x => x.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins("http://localhost:4200")
);

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseMiddleware<RedirectToIndexMiddleware>();

app.UseHttpsRedirection();

//app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

//app.UseMvc();

app.Run();
