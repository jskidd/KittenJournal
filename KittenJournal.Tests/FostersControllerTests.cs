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
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace KittenJournal.Tests
{
    public class FostersControllerTests
    {
        [Fact]
        public async void TestIndex()
        {
            // Arrange
            FostersController controller = new FostersController(await GetDatabaseContext(), new UserManager<KittenJournalUser>(
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
            
            // Act
            IEnumerable<FosterViewModel> fosters = ((await controller.Index()) as ViewResult).ViewData.Model as IEnumerable<FosterViewModel>;
            

            // Asset
            Assert.True(fosters.ToList().Count == 10);
            Assert.True(fosters.ToList()[0].foster.Name == "test user 1");
            Assert.True(fosters.ToList()[1].foster.Id == 2);
        }

        [Fact]
        public async void TestIndexWithSeachString()
        {
            // Arrange
            FostersController controller = new FostersController(await GetDatabaseContext(), new UserManager<KittenJournalUser>(
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
            string searchString1 = "test user";
            string searchString2 = "1";

            // Act
            IEnumerable<FosterViewModel> results1 = ((await controller.Index(searchString1)) as ViewResult).ViewData.Model as IEnumerable<FosterViewModel>;
            IEnumerable<FosterViewModel> results2 = ((await controller.Index(searchString2)) as ViewResult).ViewData.Model as IEnumerable<FosterViewModel>;

            // Assert
            Assert.True(results1.Count() == 10);
            Assert.True(results2.Count() == 2);
        }

        [Fact]
        public async void TestDetails()
        {
            // Arrange
            FostersController controller = new FostersController(await GetDatabaseContext(), new UserManager<KittenJournalUser>(
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

            int id = 5;

            // Act
            FosterViewModel results = ((await controller.Details(id)) as ViewResult).ViewData.Model as FosterViewModel;

            // Assert
            Assert.NotNull(results);
            Assert.True(results.foster.Id == 5);

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
            // Arrange
            FostersController controller = new FostersController(await GetDatabaseContext(), new UserManager<KittenJournalUser>(
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

            CreateEditFosterViewModel fvm = new CreateEditFosterViewModel()
            {
                Name = "Test Create",
                Address = "Test Create",
                City = "Test Create",
                State = "Test Create",
                ZipCode = "TestCreate",
                PhoneNumber = "TestCreate",
                Email = "Test Create",
                Password = "Test Create",
                ConfirmPassword = "Test Create",
                Id = 11
            };

            // Act
            await controller.Create(fvm);
            IEnumerable<FosterViewModel> fosters = ((await controller.Index()) as ViewResult).ViewData.Model as IEnumerable<FosterViewModel>;

            // Assert
            Assert.True(fosters.Count() == 11);
        }

        [Fact]
        public async void TestEdit()
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
            FostersController controller = new FostersController(await GetDatabaseContext(), userManager.Object);

            // Act
            FosterViewModel foster = (await controller.Details(9) as ViewResult).ViewData.Model as FosterViewModel;
            foster.foster.Name = "Updates User Id 9";
            await controller.Edit(9, foster.foster);
            foster = (await controller.Details(9) as ViewResult).ViewData.Model as FosterViewModel;

            // Assert
            Assert.True(foster.foster.Id == 9);
            Assert.True(foster.foster.Name == "Updates User Id 9");
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


            FostersController controller = new FostersController(await GetDatabaseContext(), userManager.Object);

            // Act
            await controller.DeleteConfirmed(2);
            List<FosterViewModel> fosters = (await controller.Index() as ViewResult).ViewData.Model as List<FosterViewModel>;
            // Assert
            Assert.True(fosters.Count() == 9);
        }

        private async Task<AppDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new AppDbContext(options);
            databaseContext.Database.EnsureCreated();
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
