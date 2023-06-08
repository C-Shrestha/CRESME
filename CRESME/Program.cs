using CRESME.Data;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//.AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();


// added for SwaggerHub

// Searches for and generates endpoints in Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRESME API", Version = "v1" });
});

var app = builder.Build();

/*// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}*/

app.UseDeveloperExceptionPage();
app.UseMigrationsEndPoint();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// *****need to change this later*****
app.MapControllerRoute(
    name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

//pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapRazorPages();

// Displays the API endpoints at /swagger/index.html
app.UseSwagger();
app.UseSwaggerUI();


using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);


    //delete this attempts seeding section after takequiz view can sucessfully create attempts
    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

    context.Database.EnsureCreated();

    var testattempt1 = context.Attempt.FirstOrDefault(b => b.StudentNID == "ab000111");
    if (testattempt1 == null)
    {
        context.Attempt.Add(new Attempt
        {
            StudentNID = "ab000111",
            StudentName = "Jason Mendez",
            QuizName = "DisplayTest",
            Term = "2022-23",
            Course = "Practice of Medicive",
            Block = "B2",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            Score = 4,
            NumColumns = 5,
            PhysicalAnswerA = "B",
            PhysicalAnswerB = "C",
            PhysicalAnswerC = "D",
            PhysicalAnswerD = "A",
            PhysicalAnswerE = "E",
            DiagnosticAnswerA = "A",
            DiagnosticAnswerB = "B",
            DiagnosticAnswerC = "C",
            DiagnosticAnswerD = "E",
            DiagnosticAnswerE = "D",
            FreeResponseA = "Heart Arrest",
            FreeResponseB = "Lung Arrest",
            FreeResponseC = "Brain Arrest",
            FreeResponseD = "Leg Arrest",
            FreeResponseE = "Arm Arrest",
            NumImage0Clicks = "B2 Image clicked 3 times",
            NumImage3Clicks = "C1 Image clicked 0 times"
        });
    }

    var testattempt2 = context.Attempt.FirstOrDefault(b => b.StudentNID == "cd222333");
    if (testattempt2 == null)
    {
        context.Attempt.Add(new Attempt
        {
            StudentNID = "cd222333",
            StudentName = "Michael John",
            QuizName = "DisplayTest",
            Term = "2023-24",
            Course = "Pediatrics",
            Block = "B4",
            StartTime = DateTime.Now,
            EndTime = DateTime.Now,
            Score = 10,
            NumColumns = 4,
            PhysicalAnswerA = "A",
            PhysicalAnswerB = "B",
            PhysicalAnswerC = "C",
            PhysicalAnswerD = "D",
            DiagnosticAnswerA = "B",
            DiagnosticAnswerB = "A",
            DiagnosticAnswerC = "C",
            DiagnosticAnswerD = "D",
            FreeResponseA = "Heart Arrest",
            FreeResponseB = "Lung Arrest",
            FreeResponseC = "Brain Arrest",
            FreeResponseD = "Leg Arrest",
            NumImage0Clicks = "B2 Image clicked 5 times",
            NumImage3Clicks = "C1 Image clicked 10 times"
        });
    }
    context.SaveChanges();
    //end of attempts seeding section
    
}






app.Run();
