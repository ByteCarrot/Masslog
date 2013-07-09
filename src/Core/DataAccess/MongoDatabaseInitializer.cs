using System.Collections.Generic;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Core.Rules;

namespace ByteCarrot.Masslog.Core.DataAccess
{
    public class MongoDatabaseInitializer : IMongoDatabaseInitializer
    {
        private readonly IMongoDatabaseManager _manager;
        private readonly IDomainContext _context;

        public MongoDatabaseInitializer(IMongoDatabaseManager manager, IDomainContext context)
        {
            _manager = manager;
            _context = context;
        }

        public void Initialize()
        {
            _manager.Drop();

            // Applications
            var application1 =
                new Application
                    {
                        Id = "514093925daf2e352cb438be",
                        Name = "Masslog Web Interface",
                        LoggingRules = Rule.Default
                    };
            _context.Applications.Save(application1);

            var application2 =
                new Application
                    {
                        Id = "514210a15daf2e2354c80820",
                        Name = "ASP.NET MVC 4 Web Application",
                        LoggingRules = Rule.Default
                    };
            _context.Applications.Save(application2);

            var application3 =
                new Application
                {
                    Id = "517046945daf2e05d03e0a20",
                    Name = "ASP.NET Web Api",
                    LoggingRules = Rule.Default
                };
            _context.Applications.Save(application3);

            // Users
            _context.Users.Save(
                new User
                    {
                        Id = "514093925daf2e352cb438bc",
                        Username = "administrator",
                        Password = "D8578EDF8458CE06FBC5BB76A58C5CA4",
                        Email = "masslog@bytecarrot.com",
                        IsAdministrator = true
                    });

            _context.Users.Save(
                new User
                    {
                        Id = "514093925daf2e352cb438bd",
                        Username = "user",
                        Password = "D8578EDF8458CE06FBC5BB76A58C5CA4",
                        Email = "masslog@bytecarrot.com",
                        IsAdministrator = false,
                        Privileges = new List<Privileges>
                                         {
                                             new Privileges { ApplicationId = application1.Id, CanModify = false },
                                         }
                    });

            _context.Users.Save(
                new User
                {
                    Id = "514093925daf2e352cb438be",
                    Username = "user2",
                    Password = "D8578EDF8458CE06FBC5BB76A58C5CA4",
                    Email = "masslog@bytecarrot.com",
                    IsAdministrator = false
                });


        }
    }
}