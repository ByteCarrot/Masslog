using System.Collections.Generic;

namespace ByteCarrot.Masslog.Core.DomainModel.Entities
{
    public class RouteData
    {
        public RouteData()
        {
            Constraints = new List<NameValue>();
            DataTokens = new List<NameValue>();
            Defaults = new List<NameValue>();
            Values = new List<NameValue>();
        }

        public string Template { get; set; }

        public string Name { get; set; }

        public List<NameValue> Constraints { get; set; }

        public List<NameValue> DataTokens { get; set; }

        public List<NameValue> Defaults { get; set; }

        public List<NameValue> Values { get; set; }
    }
}