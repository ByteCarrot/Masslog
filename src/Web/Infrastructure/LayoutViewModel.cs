using System;
using System.Collections.Generic;
using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Web.Infrastructure.Security;

namespace ByteCarrot.Masslog.Web.Infrastructure
{
    public class LayoutViewModel : ViewModel
    {
        public string Username { get; set; }

        public string Version { get; set; }

        public List<NameIdViewModel> Applications { get; set; }

        [NonSerialized] 
        private ISecurityContext _security;
        public ISecurityContext Security
        {
            get { return _security; }
            set { _security = value; }
        } 

        public static LayoutViewModel Create(IList<Application> applications, ISecurityContext security)
        {
            return new LayoutViewModel
                       {
                           Security = security,
                           Version = typeof(LayoutViewModel).Assembly.GetName().Version.ToString(),
                           Applications = applications.Select(x => new NameIdViewModel(x.Id, x.Name))
                                                      .OrderBy(x => x.Name)
                                                      .ToList()
                       };
        }

    }
}