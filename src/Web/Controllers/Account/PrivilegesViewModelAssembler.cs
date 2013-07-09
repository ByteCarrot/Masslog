using System;
using System.Collections.Generic;
using System.Linq;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;

namespace ByteCarrot.Masslog.Web.Controllers.Account
{
    public class PrivilegesViewModelAssembler : IPrivilegesViewModelAssembler
    {
        private readonly IDomainContext _context;

        public PrivilegesViewModelAssembler(IDomainContext context)
        {
            _context = context;
        }

        public List<PrivilegesViewModel> ToViewModels(List<Privileges> entities)
        {
            var models = _context.Applications.All().Select(ToViewModel).ToList();
            foreach (var entity in entities)
            {
                var model = models.Single(x => x.ApplicationId == entity.ApplicationId);
                model.CanBrowse = true;
                model.CanModify = entity.CanModify;
            }
            return models;
        }

        public List<Privileges> ToEntities(List<PrivilegesViewModel> models)
        {
            return models.Where(x => x.CanBrowse).Select(ToEntity).ToList();
        }

        public List<PrivilegesViewModel> ToViewModels()
        {
            return ToViewModels(new List<Privileges>());
        }

        private static Privileges ToEntity(PrivilegesViewModel model)
        {
            if (!model.CanBrowse)
            {
                throw new InvalidOperationException("Privileges entity cannot be created when 'CanBrowse' is false");
            }

            return new Privileges
                       {
                           ApplicationId = model.ApplicationId,
                           CanModify = model.CanModify
                       };
        }

        private PrivilegesViewModel ToViewModel(Application entity)
        {
            return new PrivilegesViewModel
                       {
                           ApplicationId = entity.Id,
                           ApplicationName = entity.Name,
                       };
        }
    }
}