using StructureMap.Attributes;
using System.Web.Mvc;

namespace ByteCarrot.Masslog.Web.Infrastructure.Security
{
    public abstract class CanAttribute : AuthorizeAttribute
    {
        [SetterProperty]
        public ISecurityContext Security { get; set; }
    }
}