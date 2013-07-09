using AutoMapper;
using System.Collections.Generic;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class ExceptionViewModel : ViewModel
    {
        static ExceptionViewModel()
        {
            Mapper.CreateMap<Exception, ExceptionViewModel>();
        }

        public static List<ExceptionViewModel> Create(IList<Exception> exceptions)
        {
            var models = new List<ExceptionViewModel>();
            foreach (var topException in exceptions)
            {
                var level = 0;
                var exception = topException;
                do
                {
                    var model = Mapper.Map<Exception, ExceptionViewModel>(exception);
                    model.Level = level;
                    models.Add(model);
                    exception = exception.InnerException;
                    level++;
                } while (exception != null);
            }

            return models;
        }

        public string Message { get; set; }

        public string Source { get; set; }

        public string StackTrace { get; set; }

        public int Level { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}