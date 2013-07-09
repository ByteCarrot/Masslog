using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using MongoDB.Bson;
using System.Collections.Generic;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.Tests.DataAccess
{
    public static class UserObjectMother
    {
        public static User CreateAdministrator()
        {
            return CreateUser("Administrator", "some password", true);
        }

        public static IEnumerable<User> CreateListOfUsers()
        {
            yield return CreateUser("Jan", "jan", false);
            yield return CreateUser("Maria", "maria", false);
            yield return CreateUser("Mark", "mark", true);
            yield return CreateUser("John", "john", false);
        }

        public static User CreateUser(string username, string password, bool isAdministrator)
        {
            return new User
                       {
                           Id = ObjectId.GenerateNewId().ToString(),
                           Username = username,
                           Password = password.Md5Hash(),
                           IsAdministrator = isAdministrator
                       };
        }
    }
}