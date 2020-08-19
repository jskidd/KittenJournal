using KittenJournal.DAL;
using KittenJournal.Models;
using KittenJournal.Models.Identity;
using KittenJournal.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace KittenJournal.Tests
{
    public class FeedingsControllerTests
    {
        [Fact]
        public async void TestIndex()
        {
            Mock<UserManager<KittenJournalUser>> userManager = new Mock<UserManager<KittenJournalUser>>(
                                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<KittenJournalUser>>().Object,
                  new IUserValidator<KittenJournalUser>[0],
                  new IPasswordValidator<KittenJournalUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<KittenJournalUser>>>().Object
                );

            Mock<HttpContext> httpContext = new Mock<HttpContext>();
            Mock<ClaimsPrincipal> mockUser = new Mock<ClaimsPrincipal>();

            mockUser.Setup(mu => mu.IsInRole(It.IsAny<string>())).Returns(true);

            httpContext.SetupGet(c => c.User).Returns((mockUser.Object));
            httpContext.Setup(m => m.User.IsInRole("Foster")).Returns(false);
            ControllerContext context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));
            

            // Arrange
            FeedingsController controller = new FeedingsController(await GetDatabaseContext(), new UserManager<KittenJournalUser>(
                new Mock<IUserStore<KittenJournalUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<KittenJournalUser>>().Object,
                  new IUserValidator<KittenJournalUser>[0],
                  new IPasswordValidator<KittenJournalUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<KittenJournalUser>>>().Object
                ));
            controller.ControllerContext = context;

            // Act
            IEnumerable<FeedingViewModel> feedings = ((await controller.Index()) as ViewResult).ViewData.Model as IEnumerable<FeedingViewModel>;
            

            // Asset
            Assert.True(feedings.ToList().Count == 10);
        }

        [Fact]
        public async void TestIndexWithSeachString()
        {
            Mock<UserManager<KittenJournalUser>> userManager = new Mock<UserManager<KittenJournalUser>>(
                      new Mock<IOptions<IdentityOptions>>().Object,
                      new Mock<IPasswordHasher<KittenJournalUser>>().Object,
                      new IUserValidator<KittenJournalUser>[0],
                      new IPasswordValidator<KittenJournalUser>[0],
                      new Mock<ILookupNormalizer>().Object,
                      new Mock<IdentityErrorDescriber>().Object,
                      new Mock<IServiceProvider>().Object,
                      new Mock<ILogger<UserManager<KittenJournalUser>>>().Object
                    );

            Mock<HttpContext> httpContext = new Mock<HttpContext>();
            Mock<ClaimsPrincipal> mockUser = new Mock<ClaimsPrincipal>();

            mockUser.Setup(mu => mu.IsInRole(It.IsAny<string>())).Returns(true);

            httpContext.SetupGet(c => c.User).Returns((mockUser.Object));
            httpContext.Setup(m => m.User.IsInRole("Foster")).Returns(false);
            ControllerContext context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            // Arrange
            KittensController controller = new KittensController(await GetDatabaseContext(), new UserManager<KittenJournalUser>(
                new Mock<IUserStore<KittenJournalUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<KittenJournalUser>>().Object,
                  new IUserValidator<KittenJournalUser>[0],
                  new IPasswordValidator<KittenJournalUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<KittenJournalUser>>>().Object
                ));
            controller.ControllerContext = context; 

            string searchString1 = "test user";
            string searchString2 = "1";

            // Act
            IEnumerable<KittenViewModel> results1 = ((await controller.Index(searchString1)) as ViewResult).ViewData.Model as IEnumerable<KittenViewModel>;
            IEnumerable<KittenViewModel> results2 = ((await controller.Index(searchString2)) as ViewResult).ViewData.Model as IEnumerable<KittenViewModel>;

            // Assert
            Assert.True(results1.Count() == 10);
            Assert.True(results2.Count() == 2);
        }

        [Fact]
        public async void TestDetails()
        {
            Mock<UserManager<KittenJournalUser>> userManager = new Mock<UserManager<KittenJournalUser>>(
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<KittenJournalUser>>().Object,
              new IUserValidator<KittenJournalUser>[0],
              new IPasswordValidator<KittenJournalUser>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<KittenJournalUser>>>().Object
            );

            Mock<HttpContext> httpContext = new Mock<HttpContext>();
            Mock<ClaimsPrincipal> mockUser = new Mock<ClaimsPrincipal>();

            mockUser.Setup(mu => mu.IsInRole(It.IsAny<string>())).Returns(true);

            httpContext.SetupGet(c => c.User).Returns((mockUser.Object));
            httpContext.Setup(m => m.User.IsInRole("Foster")).Returns(false);
            ControllerContext context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            // Arrange
            KittensController controller = new KittensController(await GetDatabaseContext(), new UserManager<KittenJournalUser>(
                new Mock<IUserStore<KittenJournalUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<KittenJournalUser>>().Object,
                  new IUserValidator<KittenJournalUser>[0],
                  new IPasswordValidator<KittenJournalUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<KittenJournalUser>>>().Object
                ));

            controller.ControllerContext = context;

            int id = 5;

            // Act
            KittenViewModel results = ((await controller.Details(id)) as ViewResult).ViewData.Model as KittenViewModel;

            // Assert
            Assert.NotNull(results);
            Assert.True(results.Kitten.Id == 5);

        }

        public class MockUserStore : IUserStore<KittenJournalUser>, IUserPasswordStore<KittenJournalUser>
        {
            public Task<IdentityResult> CreateAsync(KittenJournalUser user, CancellationToken cancellationToken)
            {
                return Task.FromResult<IdentityResult>(new IdentityResult());
            }

            public Task<IdentityResult> DeleteAsync(KittenJournalUser user, CancellationToken cancellationToken)
            {
                return Task.FromResult<IdentityResult>(new IdentityResult());
            }

            public void Dispose()
            {
            }

            public Task<KittenJournalUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
            {
                return Task.FromResult<KittenJournalUser>(new KittenJournalUser());
            }

            public Task<KittenJournalUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
            {
                return Task.FromResult<KittenJournalUser>(new KittenJournalUser());
            }

            public Task<string> GetNormalizedUserNameAsync(KittenJournalUser user, CancellationToken cancellationToken)
            {
                return Task.FromResult<string>("test");
            }

            public Task<string> GetPasswordHashAsync(KittenJournalUser user, CancellationToken cancellationToken)
            {
                return Task.FromResult<string>("test");
            }

            public Task<string> GetUserIdAsync(KittenJournalUser user, CancellationToken cancellationToken)
            {
                return Task.FromResult<string>("test");
            }

            public Task<string> GetUserNameAsync(KittenJournalUser user, CancellationToken cancellationToken)
            {
                return Task.FromResult<string>("test");
            }

            public Task<bool> HasPasswordAsync(KittenJournalUser user, CancellationToken cancellationToken)
            {
                return Task.FromResult<bool>(true);
            }

            public Task SetNormalizedUserNameAsync(KittenJournalUser user, string normalizedName, CancellationToken cancellationToken)
            {
                return Task.FromResult(user.NormalizedUserName = normalizedName);
            }

            public Task SetPasswordHashAsync(KittenJournalUser user, string passwordHash, CancellationToken cancellationToken)
            {
                return Task.FromResult(user.PasswordHash = passwordHash);
            }

            public Task SetUserNameAsync(KittenJournalUser user, string userName, CancellationToken cancellationToken)
            {
                return Task.FromResult(user.UserName = userName);
            }

            public Task<IdentityResult> UpdateAsync(KittenJournalUser user, CancellationToken cancellationToken)
            {
                return Task.FromResult<IdentityResult>(new IdentityResult());
            }
        }

        [Fact]
        public async void TestCreates()
        {
                Mock<UserManager<KittenJournalUser>> userManager = new Mock<UserManager<KittenJournalUser>>(
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<KittenJournalUser>>().Object,
              new IUserValidator<KittenJournalUser>[0],
              new IPasswordValidator<KittenJournalUser>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<KittenJournalUser>>>().Object
            );

            Mock<HttpContext> httpContext = new Mock<HttpContext>();
            Mock<ClaimsPrincipal> mockUser = new Mock<ClaimsPrincipal>();

            mockUser.Setup(mu => mu.IsInRole(It.IsAny<string>())).Returns(true);

            httpContext.SetupGet(c => c.User).Returns((mockUser.Object));
            httpContext.Setup(m => m.User.IsInRole("Foster")).Returns(false);
            ControllerContext context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));

            // Arrange
            KittensController controller = new KittensController(await GetDatabaseContext(), new UserManager<KittenJournalUser>(
                new MockUserStore(),
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<KittenJournalUser>>().Object,
                  new IUserValidator<KittenJournalUser>[0],
                  new IPasswordValidator<KittenJournalUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<KittenJournalUser>>>().Object
                ));

            controller.ControllerContext = context;

            Kitten kitten = new Kitten()
            {
                Name = "Test Create",
                Sex = "Test Create",
                CurrentWeight = 100,
            };

            // Act
            await controller.Create(kitten);
            IEnumerable<KittenViewModel> kittens = ((await controller.Index()) as ViewResult).ViewData.Model as IEnumerable<KittenViewModel>;

            // Assert
            Assert.True(kittens.Count() == 11);
        }

        [Fact]
        public async void TestEdit()
        {
            Mock<UserManager<KittenJournalUser>> userManager = new Mock<UserManager<KittenJournalUser>>(
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<KittenJournalUser>>().Object,
                new IUserValidator<KittenJournalUser>[0],
                new IPasswordValidator<KittenJournalUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<KittenJournalUser>>>().Object
                );

            Mock<HttpContext> httpContext = new Mock<HttpContext>();
            Mock<ClaimsPrincipal> mockUser = new Mock<ClaimsPrincipal>();

            mockUser.Setup(mu => mu.IsInRole(It.IsAny<string>())).Returns(true);

            httpContext.SetupGet(c => c.User).Returns((mockUser.Object));
            httpContext.Setup(m => m.User.IsInRole("Administrator")).Returns(true);
            ControllerContext context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));


            // Arrange
            userManager.Setup(um => um.Users).Returns(
                (new KittenJournalUser [] {
                    new KittenJournalUser()
                    {
                        Email = "test",
                        UserName = "test",
                        PasswordHash = "test",
                        PhoneNumber = "test",
                        FosterId = 9
                    }
                }).AsQueryable()
                );

            userManager.Setup(um => um.UpdateAsync(It.IsAny<KittenJournalUser>())).Returns(Task.FromResult(new IdentityResult()));
            KittensController controller = new KittensController(await GetDatabaseContext(), new UserManager<KittenJournalUser>(
                 new Mock<IUserStore<KittenJournalUser>>().Object,
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<IPasswordHasher<KittenJournalUser>>().Object,
                   new IUserValidator<KittenJournalUser>[0],
                   new IPasswordValidator<KittenJournalUser>[0],
                   new Mock<ILookupNormalizer>().Object,
                   new Mock<IdentityErrorDescriber>().Object,
                   new Mock<IServiceProvider>().Object,
                   new Mock<ILogger<UserManager<KittenJournalUser>>>().Object
                 ));
            controller.ControllerContext = context;

            // Act
            KittenViewModel kitten = (await controller.Details(9) as ViewResult).ViewData.Model as KittenViewModel;
            kitten.Kitten.Name = "Updates User Id 9";
            await controller.Edit(9, kitten.Kitten);
            KittenViewModel updatedKitten = (await controller.Details(9) as ViewResult).ViewData.Model as KittenViewModel;

            // Assert
            Assert.True(kitten.Kitten.Id == 9);
            Assert.True(kitten.Kitten.Name == "Updates User Id 9");
        }

        [Fact]
        public async void TestDelete()
        {
            // Arrange
            Mock<UserManager<KittenJournalUser>> userManager = new Mock<UserManager<KittenJournalUser>>(
                new MockUserStore(),
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<KittenJournalUser>>().Object,
                  new IUserValidator<KittenJournalUser>[0],
                  new IPasswordValidator<KittenJournalUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<KittenJournalUser>>>().Object);
            Mock<HttpContext> httpContext = new Mock<HttpContext>();
            Mock<ClaimsPrincipal> mockUser = new Mock<ClaimsPrincipal>();

            mockUser.Setup(mu => mu.IsInRole(It.IsAny<string>())).Returns(false);

            httpContext.SetupGet(c => c.User).Returns((mockUser.Object));
            httpContext.Setup(m => m.User.IsInRole("Administrator")).Returns(true);
            ControllerContext context = new ControllerContext(new ActionContext(httpContext.Object, new Microsoft.AspNetCore.Routing.RouteData(), new ControllerActionDescriptor()));


            KittensController controller = new KittensController(await GetDatabaseContext(), userManager.Object);
            controller.ControllerContext = context;

            // Act
            await controller.DeleteConfirmed(2);
            List<KittenViewModel> kittens = (await controller.Index() as ViewResult).ViewData.Model as List<KittenViewModel>;
            // Assert
            Assert.True(kittens.Count() == 9);
        }

        private async Task<AppDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new AppDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Kittens.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Kittens.Add(new Kitten()
                    {
                        Id = i,
                        Name = $"test user {i}",
                        CurrentWeight = i,
                        Sex = $"Test Sex {1}",
                        FosterId = 1
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }
            if (await databaseContext.Feedings.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Feedings.Add(new Feeding()
                    {
                        Id = i,
                        KittenId = i,
                        StartingWeight = i,
                        EndWeight = i,
                        Timestamp = DateTime.Now
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }

            if (await databaseContext.Fosters.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Fosters.Add(new Foster()
                    {
                        Id = i,
                        Name = $"test user {i}",
                        Email = $"testuser{i}@example.com",
                        PhoneNumber = $"Phone Number {i}",
                        Address = $"Address {i}",
                        City = "City",
                        State = "State",
                        ZipCode = "12345"
                    });
                    await databaseContext.SaveChangesAsync();
                }
            }


            return databaseContext;
        }
    }
}
