using AutoMapper;
using System.ComponentModel.DataAnnotations;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Applications
{
    public class EditViewModel : ViewModel
    {
        static EditViewModel()
        {
            Mapper.CreateMap<EditViewModel, Application>();
            Mapper.CreateMap<Application, EditViewModel>();
        }

        [Required]
        public string Id { get; set; }

        [Required, StringLength(250)]
        public string Name { get; set; }

        public static EditViewModel Create(Application application)
        {
            return Mapper.Map<EditViewModel>(application);
        }

        public void Update(Application application)
        {
            Mapper.Map(this, application);
        }
    }
}