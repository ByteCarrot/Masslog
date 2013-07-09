using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Core.Rules;

namespace ByteCarrot.Masslog.Core.Logging.Behaviors
{
    public class MonitorBehaviorFactory : IMonitorBehaviorFactory
    {
        private readonly List<IRule> _rules;

        public MonitorBehaviorFactory(ILoggingConfiguration configuration, IDomainContext context, IRulesCompiler compiler)
        {
            var application = context.Applications.Get(configuration.ApplicationId);
            var result = compiler.Compile(application.LoggingRules);
            if (!result.Success)
            {
                var messages = result.Errors.Select(x => "{0} ({1},{2})".Args(x.Message, x.Line, x.Column)).ToArray();
                throw new RulesCompilationException(String.Join(" | ", messages));
            }
            _rules = new List<IRule>(result.Rules);
        }

        public IMonitorBehavior GetBehaviorFor(HttpApplication application)
        {
            var behavior = new MonitorBehavior();
            var activityContext = new ActivityContext(application.Context);
            foreach (var rule in _rules)
            {
                if (!rule.Apply(activityContext))
                {
                    continue;
                }

                Map(rule.IgnoreActivity, value => behavior.IgnoreActivity = value);
                Map(rule.IgnoreRequestBody, value => behavior.IgnoreRequestBody = value);
                Map(rule.IgnoreResponseBody, value => behavior.IgnoreResponseBody = value);
                Map(rule.IgnoreServerVariables, value => behavior.IgnoreServerVariables = value);
                Map(rule.IgnoreRouteData, value => behavior.IgnoreRouteData = value);
            }

            return behavior;
        }

        private static void Map(bool? value, Action<bool> setter)
        {
            if (value.HasValue) setter(value.Value);
        }
    }
}
