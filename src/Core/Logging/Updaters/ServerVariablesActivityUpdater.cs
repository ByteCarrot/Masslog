using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.Logging.Updaters
{
    public class ServerVariablesActivityUpdater : IActivityUpdater
    {
        public void Update(DataCollectionContext context)
        {
            if (context.Behavior.IgnoreServerVariables)
            {
                context.Activity.VariablesIgnored = true;
                return;
            }

            var request = context.Application.Request;
            context.Activity.Variables = request.ServerVariables.AllKeys
                .Select(x => new NameValue(x, request.ServerVariables[x]))
                .ToList();
        }
    }
}