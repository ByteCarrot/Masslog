using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using log4net;
using StructureMap;
using System;
using System.Reflection;
using System.Web;

namespace ByteCarrot.Masslog.Core.Logging
{
    public class LoggingModule : IHttpModule
    {
        private readonly IContainer _ioc;
        private readonly ILog _log;
        private HttpApplication _application;

        public LoggingModule(IContainer ioc)
        {
            _ioc = ioc;
            _ioc.GetInstance<Log4NetManager>().Configure();
            _log = Log4NetManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        public LoggingModule() : this(LoggingModuleIoC.Container) {}

        public void Init(HttpApplication application)
        {
            try
            {
                var configuration = _ioc.GetInstance<ILoggingConfiguration>();
                var context = _ioc.GetInstance<IDomainContext>();
                context.Applications.FindByIdOrThrowException(configuration.ApplicationId);

                _application = application;
                _application.BeginRequest += OnBeginRequest;
                _application.EndRequest += OnEndRequest;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
            }
        }

        private void OnBeginRequest(object sender, EventArgs e)
        {
            using (_log.Duration(MethodBase.GetCurrentMethod()))
            {
                try
                {
                    _ioc.GetInstance<IMonitor>().BeginRequest(_application);
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message, ex);
                }
            }
        }

        private void OnEndRequest(object sender, EventArgs e)
        {
            using (_log.Duration(MethodBase.GetCurrentMethod()))
            {
                try
                {
                    _ioc.GetInstance<IMonitor>().EndRequest();
                }
                catch (Exception ex)
                {
                    _log.Error(ex.Message, ex);
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
