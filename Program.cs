using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Optivem.Framework.Core.Domain;
using System;
using System.Diagnostics;
using System.Text;
using Todo_List_ASPNETCore.Controllers;
using Todo_List_ASPNETCore.DAL;
using Todo_List_ASPNETCore.Services;

var builder = WebApplication.CreateBuilder(args);

/*var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });*/

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<TodoListContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<EmailService>();

builder.Services.AddControllers();

builder.Services.AddTransient<NotificationBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

/*using (var context = new TodoListContext(new DbContextOptions<TodoListContext>()))
{
    //creates db if not exists 
    context.Database.EnsureCreated();

    //create entity objects
    var task1 = new TASK()
    {
        Task_Desc = "Faire un premier test",
        Task_Title = "Test 1",
        Task_Deadline = Convert.ToDateTime("16/05/2024"),
        Task_Priority = "Haute",
        Task_Status = false,
        Category_ID = 1
    };

    var categ = new CATEGORY()
    {
        Category_Name = "Travail"
    };
    
    //add entitiy to the context
    context.TASK.Add(task1);
    context.CATEGORY.Add(categ);

    //save data to the database tables
    context.SaveChanges();

    //retrieve all the tasks from the database
    foreach (var s in context.TASK)
    {
        Console.WriteLine($"First Task registering: {s.Task_Title}, Described as : {s.Task_Desc}, Must be done at {s.Task_Deadline}\n");
        Console.WriteLine($"This task is register in the category: {s.Category.Category_Name}");
    }
}*/

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();

