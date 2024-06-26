using API.Errors;
using API.Extensions;
using API.Middleware;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder .Services .AddApplicationServices(builder .Configuration);
builder.Services.AddIdentityServices(builder .Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");// added a middleware for class no 51




//if (app.Environment.IsDevelopment())
//{
    
//}

//app.UseHttpsRedirection();---seethal

app.UseSwaggerDocumentation();

app.UseStaticFiles(); //for class 45 images in postman

app.UseStaticFiles(new StaticFileOptions{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(),"Content")),RequestPath ="/Content"
});

app.UseCors("CorsPolicy");//class 67 CORS header

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToController("Index","Fallback");

using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
var context=services.GetRequiredService<StoreContext>();
var identityContext=services.GetRequiredService<AppIdentityDbContext>();
var userManager=services.GetRequiredService<UserManager<AppUser>>();
var logger=services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    await AppIdentityDbContextSeed.SeedUsersAsync(userManager);
}
catch (Exception ex)
{
    
    logger.LogError(ex,"An ERROR occured during migration");
}
app.Run();
