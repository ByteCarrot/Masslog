using AutoMapper;
using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using ByteCarrot.Masslog.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ByteCarrot.Masslog.Core.DomainModel.Entities;
using ByteCarrot.Masslog.Core.DomainModel.Repositories;
using ByteCarrot.Masslog.Web.Infrastructure;

namespace ByteCarrot.Masslog.Web.Controllers.Activities
{
    public class ActivitiesViewModel : ViewModel
    {
        static ActivitiesViewModel()
        {
            Mapper.CreateMap<ActivityQuery, ActivitiesViewModel>()
                .Map(o => o.LocalDateFrom, a => a.LocalDateFrom.ToConsistentFormat())
                .Map(o => o.LocalDateTo, a => a.LocalDateTo.ToConsistentFormat())
                .Map(o => o.Status, a => a.Status == null ? null : a.Status.ToString())
                .Map(o => o.FailureDeterminedBy, a => a.FailureDeterminedBy == null ? null : a.FailureDeterminedBy.ToString())
                .Map(o => o.Statuses, a => MapFailureDeterminedByTypes(a))
                .Map(o => o.FailureDeterminedByTypes, a => MapStatuses(a));

            Mapper.CreateMap<ActivitiesViewModel, ActivityQuery>()
                .Map(o => o.LocalDateFrom,
                     a => a.LocalDateFrom == null ? (object) null : DateTime.Parse(a.LocalDateFrom))
                .Map(o => o.LocalDateTo, a => a.LocalDateTo == null ? (object) null : DateTime.Parse(a.LocalDateTo))
                .Map(o => o.Status, a => a.Status.ToEnum<ActivityStatus>())
                .Map(o => o.FailureDeterminedBy, a => a.FailureDeterminedBy.ToEnum<FailureDeterminedBy>());
        }

        private static List<SelectListItem> MapStatuses(ActivityQuery a)
        {
            var selectedValue = a.FailureDeterminedBy == null ? String.Empty : a.FailureDeterminedBy.ToString();
            return Enum.GetNames(typeof (FailureDeterminedBy)).ToSelectList(selectedValue);
        }

        private static List<SelectListItem> MapFailureDeterminedByTypes(ActivityQuery a)
        {
            var selectedValue = a.Status == null ? String.Empty : a.Status.ToString();
            return Enum.GetNames(typeof (ActivityStatus)).ToSelectList(selectedValue);
        }

        public static ActivitiesViewModel Create(ActivityQuery query, IOrderedEnumerable<Application> applications)
        {
            var model = Mapper.Map<ActivitiesViewModel>(query);
            var list = new List<SelectListItem>
                           {
                               new SelectListItem
                                   {
                                       Selected = query.ApplicationId == null, 
                                       Text = String.Empty, 
                                       Value = null
                                   }
                           };
            list.AddRange(applications.Select(x => Create(query, x)));
            model.Applications = list;
            return model;
        }

        private static SelectListItem Create(ActivityQuery query, Application x)
        {
            return new SelectListItem
                       {
                           Selected = query.ApplicationId != null && x.Id == query.ApplicationId,
                           Text = x.Name,
                           Value = x.Id
                       };
        }

        public ActivityQuery UpdateQuery(ActivityQuery query)
        {
            return Mapper.Map(this, query);
        }

        public string LocalDateFrom { get; set; }

        public string LocalDateTo { get; set; }

        public string Status { get; set; }

        public List<SelectListItem> Statuses { get; set; }

        public string FailureDeterminedBy { get; set; }

        public List<SelectListItem> FailureDeterminedByTypes { get; set; }

        public string ApplicationId { get; set; }

        public List<SelectListItem> Applications { get; set; } 

        public string HostName { get; set; }

        public string Machine { get; set; }

        public string Url { get; set; }

        public string StatusCode { get; set; }

        public string User { get; set; }     
    }
}