using System;
using ByteCarrot.Masslog.Core.DomainModel.Entities;

namespace ByteCarrot.Masslog.Core.DomainModel.Repositories
{
    public class ActivityQuery : QueryBase
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? LocalDateFrom
        {
            get { return DateFrom.HasValue ? DateFrom.Value.ToLocalTime() : (DateTime?)null; }
            set { DateFrom = value.HasValue ? value.Value.ToUniversalTime() : (DateTime?) null; }
        }

        public DateTime? DateTo { get; set; }

        public DateTime? LocalDateTo
        {
            get { return DateTo.HasValue ? DateTo.Value.ToLocalTime() : (DateTime?) null; }
            set { DateTo = value.HasValue ? value.Value.ToUniversalTime() : (DateTime?)null; }
        }

        public string ApplicationId { get; set; }

        public string HostName { get; set; }

        public string Machine { get; set; }

        public string Url { get; set; }

        public int? StatusCode { get; set; }

        public string User { get; set; }

        public ActivityStatus? Status { get; set; }

        public FailureDeterminedBy? FailureDeterminedBy { get; set; }

        public static ActivityQuery Default()
        {
            return new ActivityQuery
                       {
                           DateFrom = DateTime.Now.AddDays(-1),
                           PageNumber = 1,
                           PageSize = 25,
                           SortColumn = "StartedAt",
                           SortDirection = SortDirection.Descending
                       };
        }
    }
}