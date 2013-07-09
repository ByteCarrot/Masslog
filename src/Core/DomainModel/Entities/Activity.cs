using ByteCarrot.Masslog.Core.Infrastructure.Extensions;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class Activity : Entity
    {
        public Activity()
        {
            Variables = new List<NameValue>();
            Exceptions = new List<Exception>();
        }

        public string ApplicationId { get; set; }

        public string HostName { get; set; }

        public string Url { get; set; }

        public int StatusCode { get; set; }

        public DenormalizedDateTime StartedAt { get; set; }

        public DenormalizedDateTime EndedAt { get; set; }

        public TimeSpan? Duration { get; set; }

        public string UserHostAddress { get; set; }

        public string UserHostName { get; set; }

        public string Machine { get; set; }

        public string MachineFqn { get; set; }

        public string User { get; set; }

        public ActivityStatus Status { get; set; }

        public FailureDeterminedBy? FailureDeterminedBy { get; set; }

        public List<NameValue> Variables { get; set; }

        public bool VariablesIgnored { get; set; }

        public List<Exception> Exceptions { get; set; }

        public Request Request { get; set; }

        public Response Response { get; set; }

        public RouteData RouteData { get; set; }

        [BsonIgnore]
        public DateTime LocalStartedAt
        {
            get { return StartedAt.Date.ToLocalTime(); }
            set { StartedAt = new DenormalizedDateTime(value.ToUniversalTime()); }
        }

        [BsonIgnore]
        public DateTime LocalEndedAt
        {
            get { return EndedAt.Date.ToLocalTime(); }
            set { EndedAt = new DenormalizedDateTime(value.ToUniversalTime()); }
        }

        public bool RouteDataIgnored { get; set; }

        public void SetStatusToSuccess()
        {
            Status = ActivityStatus.Success;
        }

        public void SetStatusToFailure(FailureDeterminedBy failureDeterminedBy)
        {
            Status = ActivityStatus.Failure;
            FailureDeterminedBy = failureDeterminedBy;
        }

        public string GetStatusInfo()
        {
            return Status == ActivityStatus.Success ? Status.ToString() : "{0} ({1})".Args(Status, FailureDeterminedBy);
        }
    }
}