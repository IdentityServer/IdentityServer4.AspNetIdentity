using FluentAssertions;
using IdentityModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer4.AspNetIdentity.UnitTests
{
    public class ProfileServiceTests
    {
        private User _user;
        private Mock<UserManager<User>> _mockUserManager;
        private ProfileService<User> _profileService;

        public ProfileServiceTests()
        {
            _user = new User { Id = Guid.NewGuid().ToString() };

            var mockUserStore = new Mock<IUserStore<User>>();
            _mockUserManager = new Mock<UserManager<User>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
            _mockUserManager.Setup(x => x.FindByIdAsync(_user.Id))
                .ReturnsAsync(_user);

            var mockClaimsFactory = new Mock<IUserClaimsPrincipalFactory<User>>();

            _profileService = new ProfileService<User>(_mockUserManager.Object, mockClaimsFactory.Object);

        }

        public class User
        {
            public string Id { get; set; }
        }

        [Fact]
        public async Task IsActiveAsync_should_set_isactive_to_false_when_user_is_not_found()
        {
            var identity = new ClaimsIdentity(new Claim[] { new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()) });

            var context = new IsActiveContext(new ClaimsPrincipal(identity), new Client(), "test");
            await _profileService.IsActiveAsync(context);
            context.IsActive.Should().BeFalse();
        }

        [Fact]
        public async Task IsActiveAsync_should_set_isactive_to_usermanager_getlockoutenabledasync_when_usermanager_supports_user_lockout()
        {
            var identity = new ClaimsIdentity(new Claim[] { new Claim(JwtClaimTypes.Subject, _user.Id) });

            _mockUserManager.SetupGet(x => x.SupportsUserLockout)
                .Returns(true);
            _mockUserManager.Setup(x => x.IsLockedOutAsync(_user))
                .ReturnsAsync(true);

            var context = new IsActiveContext(new ClaimsPrincipal(identity), new Client(), "test");
            await _profileService.IsActiveAsync(context);
            context.IsActive.Should().BeFalse();
        }

        [Fact]
        public async Task IsActiveAsync_should_set_isactive_to_true_when_usermanager_does_not_support_user_lockout()
        {
            var identity = new ClaimsIdentity(new Claim[] { new Claim(JwtClaimTypes.Subject, _user.Id) });

            _mockUserManager.SetupGet(x => x.SupportsUserLockout)
                .Returns(false);

            var context = new IsActiveContext(new ClaimsPrincipal(identity), new Client(), "test");
            await _profileService.IsActiveAsync(context);
            context.IsActive.Should().BeTrue();
        }

    }
}
