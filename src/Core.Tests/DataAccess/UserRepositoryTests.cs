using NUnit.Framework;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;

namespace ByteCarrot.Masslog.Core.Tests.DataAccess
{
    public class UserRepositoryTests : DomainContextTests
    {
        [Test]
        public void ShouldSaveNewUser()
        {
            var admin = UserObjectMother.CreateAdministrator();
            DomainContext.Users.Save(admin);

            var user = DomainContext.Users.Get(admin.Id);
            Assert.That(user.Id, Is.EqualTo(admin.Id));
            Assert.That(user.Username, Is.EqualTo(admin.Username));
        }

        [Test]
        public void ShouldSaveManyUsersAtOnce()
        {
            DomainContext.Users.Save(UserObjectMother.CreateListOfUsers());
            Assert.That(DomainContext.Users.Count(), Is.EqualTo(4));
        }

        [Test]
        public void ShouldFindUserByCredentials()
        {
            DomainContext.Users.Save(UserObjectMother.CreateListOfUsers());
            var user = DomainContext.Users.FindByCredentials("John", "john");

            Assert.That(user, Is.Not.Null);
            Assert.That(user.Username, Is.EqualTo("John"));
        }

        [Test]
        public void ShouldBeAbleToReturnSortedUsers()
        {
            DomainContext.Users.Save(UserObjectMother.CreateListOfUsers());
            var users = DomainContext.Users.Search(
                new UserQuery
                    {
                        PageNumber = 2, 
                        PageSize = 2, 
                        SortColumn = "Username", 
                        SortDirection = SortDirection.Descending
                    });

            Assert.That(users.TotalItems, Is.EqualTo(4));
            Assert.That(users.PageNumber, Is.EqualTo(2));
            Assert.That(users.PageSize, Is.EqualTo(2));
            Assert.That(users.Items[0].Username, Is.EqualTo("John"));
            Assert.That(users.Items[1].Username, Is.EqualTo("Jan"));
        }
    }
}