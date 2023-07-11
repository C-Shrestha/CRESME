using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRESME.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRESME.Data;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.Xml;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Wordprocessing;
using CRESME.Constants;
using System.Security.Claims;

namespace CRESME.Controllers.Tests
{
    [TestClass()]
    public class AdminControllerTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _environment;
        private readonly AdminController controller;

        // Setup
        public AdminControllerTests()
        {
            var environment = new Mock<IWebHostEnvironment>();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var context = new Mock<ApplicationDbContext>(optionsBuilder.Options);

            //var userManager = new Mock<UserManager<ApplicationUser>>(optionsBuilder.Options);
            var userManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            //userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(true);

            //var signInManager = new Mock<SignInManager<ApplicationUser>>(optionsBuilder.Options);
            var signInManager = new Mock<SignInManager<ApplicationUser>>(userManager.Object, Mock.Of<IHttpContextAccessor>(), Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(), null, null, null, null);

            _context = context.Object;
            _environment = environment.Object;
            _userManager = userManager.Object;
            _signInManager = signInManager.Object;

             controller = new AdminController(_context, _userManager, _environment, _signInManager);

            Console.WriteLine("Setup");
        }



        [TestMethod()]
        //[Fact]
        public async Task DeleteAllTest()
        {
            var user = new ApplicationUser
            {
                UserName = "cresmeAdmin",
                Email = "cresmeAdmin",
                Name = "cresmeAdmin",
                Role = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };

            var userA = new ApplicationUser
            {
                UserName = "J12356",
                Email = "J@GMAIL.COM",
                Name = "J",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userB = new ApplicationUser
            {
                UserName = "K123456",
                Email = "K@GMAIL.COM",
                Name = "K",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            await _userManager.CreateAsync(user, "Testing@123");
            await _userManager.CreateAsync(userA, "Testing@123");
            await _userManager.AddToRoleAsync(userA, Roles.Student.ToString());
            await _userManager.CreateAsync(userB, "Testing@123");
            await _userManager.AddToRoleAsync(userB, Roles.Student.ToString());

            //controller.DeleteAll();
            //await Task.Delay(300);

            ApplicationUser userInDb = await _userManager.FindByEmailAsync(user.Email);
            ApplicationUser userAInDb = await _userManager.FindByEmailAsync(userA.Email);

            Assert.IsNotNull(userInDb);
            Console.WriteLine(userInDb.Name);
            Assert.IsNotNull(userAInDb);// To be changed to IsNull
            Console.WriteLine(userAInDb.Name);
        }



        [TestMethod()]
        //[Fact]
        public void InstructorUpdateQuizTest()
        {
            // Generate a sample quiz
            var quiz = new Quiz
            {
                QuizId = 4,
                QuizName = "SampleQuiz",
                Block = "B1",
                Course = "C2",
                Term = "21-22",
                DateCreated = new DateTime(2022, 05, 23),
                StartDate = new DateTime(2022, 05, 30),
                EndDate = new DateTime(2022, 06, 07),
                PatientIntro = "Test",
                AuthorNames = "Test"
            };

            // Act
            //await controller.UpdateQuiz(4, "TestQuiz", "B2", "C2", "22-23", new DateTime(2023, 06, 23), new DateTime(2023, 06, 30), new DateTime(2023, 07, 07));
            // int QuizId, string QuizName, string Block, string Course, string Term, DateTime DateCreated, DateTime StartDate, DateTime EndDate

            // Assert that the CRESME has different data
            Assert.Fail();
        }



        [TestMethod()]
       // [Fact]
        public async Task DeleteTest()
        {
            var user = new ApplicationUser
            {
                UserName = "cresmeAdmin",
                Email = "cresmeAdmin",
                Name = "cresmeAdmin",
                Role = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };

            var userA = new ApplicationUser
            {
                UserName = "J12356",
                Email = "J@GMAIL.COM",
                Name = "J",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userB = new ApplicationUser
            {
                UserName = "K123456",
                Email = "K@GMAIL.COM",
                Name = "K",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            await _userManager.CreateAsync(user, "Testing@123");
            await _userManager.CreateAsync(userA, "Testing@123");
            await _userManager.AddToRoleAsync(userA, Roles.Student.ToString());
            await _userManager.CreateAsync(userB, "Testing@123");
            await _userManager.AddToRoleAsync(userB, Roles.Student.ToString());

            //controller.Delete("2");
            //await Task.Delay(300);

            ApplicationUser userInDb = await _userManager.FindByEmailAsync(user.Email);
            ApplicationUser userAInDb = await _userManager.FindByEmailAsync(userA.Email);

            Assert.IsNotNull(userInDb);
            Console.WriteLine(userInDb.Name);
            Assert.IsNotNull(userAInDb);// To be changed to IsNull
            Console.WriteLine(userAInDb.Name);
        }




        //Teardown
        public void Dispose()
        {
            //_output.WriteLine("Teardown");
        }


    }
}

/*[TestClass()]
    public class AdminControllerTests
    {
        // Deletes all users excluding the admin
        [TestMethod()]
        public async Task DeleteAllTest()
        {
            // Arrange

            //var context = Substitute.For<ApplicationDbContext>();
            //var userManager = service.GetService<UserManager<ApplicationUser>>();
            //var userManager = Substitute.For<UserManager>();
            //context.Users.AddRange();

                var user = new ApplicationUser
                {
                    UserName = "cresmeAdmin",
                    Email = "cresmeAdmin",
                    Name = "cresmeAdmin",
                    Role = "Admin",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true

                };

            var userA = new ApplicationUser
            {
                UserName = "J123456",
                Email = "J@gmail.com",
                Name = "John",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userB = new ApplicationUser
            {
                UserName = "K123456",
                Email = "K@gmail.com",
                Name = "Kay",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };


            //var environment = Substitute.For<IWebHostEnvironment>();
            //var signInManager = Substitute.For<SignInManager>();

            // private readonly ApplicationDbContext _context;
            // private readonly SignInManager<ApplicationUser> _signInManager;
            // private readonly UserManager<ApplicationUser> _userManager;
            //private readonly IWebHostEnvironment _environment;

           // var controller = new AdminController(context, userManager, environment, signInManager);


            //Act
            //await controller.DeleteAll();


            // Assert
            // Assert that there is only one user left (admin)
            Assert.Fail();
        }



        // Updates a preexisting CRESME
        [TestMethod()]
        public async Task UpdateQuizTest()
        {
            // Arrange
            //var context = Substitute.For<ApplicationDbContext>();
            //context.Users.AddRange();

            var userA = new ApplicationUser
            {
                UserName = "J123456",
                Email = "J@gmail.com",
                Name = "John",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var userB = new ApplicationUser
            {
                UserName = "K123456",
                Email = "K@gmail.com",
                Name = "Kay",
                Role = "Student",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };


            //var environment = Substitute.For<IWebHostEnvironment>();
            //var signInManager = Substitute.For<SignInManager>();
            //var userManager = Substitute.For<UserManager>();
            // private readonly ApplicationDbContext _context;
            // private readonly SignInManager<ApplicationUser> _signInManager;
            // private readonly UserManager<ApplicationUser> _userManager;
            //private readonly IWebHostEnvironment _environment;

            //var controller = new AdminController(context, userManager, environment, signInManager);


            // Act
            //await controller.UpdateQuiz(4, "TestQuiz", "B2", "C2", "22-23", new DateTime(2023, 06, 23), new DateTime(2023, 06, 30), new DateTime(2023, 07, 07));
            // int QuizId, string QuizName, string Block, string Course, string Term, DateTime DateCreated, DateTime StartDate, DateTime EndDate

            // Assert
            // Assert that the CRESME has different data
            Assert.Fail();
        }



        // Deletes a User
        [TestMethod()]
        public async Task DeleteTest()
        {
            // Arrange
            //var context = Substitute.For<ApplicationDbContext>();
           // var signInManager = Substitute.For<SignInManager>();
           // var userManager = Substitute.For<UserManager>();

            //var environment = Substitute.For<IWebHostEnvironment>();

            // private readonly ApplicationDbContext _context;
            // private readonly SignInManager<ApplicationUser> _signInManager;
            // private readonly UserManager<ApplicationUser> _userManager;
            //private readonly IWebHostEnvironment _environment;

            //var controller = new AdminController(context, userManager, environment, signInManager);


            // Act
            //await controller.Delete("4");

            // Assert
            // Assert that the designated CRESME no longer exists
            Assert.Fail();
        }


}
}*/
